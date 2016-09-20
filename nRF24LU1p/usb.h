/* Copyright (c) 2007 Nordic Semiconductor. All Rights Reserved.
 *
 * The information contained herein is distributed in the hope that
 * it will be useful, but WITHOUT WARRANTY OF ANY KIND.
 *
 * @author Ken A. Redergaard
 * @author Ole Saether
 *
 */
#ifndef USB_H__
#define USB_H__

#include "usb_desc_templ.h"

#define USB_EP0_HSNAK() do {ep0cs = 0x02; } while(0)
#define USB_EP0_STALL() do {ep0cs = 0x11; } while(0) // Set both DSTALL and STALL when we want to stall a request during a SETUP transaction

#define INT_SUDAV    0x00
#define INT_SOF      0x04
#define INT_SUTOK    0x08
#define INT_SUSPEND  0x0C
#define INT_USBRESET 0x10
#define INT_EP0IN    0x18
#define INT_EP0OUT   0x1C
#define INT_EP1IN    0x20
#define INT_EP1OUT   0x24
#define INT_EP2IN    0x28
#define INT_EP2OUT   0x2C
#define INT_EP3IN    0x30
#define INT_EP3OUT   0x34
#define INT_EP4IN    0x38
#define INT_EP4OUT   0x3C
#define INT_EP5IN    0x40
#define INT_EP5OUT   0x44

typedef struct {
    unsigned char code * data_ptr;
    unsigned int data_size;
    unsigned int pkt_size;
} packetizer_t;

#define USB_BM_STATE_CONFIGURED           0x01
#define USB_BM_STATE_ALLOW_REMOTE_WAKEUP  0x02
#define USB_BM_STATE_HOST_WU              0x04

/** An enum describing the USB state
 * 
 *  The states described in this enum are found in Chapter 9 of the USB 2.0 specification
 */

typedef enum  { 
    ATTACHED,   /**< Device is attached to the USB, but is not powered */
    POWERED,    /**< Device is attached to the USB and powered */
    DEFAULT,    /**< Device is attached to the USB and powered and has been reset, but has not been assigned a unique address */
    ADDRESSED,  /**< Device is attached to the USB, powered, has been reset, and a unique device address has been assigned. Device is not configured */
    CONFIGURED, /**< Device is attached to the USB, powered, has been reset, has a unique address, is configured and is not suspended */
    SUSPENDED   /**< Device is, at a minimum, attached to the USB and is powered and has not seen bus activity for 3ms. It may also have a unique address and be configured for use. However, because the device is susended, the host may not use the device configuration */
} usb_state_t;

void usb_init(void);
void usb_send_data_ep1(unsigned char bytes_to_send);
void usb_endpoint_config(unsigned char ep_num);
void usb_endpoint_stall(unsigned char ep_num, unsigned char stall);
void usb_ep1_in_out_conf(void);
void usb_irq(void);

#endif  // USB_H__