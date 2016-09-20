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
 * $LastChangedRevision: 2310 $
 */ 

 /** @file
 * @ingroup Main
 * @ingroup hwdep
 * System level functions: Timers, port setup, interrupt handlers, and other 
 * system functions.
 * 
 * @author Per Kristian Schanke
 */

#include "hal_nrf.h"
#include "system.h"
#include "target_includes.h"

/** Indicates which LED is set to blink. */
volatile uint8_t led_blink;
/** How many times timer 1 should be restarted to be able to run for the
 * wanted amount of time.
 */
static uint8_t timer_rounds;

/** Starts timer 1.
 * 
 * @param time The runtime in timer cycles
 */
static void run_timer (uint16_t time);

void device_boot_msg(void)
{

}

void delay_10ms()
{
  start_timer(10);
  wait_for_timer();
}

void delay_100ms()
{
  start_timer(100);
  wait_for_timer();
}


/** Interrupt handler for timer0.
 * Turns off all blinking LED's. Hardware dependant in setting up
 * timers and interrupt vector.
 */
 
/*void t0_ov_interrupt(void) interrupt INTERRUPT_T0
{
  TR0 = 0;                          // stop timer0
  TL0 = 0xE9;                       // reload timer..
  TH0 = 0xCB;
  P03 = 1;
  led_blink = 0;
}
  */  
void start_timer (uint16_t time)
{
  uint16_t setuptime;
  uint16_t firstruntime;

  firstruntime = (uint16_t)(time % MAX_RUNTIME);

  setuptime = 0 - (firstruntime * CYCLES_PR_MS);
  time -= firstruntime;
  timer_rounds = (uint8_t)(time / MAX_RUNTIME) + 1;

  if (setuptime == 0)
  {
    setuptime = MAX_TIME;
    timer_rounds--;
  }

  run_timer (setuptime);
}

static void run_timer (uint16_t time)
{
  if (time != 0)
  {
    T1_MODE1(); // Setting up mode 1 on timer 1 (16-bit timer) 
    T1_SET_LB((uint8_t)time);
    T1_SET_HB((uint8_t)(time >> 8));
    T1_START();
  }
}

void wait_for_timer (void)
{
  while (timer_rounds > 0)
  {
    while (!TIMER1_OVERFLOW())
      ;

    timer_rounds--;

    if (timer_rounds > 0)
    {
      run_timer (MAX_TIME);
    }
  }

  T1_STOP();
}

uint8_t timer_done (void)
{
  bool retval = false;

  if (TIMER1_OVERFLOW())
  {
    timer_rounds--;

    if (timer_rounds > 0)
    {
      run_timer (MAX_TIME);
    }
    else
    {
      retval = true;
      T1_STOP();
    }
  }

  return retval;
}
   