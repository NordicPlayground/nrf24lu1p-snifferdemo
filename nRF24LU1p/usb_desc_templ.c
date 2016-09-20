/* Copyright (c) 2007 Nordic Semiconductor. All Rights Reserved.
 *
 * The information contained herein is distributed in the hope that
 * it will be useful, but WITHOUT WARRANTY OF ANY KIND.
 *
 * @author Ken A. Redergaard
 * @author Ole Saether
 *
 */
#include "config.h"

#include "usb_desc_templ.h"

/** Swaps the upper byte with the lower byte in a 16 bit variable */
//lint -emacro((572),SWAP) // Suppress warning 572 "Excessive shift value"
#define SWAP(x) ((((x)&0xFF)<<8)|(((x)>>8)&0xFF))


code usb_dev_desc_t g_usb_dev_desc = {
  sizeof(usb_dev_desc_t), 
  USB_DESC_DEVICE, 
  SWAP(0x0200),       // bcdUSB
  0xff,               // bDeviceClass
  0xff,               // bDeviceSubclass
  0xff,               // bDeviceProtocol
  MAX_PACKET_SIZE_EP0,// bMaxPAcketSize0
  SWAP(6421),         // idVendor - 6421 (0x1915) : Nordic Semiconductor ASA
  SWAP(0xDAFF),       // idProduct
  SWAP(0x0001),       // bcdDevice - Device Release Number (BCD)
  0x01,               // iManufacturer
  0x02,               // iProduct
  0x00,               // iSerialNumber
  0x01                // bNumConfigurations
};

code usb_conf_desc_templ_t g_usb_conf_desc = 
{
    {
        sizeof(usb_conf_desc_t),
        USB_DESC_CONFIGURATION,
        SWAP(sizeof(usb_conf_desc_templ_t)),
        1,            // bNumInterfaces
        1,            // bConfigurationValue
        2,            // iConfiguration
        0xE0,         // bmAttributes - D6: self-powered, bus powered: 0xA0
        50,           // bMaxPower
    },
    /* Interface Descriptor 0 */ 
    {
        sizeof(usb_if_desc_t),
        USB_DESC_INTERFACE,
        0,            // bInterfaceNumber
        0,            // bAlternateSetting
        2,            // bNumEndpoints
        0xff,         // bInterfaceClass
        0x00,         // bInterfaceSubClass  
        0xff,         // bInterfaceProtocol 
        0x00,         // iInterface
    },
     /* Endpoint Descriptor EP1IN */
     {
        sizeof(usb_ep_desc_t),
        USB_DESC_ENDPOINT,
        0x81,                   // bEndpointAddress
        USB_ENDPOINT_TYPE_BULK, // bmAttributes
        SWAP(USB_EP1_SIZE),     // wMaxPacketSize
        0x02                    // bInterval
     },
     /* Endpoint Descriptor EP1OUT */
     {
        sizeof(usb_ep_desc_t),
        USB_DESC_ENDPOINT,
        0x01,                   // bEndpointAddress
        USB_ENDPOINT_TYPE_BULK, // bmAttributes
        SWAP(USB_EP1_SIZE),     // wMaxPacketSize
        0x02                    // bInterval
    },
};

#define USB_STRING_IDX_1_DESC "Nordic Semiconductor"

code unsigned char g_usb_string_desc_1[] = 
{
    sizeof(USB_STRING_IDX_1_DESC) * 2, 0x03,
    'N', 00,
    'o', 00,
    'r', 00,
    'd', 00,
    'i', 00,
    'c', 00,
    ' ', 00,
    'S', 00,
    'e', 00,
    'm', 00,
    'i', 00,
    'c', 00,
    'o', 00,
    'n', 00,
    'd', 00,
    'u', 00,
    'c', 00,
    't', 00,
    'o', 00,
    'r', 00 
};

#define USB_STRING_IDX_2_DESC "Nordic Semiconductor nRF24LU1 SNIFFER DEMO"

code unsigned char g_usb_string_desc_2[] = 
{
    sizeof(USB_STRING_IDX_2_DESC) * 2, 0x03,
    'N', 00,
    'o', 00,
    'r', 00,
    'd', 00,
    'i', 00,
    'c', 00,
    ' ', 00,
    'S', 00,
    'e', 00,
    'm', 00,
    'i', 00,
    'c', 00,
    'o', 00,
    'n', 00,
    'd', 00,
    'u', 00,
    'c', 00,
    't', 00,
    'o', 00,
    'r', 00,
    ' ', 00,
    'n', 00,
    'R', 00,
    'F', 00,
    '2', 00,
    '4', 00,
    'L', 00,
    'U', 00,
    '1', 00,
    ' ', 00,
    'S', 00,
    'N', 00,
    'I', 00,
    'F', 00,
    'F', 00,
    'E', 00,
    'R', 00,
    ' ', 00,
    'D', 00,
    'E', 00,
    'M', 00,
    'O', 00
};

// This is for setting language American English (String descriptor 0 is an array of supported languages):
code unsigned char string_zero[] = {0x04, 0x03, 0x09, 0x04} ;
