/* Copyright (c) 2007 Nordic Semiconductor. All Rights Reserved.
 *
 * The information contained herein is property of Nordic Semiconductor ASA.
 * Terms and conditions of usage are described in detail in NORDIC
 * SEMICONDUCTOR STANDARD SOFTWARE LICENSE AGREEMENT. 
 *
 * Licensees are granted free, non-transferable use of the information. NO
 * WARRENTY of ANY KIND is provided. This heading must NOT be removed from
 * the file.
 *
 * $LastChangedRevision: 2185 $
 */ 

/** @ingroup ESB 
 * @file
 * Initialise the radio in Enhanced ShockBurst mode. This is done by opening 
 * @b pipe0 with auto ACK and with auto retransmits.
 *
 * @author Per Kristian Schanke
 */

#include "hal_nrf_reg.h"
#include "hal_nrf.h"
#include "radio_init.h"
#include "system.h"

void sniffer_radio_sb_init (radioSettings_t *settingPtr)
{
  hal_nrf_close_pipe(HAL_NRF_ALL);               // First close all radio pipes
                                                 // Pipe 0 and 1 open by default
  hal_nrf_open_pipe(HAL_NRF_PIPE0, false);       // Open pipe0, without/autoack
  switch( settingPtr->crcMode )
  {
    case 0:
      hal_nrf_set_crc_mode(HAL_NRF_CRC_OFF);       // Operates in no CRC mode
      break;
    case 1:
      hal_nrf_set_crc_mode(HAL_NRF_CRC_8BIT);       // Operates in 8bits CRC mode
      break;
    case 2:
      hal_nrf_set_crc_mode(HAL_NRF_CRC_16BIT);       // Operates in 16bits CRC mode
      break;
  }

  hal_nrf_set_auto_retr(15, RF_RETRANS_DELAY);    // Disables auto retransmit

  hal_nrf_set_address_width(settingPtr->radioAddressLen);  // 5 bytes address width
  hal_nrf_set_address(HAL_NRF_TX, settingPtr->radioAddress);      // Set device's addresses
  hal_nrf_set_address(HAL_NRF_PIPE0, settingPtr->radioAddress);   // Sets recieving address on 
                                                 // pipe0  
  
  hal_nrf_set_operation_mode(HAL_NRF_PRX);     // Enter RX mode
  hal_nrf_set_rx_pload_width((uint8_t)HAL_NRF_PIPE0, settingPtr->payloadLength);
                                                 // Pipe0 expect 
                                                 // PAYLOAD_LENGTH byte payload
                                                 // PAYLOAD_LENGTH in radio.h
  hal_nrf_set_rf_channel(settingPtr->radioChannel);            // Operating on static channel 
                                                 // Defined in radio.h. 
                                                 // Frequenzy = 
                                                 //        2400 + RF_CHANNEL
  hal_nrf_disable_dynamic_pl();                   // Enables dynamic payload
  hal_nrf_disable_ack_pl();
  hal_nrf_disable_dynamic_ack();
  hal_nrf_set_power_mode(HAL_NRF_PWR_UP);        // Power up device

  switch( settingPtr->bitRate )
  {
    case 0:
      hal_nrf_write_reg(RF_SETUP, (hal_nrf_read_reg(RF_SETUP) | (1<<5)));
      hal_nrf_write_reg(RF_SETUP, (hal_nrf_read_reg(RF_SETUP) & ~(1<<3)));
      break;
    case 1:
      hal_nrf_set_datarate(HAL_NRF_1MBPS);
      break;
    case 2:
      hal_nrf_set_datarate(HAL_NRF_2MBPS);
      break;
  }                                          // compatibility with nRF2401 
                                                 // and nRF24E1
  start_timer(RF_POWER_UP_DELAY);                // Wait for the radio to 
  wait_for_timer();                              // power up
} 
   
