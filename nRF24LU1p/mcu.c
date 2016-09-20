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
 * $LastChangedRevision: 2093 $
 */ 

 /** @file
 * @ingroup LU1
 * Implementation of nRF24LU1 hardware functions. This file implements the
 * system_init() function, that sets up hardware such as internal clocks,
 * timers, and IO ports.
 *
 * @author Per Kristian Schanke
 */

#include "hal_nrf.h"
#include "system.h"

void port_init(uint8_t alt, uint8_t dir, uint8_t value);

void system_init(void)
{
  port_init(0x00, 0x38, 0x00);      // P0[2..0] outputs LED1,2,3

  USBSLP = 0x01;                    // shut down USB part...save pwr

  delay_10ms();
  WUIRQ = 1;                        // wakeup int enabled
  EA = 1;                           // global interrupt enable
}

void port_init(uint8_t alt, uint8_t dir, uint8_t value)
{
  P0ALT = alt;
  P0DIR = dir;
  P0    = value;  
}