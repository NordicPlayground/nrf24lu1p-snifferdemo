/** @file
 *
 * Header file defining hardware dependent functions for nRF24LU1.
 *
 * @addtogroup nordic_hal_nrf
 *
 * @{
 * @defgroup nordic_hal_nrf_hw nRF24L01 HW dependents.
 * @{
 *
 * $Rev: 1731 $
 *
 */

#ifndef _HAL_NRF_LU1_H_
#define _HAL_NRF_LU1_H_

#include <Nordic\reg24lu1.h>

/** Macro that set radio's CSN line LOW.
 *
 */
#define CSN_LOW() do { RFCSN = 0; } while(0)

/** Macro that set radio's CSN line HIGH.
 *
 */
#define CSN_HIGH() do { RFCSN = 1; } while(0)

/** Macro that set radio's CE line LOW.
 *
 */
#define CE_LOW() do { RFCE = 0; } while(0)

/** Macro that set radio's CE line HIGH.
 *
 */
#define CE_HIGH() do { RFCE = 1; } while(0)

/** Macro that set master SPI CSN line LOW.
 *
 */
#define MCSN_LOW() do { MCSN = 0; } while(0)

/** Macro that set master SPI CSN line HIGH.
 *
 */
#define MCSN_HIGH() do { MCSN = 1; } while(0)

/**
 * Pulses the CE to nRF24L01 for at least 10 us
 */
#define CE_PULSE() do { \
  uint8_t count; \
  count = 20; \
  CE_HIGH();  \
  while(count--) \
    ; \
  CE_LOW();  \
  } while(0)

#endif

/** @} */
/** @} */