void sniffer_radio_esb_init (radioSettings_t *settingPtr)
{
  hal_nrf_close_pipe(HAL_NRF_ALL);               // First close all radio pipes
                                                 // Pipe 0 and 1 open by default
  hal_nrf_open_pipe(HAL_NRF_PIPE0, true);        // Then open pipe0, w/autoack
                                                 // Changed from sb/radio_sb.c

  switch( settingPtr->crcMode )
  {
    case 0:
      hal_nrf_set_crc_mode(HAL_NRF_CRC_OFF);       // Operates in no CRC mode
      break;
    case 1:
      hal_nrf_set_crc_mode(HAL_NRF_CRC_8BIT);       // Operates in 8bits CRC mode
      break;
    case 2:
      hal_nrf_set_crc_mode(HAL_NRF_CRC_16BIT);       // Operates in 16bits CRC mode
      break;
  }
  hal_nrf_set_auto_retr(RF_RETRANSMITS, RF_RETRANS_DELAY);                 

  hal_nrf_set_address_width(settingPtr->radioAddressLen);  // 5 bytes address width
  hal_nrf_set_address(HAL_NRF_TX, settingPtr->radioAddress);      // Set device's addresses
  hal_nrf_set_address(HAL_NRF_PIPE0, settingPtr->radioAddress);   // Sets recieving address on 
                                                 // pipe0
  
  hal_nrf_set_operation_mode(HAL_NRF_PRX);     // Enter RX mode
  hal_nrf_set_rx_pload_width((uint8_t)HAL_NRF_PIPE0, settingPtr->payloadLength);

  hal_nrf_set_rf_channel(settingPtr->radioChannel);            // Operating on static channel 

  hal_nrf_disable_dynamic_pl();                   // Enables dynamic payload
  hal_nrf_disable_ack_pl();
  hal_nrf_disable_dynamic_ack();
  hal_nrf_set_power_mode(HAL_NRF_PWR_UP);        // Power up device

  switch( settingPtr->bitRate )
  {
    case 0:
      hal_nrf_write_reg(RF_SETUP, (hal_nrf_read_reg(RF_SETUP) | (1<<5)));
      hal_nrf_write_reg(RF_SETUP, (hal_nrf_read_reg(RF_SETUP) & ~(1<<3)));
      break;
    case 1:
      hal_nrf_set_datarate(HAL_NRF_1MBPS);
      break;
    case 2:
      hal_nrf_set_datarate(HAL_NRF_2MBPS);
      break;
  }     
  
  start_timer(RF_POWER_UP_DELAY);                // Wait for the radio to 
  wait_for_timer();                              // power up
}

