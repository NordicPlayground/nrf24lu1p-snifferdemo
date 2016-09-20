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
 * $LastChangedRevision: 2129 $
 */ 
 
 /** @file
 */
#ifndef SYSTEM_H__
#define SYSTEM_H__

#include <stdint.h>

extern volatile uint8_t led_blink;

/** Enumeration of the LED's to be used to 
 * indicate which bit in @b led_blink that 
 * indicate which LED. */
enum {
 LED1_INDEX,
 LED2_INDEX,
 LED3_INDEX
};

//lint -e717
#define LED1_ON() do{LED1 = LED_ON;}while(0) /**< Turn on LED1 */
#define LED2_ON() do{LED2 = LED_ON;}while(0) /**< Turn on LED2 */
#define LED3_ON() do{LED3 = LED_ON;}while(0) /**< Turn on LED3 */

#define LED1_OFF() do{LED1 = LED_OFF;}while(0) /**< Turn off LED1 */
#define LED2_OFF() do{LED2 = LED_OFF;}while(0) /**< Turn off LED2 */
#define LED3_OFF() do{LED3 = LED_OFF;}while(0) /**< Turn off LED3 */

/** Turn off all LED's */
#define LED_ALL_OFF() do{ LED1_OFF(); \
                          LED2_OFF(); \
                          LED3_OFF(); \
                         }while(0)

/** Initialize LED1 blinking. Enables timer0 interrupts to stop the blinking. */
#define LED1_BLINK() do{  LED1_ON();        \
                          T0_START();       \
                          led_blink |= (1<<LED1_INDEX); \
                          }while(0)
/** Initialize LED2 blinking. Enables timer0 interrupts to stop the blinking. */
#define LED2_BLINK() do{  LED2_ON();        \
                          T0_START();       \
                          led_blink |= (1<<LED2_INDEX); \
                          }while(0)
/** Initialize LED3 blinking. Enables timer0 interrupts to stop the blinking. */
#define LED3_BLINK() do{  LED3_ON();        \
                          T0_START();       \
                          led_blink |= (1<<LED3_INDEX); \
                          }while(0)

/** Checks whether B1 is pressed or not */
#define B1_PRESSED() (B1 == B_PRESSED)
/** Checks whether B2 is pressed or not */
#define B2_PRESSED() (B2 == B_PRESSED)
/** Checks whether B3 is pressed or not */
#define B3_PRESSED() (B3 == B_PRESSED)

void system_init();       /**< Initialise the timers, ports, interupts, etc */
void device_boot_msg();   /**< Runs a LED pattern to indicate startup */
void delay_10ms();        /**< Delay of aprox 10ms */
void delay_100ms();       /**< Delay of aprox 100ms */

/** Starts a timer that runs @a time ms. As it is only possible to run for aprox
 * 50ms using the internal timers, anything above 50ms is done by restarting the
 * counter. To restart the counter, either wait_for_timer() or timer_done() should
 * be called. These check the timer and restart it if needed.
 *
 * @param time How many ms you want the timer to run 
 */
void start_timer(uint16_t time); 

/** Runs a loop that waits for the timer to be done. 
 * Restarts the timer if needed */
void wait_for_timer(void); 

/** Checks the timer flags and return the status. Restarts the timer if needed.
 * 
 * @return If the clock is done or not
 * @retval true clock is done
 * @retval false clock is not done
 */
uint8_t timer_done (void);    

#endif // SYSTEM_H__
