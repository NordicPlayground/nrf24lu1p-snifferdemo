/* Copyright (c) 2007 Nordic Semiconductor. All Rights Reserved.
 *
 * The information contained herein is distributed in the hope that
 * it will be useful, but WITHOUT WARRANTY OF ANY KIND.
 *
 * @author Ken A. Redergaard
 * @author Ole Saether
 *
 */
#ifndef USB_DESC_TEMPL_H__
#define USB_DESC_TEMPL_H__

#include "usb_desc.h"

#define USB_DESC_TEMPLATE
#define USB_STRING_DESC_COUNT 2

typedef struct {
    usb_conf_desc_t conf;
    usb_if_desc_t if0;
    usb_ep_desc_t ep1in;
    usb_ep_desc_t ep1out;
} usb_conf_desc_templ_t;

extern code usb_conf_desc_templ_t g_usb_conf_desc;
extern code usb_dev_desc_t g_usb_dev_desc;
extern code unsigned char g_usb_string_desc_1[];
extern code unsigned char g_usb_string_desc_2[];
extern code unsigned char string_zero[4];

#endif  // USB_DESC_TEMPL_H__
