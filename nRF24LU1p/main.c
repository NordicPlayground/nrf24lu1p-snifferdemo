/* Copyright (c) 2007 Nordic Semiconductor. All Rights Reserved.
 *
 * The information contained herein is distributed in the hope that
 * it will be useful, but WITHOUT WARRANTY OF ANY KIND.
 *
 * @author Torbjørn Øvrebekk
 *
 */
#include <Nordic\reg24lu1.h>
#include <stdint.h>

#include "config.h"
#include "usb.h"
#include "stdint.h"
#include "system.h"
#include "hal_flash.h"

#include "cklf.h"
#include "hal_nrf.h"
#include "hal_nrf_reg.h"
#include "target_includes.h"
#include "radio_init.h"

#define FIRMWARE_MAIN 6
#define FIRMWARE_SUB  0x07

#define MAX_PACKETS_PR_25MS 15

#define CMD_FIRMWARE_VERSION   0x01
#define CMD_LED1               0x41
#define CMD_LED2               0x42
#define CMD_LED3               0x43
#define CMD_CONFIGURE_RADIO    0x60
#define CMD_GET_RADIO_SETTINGS 0x61
#define CMD_DEACTIVATE_RADIO   0x62
#define CMD_ALLOW_RADIO_COM    0x68
#define CMD_DONT_ALLOW_RADIO_COM 0x69
#define CMD_REACTIVATE_BOOTLOADER 0x70
#define CMD_RESET_DONGLE          0x71
#define CMD_TRANSMIT_PACKET    0x90
#define CMD_INTERVAL_TRANSMIT  0x91

#define CMD_TRANSMIT_PACKET_DUMP 0xA0

#define DATA_DUMP_BUFFER_SIZE  150

typedef enum e_UsbWriteCommands{ USB_FIRMWARE_VERSION = 0xa1, USB_BUTTON_PRESSED = 0xa2, USB_RADIO_DATA = 0xb0, USB_RADIO_CONFIG_DATA = 0xb1, USB_RADIO_CONFIRM_DEACTIVATE = 0xb2, USB_RADIO_PACKET_SENT = 0xb3, USB_MANY_PACKETS_RECEIVED = 0xc0, USB_TIME_BETWEEN_PACKETS = 0xC1 };
data uint8_t packet_received=0, pckCounter = 0, i, radioActive = 0, allowRadioCom = 1, radio_status = RF_IDLE;
data uint8_t timer_div = 0, received_packets = 0, radio_packet_length, time_between_packets_hibyte = 0, packet_sent = 0;
idata uint16_t total_received_packets;
idata uint16_t tbp1, tbp2, tbp3;
idata radioSettings_t settings;
static code const uint8_t address[HAL_NRF_AW_5BYTES] = {0x22,0x33,0x44,0x55,0x01};
data bit send_all_packets = 1, update_time_between_packets = 0, send_time_between_packets = 0;
data uint8_t pload[32];

extern xdata volatile unsigned char in1buf[];
extern xdata volatile unsigned char out1buf[];
extern xdata volatile unsigned char in1bc;

xdata uint8_t tx_buf[32];   
data bit wdog_keep_alive = 1;
uint8_t tx_pl;

xdata uint8_t data_dump_buffer[DATA_DUMP_BUFFER_SIZE];
xdata uint8_t data_dump_buffer_radio_offset;

void some_delay()
{
  data uint16_t delCnt=32000;
  while(delCnt--);
}

