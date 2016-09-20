nRF24LU1+ SnifferDemo
=====================

The Snifferdemo lets you configure the radio in the nRF24LU1/nRF24LU1+ and relays radio packets received on the radio to a PC application. It can be used to receive transmitted packets from other 2.4 GHz nordic devices as long as the radio is configured with the right settings. 
 
Setup
-----

In this project you will find a MS Visual Studio C# 2008 express example in the \snifferdemo\WinApp folder.  
This example uses the usb device driver LibUsb-Win32 found on http://libusb-win32.sourceforge.net/ and the C# library LibUsbDotNet for easy access of this device driver in .NET. I have included all the required files in the attached file so you do not need to download these libraries but if you plan to distribute any of the files please read the license information at the two web sites.
 
In the attached zip-file you will also find firmware source code for the nRF24LU1+ that the Windows application above connects to via the USB. A pre-compiled hex-file (snifferdemo.hex) can be found in the \nRF24LU1p\build directory. 
 
Program the board with the file snifferdemo.hex and when the “Found new hardware” dialog pops up, select that you want to specify a specific location for the device driver and browse to the \snifferdemo\usbdriver subfolder where you un-zipped the example and click OK. If everything went well you can now run the windows application (SnifferDemo.exe) found in the \WinApp\SnifferDemo\bin\Release directory.
 
To learn how the device is accessed in windows see the file \WinApp\SnifferDemo\Form1.cs. And for the device end the file \ nRF24LU1p \main.c in root folder.

PS: This example is provided as-is, no guarantees to functionality or support. 

Explanation of the radio modes:
-------------------------------

SB:	 Shockburst

Elementary shockburst protocol without auto acknowledge activated. If CRC is also deactivated any package with the right address will go through.

ESB:	 Enhanced Shockburst protocol

Auto acknowledge is activated and the sniffer will signal back to the transmitter when a packet is received. 

ESB DPL: Enhanced Shockburst with dynamic payload length

Use this mode if the transmitter is using the dynamic payload length feature. By entering this mode the payload length value in the application is disregarded, and packets can vary in length. 
This also allows the use of the dynamic ACK and ACK payload features. 