void sniffer_radio_pl_init(radioSettings_t *settingPtr)
{
  hal_nrf_close_pipe(HAL_NRF_ALL);               // First close all radio pipes
                                                 // Pipe 0 and 1 open by default
  hal_nrf_open_pipe(HAL_NRF_PIPE0, true);        // Then open pipe0, w/autoack 
  switch( settingPtr->crcMode )
  {
    case 0:
      hal_nrf_set_crc_mode(HAL_NRF_CRC_8BIT);       // Cannot operate in no CRC mode here
      break;
    case 1:
      hal_nrf_set_crc_mode(HAL_NRF_CRC_8BIT);       // Operates in 8bits CRC mode
      break;
    case 2:
      hal_nrf_set_crc_mode(HAL_NRF_CRC_16BIT);       // Operates in 16bits CRC mode
      break;
  } 

  hal_nrf_set_auto_retr(RF_RETRANSMITS, RF_RETRANS_DELAY);
                                                 // Enables auto retransmit.
                                                 // 3 retrans with 250ms delay

  hal_nrf_set_address_width(settingPtr->radioAddressLen);  // 5 bytes address width
  hal_nrf_set_address(HAL_NRF_TX, settingPtr->radioAddress);      // Set device's addresses
  hal_nrf_set_address(HAL_NRF_PIPE0, settingPtr->radioAddress);   // Sets recieving address on 
                                                 // pipe0

/*****************************************************************************
 * Changed from esb/radio_esb.c                                              *
 * Enables:                                                                  *
 *  - ACK payload                                                            *
 *  - Dynamic payload width                                                  *
 *  - Dynamic ACK                                                            *
 *****************************************************************************/
  hal_nrf_enable_ack_pl();                       // Try to enable ack payload

  // When the features are locked, the FEATURE and DYNPD are read out 0x00
  // even after we have tried to enable ack payload. This mean that we need to
  // activate the features.
  if(hal_nrf_read_reg(FEATURE) == 0x00 && (hal_nrf_read_reg(DYNPD) == 0x00))
  {
    hal_nrf_lock_unlock ();                      // Activate features
    hal_nrf_enable_ack_pl();                     // Enables payload in ack
  }

  hal_nrf_enable_dynamic_pl();                   // Enables dynamic payload
  hal_nrf_setup_dyn_pl(ALL_PIPES);               // Sets up dynamic payload on
                                                 // all data pipes.
/*****************************************************************************
 * End changes from esb/radio_esb.c                                          *
 *****************************************************************************/
   
    hal_nrf_set_operation_mode(HAL_NRF_PRX);     // Enter RX mode
    hal_nrf_set_rx_pload_width((uint8_t)HAL_NRF_PIPE0, settingPtr->payloadLength);


  hal_nrf_set_rf_channel(settingPtr->radioChannel);            // Operating on static channel
                                                 // Defined in radio.h. 
                                                 // Frequenzy = 
                                                 //        2400 + RF_CHANNEL

  switch( settingPtr->bitRate )
  {
    case 0:
      hal_nrf_write_reg(RF_SETUP, (hal_nrf_read_reg(RF_SETUP) & ~(1<<3)));
      hal_nrf_write_reg(RF_SETUP, (hal_nrf_read_reg(RF_SETUP) | (1<<5)));
      break;
    case 1:
      hal_nrf_write_reg(RF_SETUP, (hal_nrf_read_reg(RF_SETUP) & ~(1<<5)));
      hal_nrf_set_datarate(HAL_NRF_1MBPS);
      break;
    case 2:
      hal_nrf_write_reg(RF_SETUP, (hal_nrf_read_reg(RF_SETUP) & ~(1<<5)));
      hal_nrf_set_datarate(HAL_NRF_2MBPS);
      break;
  } 
 
  hal_nrf_set_power_mode(HAL_NRF_PWR_UP);        // Power up device
  
  start_timer(RF_POWER_UP_DELAY);                // Wait for the radio to 
  wait_for_timer();                              // power up
}   

void sniffer_radio_auto_init (radioSettings_t *settingPtr)
{
  hal_nrf_close_pipe(HAL_NRF_ALL);               // First close all radio pipes
                                                 // Pipe 0 and 1 open by default
  hal_nrf_open_pipe(HAL_NRF_PIPE0, false);       // Open pipe0, without/autoack
  hal_nrf_set_crc_mode(HAL_NRF_CRC_OFF);       // Operates in no CRC mode

  hal_nrf_set_address_width(settingPtr->radioAddressLen);  // 5 bytes address width
  hal_nrf_set_address(HAL_NRF_TX, settingPtr->radioAddress);      // Set device's addresses
  hal_nrf_set_address(HAL_NRF_PIPE0, settingPtr->radioAddress);   // Sets recieving address on pipe0  
  
  hal_nrf_set_operation_mode(HAL_NRF_PRX);     // Enter RX mode
  hal_nrf_set_rx_pload_width((uint8_t)HAL_NRF_PIPE0, 32);
                                                 // Pipe0 expect 
                                                 // PAYLOAD_LENGTH byte payload
                                                 // PAYLOAD_LENGTH in radio.h
  hal_nrf_set_rf_channel(settingPtr->radioChannel);            // Operating on static channel 

  hal_nrf_set_power_mode(HAL_NRF_PWR_UP);        // Power up device

  switch( settingPtr->bitRate )
  {
    case 0:
      hal_nrf_write_reg(RF_SETUP, (hal_nrf_read_reg(RF_SETUP) | (1<<5)));
      hal_nrf_write_reg(RF_SETUP, (hal_nrf_read_reg(RF_SETUP) & ~(1<<3)));
      break;
    case 1:
      hal_nrf_set_datarate(HAL_NRF_1MBPS);
      break;
    case 2:
      hal_nrf_set_datarate(HAL_NRF_2MBPS);
      break;
  }                                          // compatibility with nRF2401 
                                                 // and nRF24E1
  start_timer(RF_POWER_UP_DELAY);                // Wait for the radio to 
  wait_for_timer();                              // power up
} 