// NB!! For this to work the activate_bootloader must be placed after address 512 in the code flash.
// If not, the function will detele its own code when running hal_flash_page_erase(0);
void activate_bootloader()
{
  EA = 0;
    
  // Change reset vector to 0x7800
  hal_flash_page_erase(0);
  hal_flash_byte_write(0x0000, 0x02);
  hal_flash_byte_write(0x0001, 0x78);    
  hal_flash_byte_write(0x0002, 0x00); 

  // Reset MCU by activating watchdog
  REGXH = 0;
  REGXL = 1;
  REGXC = 0x08;
}
void sendUsbPackage(uint8_t packageType);
void parse_commands(void)
{
    unsigned char count = 0, i, length, offset;

    switch(out1buf[0])
    {
      case CMD_FIRMWARE_VERSION:
        sendUsbPackage(USB_FIRMWARE_VERSION);
        break;
  
      case CMD_CONFIGURE_RADIO:
        settings.radioChannel = out1buf[1];
        settings.radioMode = out1buf[2];
        settings.payloadLength = out1buf[3];
        settings.crcMode = out1buf[4];
        settings.bitRate = out1buf[5];
        settings.radioAddressLen = out1buf[6];
        for( i = 0; i < settings.radioAddressLen; i++ )
          settings.radioAddress[i] = out1buf[7+i];
        //radio_esb_init(address, HAL_NRF_PRX);   // Configure the radio as primary receiver
        if( settings.radioMode == 0 )sniffer_radio_sb_init(&settings);
        else if( settings.radioMode == 1 )sniffer_radio_esb_init(&settings);
        else if( settings.radioMode == 2 )sniffer_radio_pl_init(&settings);
        else if( settings.radioMode == 3 )sniffer_radio_sb_init(&settings);
        //P0 = settings.radioMode+1;
        radioActive = 1;//settings.radioMode+1;

        sendUsbPackage(USB_RADIO_CONFIG_DATA);

        CE_HIGH(); 
        count = 0;
        break;
      case CMD_GET_RADIO_SETTINGS:
        sendUsbPackage(USB_RADIO_CONFIG_DATA);
        break;
      case CMD_DEACTIVATE_RADIO:
        CE_LOW();
        radioActive = 0;
        sendUsbPackage(USB_RADIO_CONFIRM_DEACTIVATE);
        break;
      case CMD_ALLOW_RADIO_COM:
        allowRadioCom = 1;
        break;
      case CMD_DONT_ALLOW_RADIO_COM:
        allowRadioCom = 0;
        break;
      case CMD_REACTIVATE_BOOTLOADER:
        activate_bootloader();
        break;
      case CMD_RESET_DONGLE:
        wdog_keep_alive = 0;
        break;
      case CMD_TRANSMIT_PACKET:
        tx_pl = out1buf[1];
        for( i = 0; i < tx_pl; i++ )
          pload[i] = out1buf[i+2];
        CE_LOW();
        hal_nrf_set_operation_mode(HAL_NRF_PTX);
        hal_nrf_write_tx_pload(pload, tx_pl);
        CE_PULSE(); 
        radio_status = RF_BUSY;   
        /*while( radio_status != RF_TX_DS && radio_status != RF_MAX_RT);       
        in1buf[2] = (radio_status == RF_TX_DS );
        radio_status = RF_IDLE;
        sendUsbPackage(USB_RADIO_PACKET_SENT); 
        hal_nrf_set_operation_mode(HAL_NRF_PRX);
        CE_HIGH();*/
        break;
      case CMD_TRANSMIT_PACKET_DUMP:
        length = out1buf[1];
        offset = out1buf[2];
        // A length of 0xFF means we should flush the buffer over the radio
        if(length == 0xFF)
        {
          CE_LOW();
          hal_nrf_set_operation_mode(HAL_NRF_PTX);
          radio_status = RF_BUSY;   
          data_dump_buffer_radio_offset = 0;
        }
        // When the length is less than 0xFF and the sum of length + offset is less than the buffer size, 
        // copy the data to the buffer
        else if((length + offset) < DATA_DUMP_BUFFER_SIZE)
        {
            for(i = 0; i < length; i++)
                data_dump_buffer[i + offset] = out1buf[i + 3];
        }
        
        break;
    default:
        break;
    }
    if (count > 0)
        in1bc = count;
}

