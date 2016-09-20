/* Copyright (c) 2007 Nordic Semiconductor. All Rights Reserved.
 *
 * The information contained herein is distributed in the hope that
 * it will be useful, but WITHOUT WARRANTY OF ANY KIND.
 *
 * @author Ken A. Redergaard
 * @author Ole Saether
 *
 */
#ifndef USB_DESC_H__
#define USB_DESC_H__

// Standard request codes
#define USB_REQ_GET_STATUS         0x00
#define USB_REQ_CLEAR_FEATURE      0x01
#define USB_REQ_RESERVED_1         0x02
#define USB_REQ_SET_FEATURE        0x03
#define USB_REQ_RESERVED_2         0x04
#define USB_REQ_SET_ADDRESS        0x05
#define USB_REQ_GET_DESCRIPTOR     0x06
#define USB_REQ_SET_DESCRIPTOR     0x07
#define USB_REQ_GET_CONFIGURATION  0x08
#define USB_REQ_SET_CONFIGURATION  0x09
#define USB_REQ_GET_INTERFACE      0x0a
#define USB_REQ_SET_INTERFACE      0x0b
#define USB_REQ_SYNCH_FRAME        0x0c

// Descriptor types
#define USB_DESC_DEVICE           0x01
#define USB_DESC_CONFIGURATION    0x02
#define USB_DESC_STRING           0x03
#define USB_DESC_INTERFACE        0x04
#define USB_DESC_ENDPOINT         0x05
#define USB_DESC_DEVICE_QUAL      0x06
#define USB_DESC_OTHER_SPEED_CONF 0x07
#define USB_DESC_INTERFACE_POWER  0x08
#define USB_DESC_OTG              0x09
#define USB_DESC_DEBUG            0x0A
#define USB_DESC_INTERFACE_ASSOC  0x0B

#define USB_ENDPOINT_TYPE_CONTROL       0x00
#define USB_ENDPOINT_TYPE_ISOCHRONOUS   0x01
#define USB_ENDPOINT_TYPE_BULK          0x02
#define USB_ENDPOINT_TYPE_INTERRUPT     0x03

// USB device classes
#define USB_DEVICE_CLASS_RESERVED        0x00
#define USB_DEVICE_CLASS_AUDIO	         0x01
#define USB_DEVICE_CLASS_COMMUNICATIONS  0x02
#define USB_DEVICE_CLASS_HUMAN_INTERFACE 0x03
#define USB_DEVICE_CLASS_MONITOR         0x04
#define USB_DEVICE_CLASS_PHYSICAL_INTERFACE 0x05
#define USB_DEVICE_CLASS_POWER              0x06
#define USB_DEVICE_CLASS_PRINTER            0x07
#define USB_DEVICE_CLASS_STORAGE            0x08
#define USB_DEVICE_CLASS_HUB                0x09
#define USB_DEVICE_CLASS_APPLICATION_SPECIFIC 0xFE
#define USB_DEVICE_CLASS_VENDOR_SPECIFIC    0xFF


#define USB_CLASS_DESCRIPTOR_HID    0x21
#define USB_CLASS_DESCRIPTOR_REPORT 0x22
#define USB_CLASS_DESCRIPTOR_PHYSICAL_DESCRIPTOR 0x23

#define USB_DEVICE_REMOTE_WAKEUP 0x01
#define USB_ENDPOINT_HALT 0x00
#define USB_TEST_MODE     0x02

typedef struct {
     volatile unsigned char bLength;
     volatile unsigned char bDescriptorType;
     volatile unsigned int bcdUSB;
     volatile unsigned char bDeviceClass;
     volatile unsigned char bDeviceSubClass;
     volatile unsigned char bDeviceProtocol;
     volatile unsigned char bMaxPacketSize0;
     volatile unsigned int idVendor;
     volatile unsigned int idProduct;
     volatile unsigned int bcdDevice;
     volatile unsigned char iManufacturer;
     volatile unsigned char iProduct;
     volatile unsigned char iSerialNumber;
     volatile unsigned char bNumConfigurations;
} usb_dev_desc_t;

typedef struct {
     volatile unsigned char bLength;
     volatile unsigned char bDescriptorType;
     volatile unsigned int wTotalLength;
     volatile unsigned char bNumInterfaces;
     volatile unsigned char bConfigurationValue;
     volatile unsigned char iConfiguration;
     volatile unsigned char bmAttributes;
     volatile unsigned char bMaxPower;
} usb_conf_desc_t;

typedef struct {
     volatile unsigned char bLength;
     volatile unsigned char bDescriptorType;
     volatile unsigned char bInterfaceNumber;
     volatile unsigned char bAlternateSetting;
     volatile unsigned char bNumEndpoints;
     volatile unsigned char bInterfaceClass;
     volatile unsigned char bInterfaceSubClass;
     volatile unsigned char bInterfaceProtocol;
     volatile unsigned char iInterface;
} usb_if_desc_t;

typedef struct {
     volatile unsigned char bLength;
     volatile unsigned char bDescriptorType;
     volatile unsigned char bEndpointAddress;
     volatile unsigned char bmAttributes;
     volatile unsigned int wMaxPacketSize;
     volatile unsigned char bInterval;
} usb_ep_desc_t;
/*
typedef struct {
    volatile unsigned char bLength;
    volatile unsigned char bDescriptorType;
    volatile unsigned int bcdHID;
    volatile unsigned char bCountryCode;
    volatile unsigned char bNumDescriptors;
    volatile unsigned char bDescriptorType2;
    volatile unsigned int wDescriptorLength;
} usb_hid_desc_t;

typedef struct {
     volatile unsigned char* desc;
} usb_string_desc_t;

typedef struct {
     volatile unsigned char bLength;
     volatile unsigned char bDescriptorType;
} usb_common_desc_t;
*/

#endif // USB_DESC_H__
