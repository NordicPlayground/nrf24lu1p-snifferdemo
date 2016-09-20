/* Copyright (c) 2007 Nordic Semiconductor. All Rights Reserved.
 *
 * $LastChangedRevision: 2074 $
 */
/** @file
 * Implementation of the Flash HAL module
 * @author Ole Saether
 */

#include <Nordic\reg24lu1.h>
#include "hal_flash.h"

void hal_flash_page_erase(uint8_t pn)
{
    // Save interrupt enable state and disable interrupts:
    F0 = EA;
    EA = 0;
    // Enable flash write operation:
    FCR = 0xAA;
    FCR = 0x55;
    WEN = 1;
    //
    // Write the page address to FCR to start the page erase operation. This
    // operation is "self timed" when executing from the flash; the CPU will
    // halt until the operation is finished:
    FCR = pn;
    //
    // When running from XDATA RAM we need to wait for the operation to finish:
    while(RDYN == 1)
        ;
    WEN = 0;
    EA = F0; // Restore interrupt enable state
}

void hal_flash_byte_write(uint16_t a, uint8_t b)
{
    uint8_t xdata *data pb;
    
    // Save interrupt enable state and disable interrupts:
    F0 = EA;
    EA = 0;
    // Enable flash write operation:
    FCR = 0xAA;
    FCR = 0x55;
    WEN = 1;
    //
    // Write the byte directly to the flash. This operation is "self timed" when
    // executing from the flash; the CPU will halt until the operation is
    // finished:
    pb = (uint8_t xdata *)a;
    *pb = b;
    //
    // When running from XDATA RAM we need to wait for the operation to finish:
    while(RDYN == 1)
        ;
    WEN = 0;
    EA = F0; // Restore interrupt enable state
}

void hal_flash_bytes_write(uint16_t a, uint8_t *p, uint16_t n)
{
    uint8_t xdata *data pb;

    // Save interrupt enable state and disable interrupts:
    F0 = EA;
    EA = 0;
    // Enable flash write operation:
    FCR = 0xAA;
    FCR = 0x55;
    WEN = 1;
    //
    // Write the bytes directly to the flash. This operation is
    // "self timed"; the CPU will halt until the operation is
    // finished:
    pb = (uint8_t xdata *)a;
    while(n--)
    {
        //lint --e{613} Suppress possible use of null pointer warning:
        *pb++ = *p++;
        //
        // When running from XDATA RAM we need to wait for the operation to
        // finish:
        while(RDYN == 1)
            ;
    }
    WEN = 0;
    EA = F0; // Restore interrupt enable state
}

uint8_t hal_flash_byte_read(uint16_t a)
{
    //lint --e{613} Suppress possible use of null pointer warning:
    uint8_t xdata *pb = (uint8_t xdata *)a;
    return *pb;
}

void hal_flash_bytes_read(uint16_t a, uint8_t *p, uint16_t n)
{  
    uint8_t xdata *pb = (uint8_t xdata *)a;
    while(n--)
    {
        //lint --e{613} Suppress possible use of null pointer warning:
        *p = *pb;
        pb++;
        p++;
    }
}