/* Copyright (c) 2006 Nordic Semiconductor. All Rights Reserved.
 *
 * The information contained herein is confidential property of Nordic Semiconductor. The use,
 * copying, transfer or disclosure of such information is prohibited except by express written
 * agreement with Nordic Semiconductor.
 */

/** @file
 * @defgroup nordic_flash Flash HAL
 * @{
 * @ingroup nordic_hal
 * @brief Interface for self-programming of of
 * on-chip Flash / Non Volatile Data Memory.
 *
 * @author Ole Saether
 */
#ifndef HAL_FLASH_H__
#define HAL_FLASH_H__

#include <stdint.h>
#include "hal_flash_hw.h"

/** Function to erase a page in the Flash memory
 *  @param pn Page number
 */
void hal_flash_page_erase(uint8_t pn);

/** Function to write a byte to the Flash memory
 *  @param a 16 bit address in Flash
 *  @param b byte to write
 */
void hal_flash_byte_write(uint16_t a, uint8_t b);

/** Function to write n bytes to the Flash memory
 *  @param a 16 bit address in Flash
 *  @param *p pointer to bytes to write
 *  @param n number of bytes to write
 */
void hal_flash_bytes_write(uint16_t a, uint8_t *p, uint16_t n);

/** Function to read a byte from the Flash memory
 *  @param a 16 bit address in Flash
 *  @return the byte read
 */
uint8_t hal_flash_byte_read(uint16_t a);

/** Function to read n bytes from the Flash memory
 *  @param a 16 bit address in Flash
 *  @param *p pointer to bytes to write
 *  @param n number of bytes to read
 */
void hal_flash_bytes_read(uint16_t a, uint8_t *p, uint16_t n);

#endif // HAL_FLASH_H__
/** @} */