void usb_send_radio_data(uint8_t len)
{
  data uint8_t i;
  if( allowRadioCom )
  {
    in1buf[0] = USB_RADIO_DATA;
    in1buf[1] = pckCounter++;
    for( i = 0; i < len; i++ )
      in1buf[i+2] = pload[i];
    in1bc = 2+len;
  }
}
void usb_send_radio_and_freq_data(uint8_t len)
{
  data uint8_t i;
  if( allowRadioCom )
  {
    in1buf[0] = USB_TIME_BETWEEN_PACKETS;
    in1buf[1] = pckCounter++;
    for( i = 0; i < len; i++ )
      in1buf[i+2] = pload[i];
    in1buf[len+2] = tbp3;
    in1buf[len+3] = tbp2;
    in1buf[len+4] = tbp1;

    in1bc = 5+len;
  } 
} 
void sendUsbPackage(uint8_t packageType)
{
  in1buf[0] = packageType;
  in1buf[1] = pckCounter++;
  switch(packageType)
  {
    case USB_FIRMWARE_VERSION:
        in1buf[2] = FIRMWARE_MAIN;
        in1buf[3] = FIRMWARE_SUB;
        hal_nrf_write_reg(RF_SETUP, (hal_nrf_read_reg(RF_SETUP) | (1<<5)));
        in1buf[4] = ((hal_nrf_read_reg(RF_SETUP) & (1<<5))!=0);
        hal_nrf_write_reg(RF_SETUP, (hal_nrf_read_reg(RF_SETUP) & ~(1<<5)));
        in1buf[5] = radioActive;
        in1bc = 6;
        break;
    case USB_RADIO_CONFIG_DATA:
        in1buf[2] = settings.radioChannel;
        in1buf[3] = settings.radioMode;
        in1buf[4] = settings.payloadLength;
        in1buf[5] = settings.crcMode;
        in1buf[6] = settings.bitRate;
        in1buf[7] = settings.radioAddressLen;
        for( i = 0; i < settings.radioAddressLen; i++ )
          in1buf[8+i] = settings.radioAddress[i];
        in1bc = 8 + settings.radioAddressLen;
        break;
    case USB_RADIO_CONFIRM_DEACTIVATE:
      in1bc = 2;
      break;
    case USB_RADIO_PACKET_SENT:
      in1bc = 3;
      break;
    case USB_MANY_PACKETS_RECEIVED:
      in1buf[2] = received_packets;
      in1bc = 3;
      break;

  }
}
void timer0_init()
{
  TMOD = 0x01;
  ET0 = 1;
  TR0 = 1;
}
void main(void)
{
  usb_init();
  P0DIR = 0x00;
  delay_10ms();
  
  USB = 1;
  //IP0 |= 0x02;
  //IP1 |= 0x02;
  RF = 1;
                         // global interrupt enable
  RFCKEN = 1;        // enable L01 clock
  RFCTL = 0x10;      // L01 SPI speed = max (CK/2) & SPI enable

  //timer0_init();
  TICKDV = 32;
  //cklf_rtc_init(0x00, 100);
  WUIRQ = 1;
  P0 = 0;
  EA = 1;  
  packet_received = 0;
  //cklf_wdog_init(0xFFFF);
  while(1)
  {
    if( radioActive )
    {
      if( radio_status == RF_RX_DR )
      {
        //if( send_time_between_packets )
          //usb_send_radio_and_freq_data(radio_packet_length);  
          if( send_time_between_packets )
          {
            usb_send_radio_and_freq_data(radio_packet_length);
            send_time_between_packets = 0;
          }else usb_send_radio_data(radio_packet_length);
        radio_status = RF_IDLE;
      }
      if( packet_sent )
      {
        if( settings.radioMode == 0 || settings.radioMode == 3)
          in1buf[2] = 2;
        else
          in1buf[2] = (packet_sent == 1);
        radio_status = RF_IDLE;
        sendUsbPackage(USB_RADIO_PACKET_SENT); 
        hal_nrf_set_operation_mode(HAL_NRF_PRX);
        CE_HIGH();
        packet_sent = 0;
      }
    }
  }
}
void timer0_interrupt() interrupt INTERRUPT_T0 
{
  time_between_packets_hibyte++;
} 
void rtc_interrupt() interrupt INTERRUPT_WU
{
  update_time_between_packets = 1;
}
void usbInterrupt() interrupt INTERRUPT_USB_INT
{
  P01 = !P01;
  usb_irq();      
  if(packet_received == 1)
  {
    parse_commands();
    packet_received = 0;
  }
}
void rf_interrupt() interrupt INTERRUPT_RFIRQ
{
  P00 = !P00;
  switch(hal_nrf_get_clear_irq_flags ())
  {
    case (1<<HAL_NRF_MAX_RT):                 // Max retries reached
      P03 = 1;
      hal_nrf_flush_tx();                     // flush tx fifo, avoid fifo jam
      radio_status = RF_MAX_RT;
      packet_sent = 2;
      break;
    
    case (1<<HAL_NRF_TX_DS):                  // Packet sent
      P04 = 1;
      radio_status = RF_TX_DS;
      packet_sent = 1;
      break;
    
    case (1<<HAL_NRF_RX_DR):                  // Packet received
      TR0 = 0;
      tbp3 = time_between_packets_hibyte;
      if( update_time_between_packets )
      {
        P00 = !P00;
        tbp1 = TL0;
        tbp2 = TH0;
        update_time_between_packets = 0;
        send_time_between_packets = 1;
      }
      TH0 = TL0 = 0;
      time_between_packets_hibyte = 0;
      TR0 = 1;
      if (!hal_nrf_rx_fifo_empty ())
      {
        received_packets++;
        radio_packet_length = hal_nrf_read_rx_pl_w();
        hal_nrf_read_multibyte_reg(HAL_NRF_RX_PLOAD, pload);
      }
      radio_status = RF_RX_DR;
      break;

    case ((1<<HAL_NRF_RX_DR)|(1<<HAL_NRF_TX_DS)): // Ack payload recieved
      //P02 = 1;
      while (!hal_nrf_rx_fifo_empty ())
      {
        hal_nrf_read_rx_pload(pload);
      }
      radio_status = RF_TX_AP;
      break;

    default:
      break;    
  }
}