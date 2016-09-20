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
 * $LastChangedRevision: 2089 $
 */ 

#ifndef __RADIO_INIT__
#define __RADIO_INIT__

typedef enum {
  RF_IDLE,    /**< Radio is idle */
  RF_MAX_RT,  /**< Maximum number of retries have occured */
  RF_TX_DS,   /**< Data is sent */
  RF_RX_DR,   /**< Data recieved */
  RF_TX_AP,   /**< Ack payload recieved */
  RF_BUSY     /**< Radio is busy */
} radio_status_t;

typedef struct
{
  uint8_t radioChannel;
  uint8_t radioMode;
  uint8_t payloadLength;
  uint8_t crcMode;
  uint8_t bitRate;
  uint8_t radioAddress[5];
  uint8_t radioAddressLen;
}radioSettings_t;

/** Defines the channel the radio should operate on*/
#define RF_CHANNEL 2

/** Defines the time it takes for the radio to come up to operational mode */
#define RF_POWER_UP_DELAY 2

/** Defines the payload length the radio should use */
#define RF_PAYLOAD_LENGTH 1                           

/** Defines how many retransmitts that should be performed */
#define RF_RETRANSMITS 15

/** Defines the retransmit delay. Should be a multiple of 250. If the 
 * RF_PAYLOAD_LENGTH is larger than 18, a higher retransmitt delay need to
 * be set. This is because both the original package and ACK payload will
 * be of this size. When the ACK payload exeedes 18 byte, it will not be able
 * to recieve the full ACK in the ordinary 250 mikroseconds, so the delay
 * will need to be increased. */
#define RF_RETRANS_DELAY 250

#define ALL_PIPES 0x3F

void sniffer_radio_sb_init (radioSettings_t *settingPtr);
void sniffer_radio_pl_init (radioSettings_t *settingPtr);
void sniffer_radio_esb_init (radioSettings_t *settingPtr);

#endif
