/* Copyright (c) 2008 Nordic Semiconductor. All Rights Reserved.
 *
 * The information contained herein is confidential property of 
 * Nordic Semiconductor. The use, copying, transfer or disclosure 
 * of such information is prohibited except by express written
 * agreement with Nordic Semiconductor.
 * 
 * SnifferDemo: Application used to register radio communication.
 *
 * Author: Torbjørn Øvrebekk
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

using LibUsbDotNet;
using LibUsbDotNet.Usb;
using LibUsbDotNet.Usb.Info;
using LibUsbDotNet.Usb.Main;
using LibUsbDotNet.DeviceNotify;
using LibUsbDotNet.Usb.Internal;

using Lu1BootLdrTest;

namespace SnifferDemo
{
    enum t_ConnectionType { NOT_CONNECTED, CONNECTED_NORMAL, CONNECTED_BOOTLOADER };
    public partial class Form1 : Form
    {
        private t_ConnectionType isConnected = t_ConnectionType.NOT_CONNECTED;
        private bool radioActivated = false;
        private const int VENDOR_ID = 0x1915;
        private const int PRODUCT_ID = 0xDAFF;
        private const int BULK_OUT_EP = 0x01;
        private const int BULK_IN_EP = 0x81;
        private const int INTERFACE_ID = 0;
        private const float MAX_PACKETDETAILS_UPDATE_FREQ = 20.0f;
        private const int USB_EP1_SIZE = 64;

        private Color c_GuiActiveColor = Color.FromArgb(134, 218, 134);



        private UsbDeviceList mDevList;
        private UsbDevice mDev;
        private UsbEndpointWriter mEpWriter;
        private UsbEndpointReader mEpReader;
        private DeviceNotifier mDevNotifier;

        private RadioConfig m_StartConfig = new RadioConfig();
        DirectoryInfo m_ConfigDataDir = new DirectoryInfo("configdata");
        private byte[] usbCmd_ = new byte[64];
        private byte[] lastInPacket;
        private byte[] transmitPacketBuffer = new byte[33];
        private char[] lcdDisplay = new char[17];
        private string lcdDisp;
        private bool transmitByteHexInputMode = true;
        private int transmitByteCounter = 0;
        private int lastInPacketLength = 0;
        private int maxListLength = 1000;
        private bool m_ListHexDisplayMode = false;
        private int frequencyCounterTimeout = 0;
        private const int maxTimeOutSecs = 3;
        private UInt16 radioPacketFrequencyCounter = 0;
        float radioPacketFrequency = 0.0f;
        private int mouseX = 0, mouseY = 0;

        // BOOTLOADER STUFF
        private bool bIsLU1P_F32_ = true;
        private const int FLASH_PAGE_SIZE = 512;
        private const int NUM_FLASH_BLOCKS = FLASH_PAGE_SIZE / USB_EP1_SIZE;
        private int num_flash_pages_;
        private int flash_size_ = 32 * 1024;
        private const int MIDFLASH_START = 1;
        private int midflash_npages_;
        private byte[] flashBuf_ = null;
        private byte[] usbWrCmd_ = new byte[USB_EP1_SIZE];
        private byte[] usbRdCmd_ = new byte[USB_EP1_SIZE];
        private bool blOptions_SkipFF = true, blOptions_VerifyAfterProgramming = true, blOptions_VerifySkipped = false;
        // BOOTLOADER END

        // Radio control variables
        private string [,] c_ConButtonText = {{"Transmit single packet", ""},
                                              {"Start interval transmission", "Stop interval transmission"}, 
                                              {"Start TX sweep", "Stop TX sweep"}, 
                                              {"Enable data comparator", "Disable data comparator"}};
        private string [] c_ConLabelStarttext = {"Transmits the payload in the payload field once when you press the button", 
                                                 "The payload in the payload field is transmitted automatically at the interval set in the interval field.", 
                                                 "TX sweep sends the payload in the payload field repeatedly and increases the radio channel for every sent packet.", 
                                                 "The data comparator compares incoming RX payloads with the data in the payload field, and counts the number of bit errors between the two.\nThis can be used for simple RX sensitivity testing, by setting up a transmitter to transmit a known payload at regular intervals, and have the Data comparator count any bit errors when receiving the data."};
        private int m_RadioControlMode = 0;
        //  Data comparator stuff (for RX sensitivity testing)
        private bool m_DataComparatorRunning = false;
        private int m_DataComparatorPayloadCounter = 0;
        private int m_DataComparatorBitCounter = 0;
        private int m_DataComparatorBitErrorCounter = 0;
        private bool m_UpdateListCheckboxPrevStatus;

        // Custom radio functionality stuff
        private bool m_StatisticsRunning = false;
        private List<UInt32> m_RandomNumberList = new List<uint>();
        private int m_TagCount = 1000, m_PckLen = 100;
        private int txSweepMode = 1;
        

        delegate void UsbPackRecv(object target, DataReceivedArgs e);
        
        public Form1()
        {
            InitializeComponent();
            InitUsb();
            m_StartConfig.loadData("configdata\\default.dat");
            updateSettingComboBox();
            updateRadioConfigGui(m_StartConfig);

            mDevNotifier = new DeviceNotifier();
            mDevNotifier.OnDeviceNotify += new EventHandler<DeviceNotifyEventArgs>(mDevNotifier_OnDeviceNotify);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listPackages.UseCustomTabOffsets = true;
            for (int i = 0; i < 20; i++) listPackages.CustomTabOffsets.Add(20);
            comboBoxListDisp.SelectedIndex = 0;
            labelControl.Text = c_ConLabelStarttext[0];
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mDev != null)
            {
                usbAllowRadioCom(false);            // The dongle should not send messages on the usb while the software is closed
                mDev.Close();
            }
        }
        private Int16 getSignedByte(Byte a_Byte)
        {
            if (a_Byte > 127)
            {
                return (Int16)((int)(a_Byte) - 256);
            }
            else return a_Byte;
        }
        private void setConnectStatus(t_ConnectionType a_Status)
        {
            isConnected = a_Status;
            //Label1.Text = ((a_Status != t_ConnectionType.NOT_CONNECTED) ? "Con" : "DIScon");
            timerReconnect.Enabled = (a_Status == t_ConnectionType.NOT_CONNECTED);
            if (a_Status == t_ConnectionType.CONNECTED_NORMAL)
            {
                statusUsb.Text = "Dongle: Connected";
                statusUsb.BackColor = Color.GreenYellow;
                statusRadio.Enabled = true;
                if (numericChannel.Enabled == false) guiActivateRadio(false);
                RequestFirmwareVersion();
            }
            else if (a_Status == t_ConnectionType.CONNECTED_BOOTLOADER)
            {
                statusUsb.Text = "Dongle: Bootloader mode";
                statusUsb.BackColor = Color.Yellow;
            }
            else
            {
                statusUsb.Text = "Dongle: Not connected";
                statusUsb.BackColor = Color.Red;
                statusRadio.Enabled = false;
                statusRadio.BackColor = Color.Gray;
                mDev.Close();
            }
        }
        private UsbDevice findDevice(int a_ProductId)
        {
            int namm = 0;
            mDevList = UsbGlobals.DeviceList;
            UsbDevice returnDevice = null;
            foreach (UsbDevice device in mDevList)
                if (device.Info.IdVendor == VENDOR_ID && device.Info.IdProduct == a_ProductId)
                {
                    namm++;
                    returnDevice = device;
                }
            return returnDevice;
        }
        private void InitUsb()
        {
            statusUsb.Text = "Dongle: Attempting to connect";
            UsbDevice tmpDevice = findDevice(PRODUCT_ID);
            if (tmpDevice != null)
            {
                mDev = tmpDevice;
                mDev.Open();

                mDev.SetConfiguration(1);
                mDev.ClaimInterface(0);

                mEpReader = mDev.OpenBulkEndpointReader(ReadEndpoints.Ep01);
                mEpWriter = mDev.OpenBulkEndpointWriter(WriteEndpoints.Ep01);
                mEpReader.DataReceivedEnabled = true;
                mEpReader.DataReceived += new EventHandler<DataReceivedArgs>(UsbPackageReceived);
                setConnectStatus(t_ConnectionType.CONNECTED_NORMAL);
            }
            else
            {
                tmpDevice = findDevice(0x0101);
                if (tmpDevice != null)
                {
                    mDev = tmpDevice;
                    mDev.Open();

                    mDev.SetConfiguration(1);
                    mDev.ClaimInterface(0);

                    mEpReader = mDev.OpenBulkEndpointReader(ReadEndpoints.Ep01);
                    mEpWriter = mDev.OpenBulkEndpointWriter(WriteEndpoints.Ep01);
                    //mEpReader.DataReceivedEnabled = true;
                    //mEpReader.DataReceived += new EventHandler<DataReceivedArgs>(UsbPackageReceived);
                    setConnectStatus(t_ConnectionType.CONNECTED_BOOTLOADER);
                    InitDeviceType();
                }
            }
        }
        private void BootLoader_InitUsb()
        {
            mDevList = UsbGlobals.DeviceList;
            UsbDevice tmpDevice = null;
            foreach (UsbDevice device in mDevList)
                if (device.Info.IdVendor == VENDOR_ID && device.Info.IdProduct == 0x0101)
                    tmpDevice = device;
            if (tmpDevice != null)
            {
                mDev = tmpDevice;
                mDev.Open();

                mDev.SetConfiguration(1);
                mDev.ClaimInterface(0);

                mEpReader = mDev.OpenBulkEndpointReader(ReadEndpoints.Ep01);
                mEpWriter = mDev.OpenBulkEndpointWriter(WriteEndpoints.Ep01);
                mEpReader.DataReceivedEnabled = true;
                mEpReader.DataReceived += new EventHandler<DataReceivedArgs>(UsbPackageReceived);
                setConnectStatus(t_ConnectionType.CONNECTED_BOOTLOADER);
                MessageBox.Show("Bootloader present!!");
            }

            /*usb_.Open(device);
                    usb_.SetConfiguration(1);
                    usb_.ClaimInterface(0);
                    endpoint_ = usb_.CreateEndPoint(BULK_IN_EP, BULK_OUT_EP, 1000, 1000, CEndPoint.eEndPointType.Bulk, USB_EP1_SIZE);
            if (endpoint_ == null)
            {
                throw new SystemException("No nRF24LU1 BOOT LDR devices found");
            }*/
        }
        private void mDevNotifier_OnDeviceNotify(object sender, DeviceNotifyEventArgs e)
        {
            if (e.EventType == EventType.DEVICEARRIVAL)
            {
                findDevice(PRODUCT_ID);
                if( isConnected == t_ConnectionType.NOT_CONNECTED ) InitUsb();
            }
            else
            {
                if( findDevice(PRODUCT_ID) == null ) setConnectStatus(t_ConnectionType.NOT_CONNECTED);
            }
        }

        private void chkLedx_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkBox = (CheckBox)sender;
            usbCmd_[0] = (byte)(0x40 + Convert.ToByte(chkBox.Tag));
            if (chkBox.Checked)
                usbCmd_[1] = 0x01;
            else
                usbCmd_[1] = 0x00;
            mEpWriter.Write(usbCmd_, 0, 2, 100);
        }
        private void RequestFirmwareVersion()
        {
            if (isConnected == t_ConnectionType.CONNECTED_NORMAL)
            {
                usbCmd_[0] = 0x01;
                mEpWriter.Write(usbCmd_, 0, 1, 100);
            }
        }
        private void usbDeactivateRadio()
        {
            if (isConnected == t_ConnectionType.CONNECTED_NORMAL)
            {
                usbCmd_[0] = 0x62;
                mEpWriter.Write(usbCmd_, 0, 1, 100);
            }
        }
        private void usbAllowRadioCom(bool allow)
        {
            if (allow) usbCmd_[0] = 0x68;
            else usbCmd_[0] = 0x69;
            mEpWriter.Write(usbCmd_, 0, 1, 100);
        }
        private void usbActivateRadio()
        {
            if (isConnected == t_ConnectionType.CONNECTED_NORMAL)
            {
                m_StartConfig.prepareUsbPacket(usbCmd_);
                mEpWriter.Write(usbCmd_, 0, (7 + System.Convert.ToInt16(m_StartConfig.AddressLength)), 100);
            }
        }
        private void usbGetRadioSettings()
        {
            if (isConnected == t_ConnectionType.CONNECTED_NORMAL)
            {
                usbCmd_[0] = 0x61;
                mEpWriter.Write(usbCmd_, 0, 1, 100);
            }
        }

        private void AddListItem(bool addToList)
        {
            if (checkBoxUpdateList.Checked)
            {
                if (listPackages.Items.Count >= maxListLength) listPackages.Items.RemoveAt(0);

                if (addToList)
                {
                    listPackages.Items.Add( new DataPacket(lastInPacket, lastInPacketLength));
                    listPackages.SelectedIndex = listPackages.Items.Count - 1;
                }
                labelListSize.Text = "List size: " + listPackages.Items.Count;
            }
        }
        private bool CustomRadioFunctionality()
        {
            // Return true to have the radio packet shown in the list, false otherwise.
            return true;
        }

        private void guiActivateRadio(bool active)
        {
            numericAddress1.Enabled = numericAddress2.Enabled = numericAddress3.Enabled = !active;
            numericAddress4.Enabled = (numericAddressLength.Value > 3) && !active;
            numericAddress5.Enabled = (numericAddressLength.Value > 4) && !active;
            numericAddressLength.Enabled = numericChannel.Enabled = !active;
            numericPayloadLength.Enabled = !active && (m_StartConfig.RadioMode < 2);
            comboBoxBitrate.Enabled = !active;
            comboBoxCrc.Enabled = !active && (m_StartConfig.RadioMode < 3);
            //radioButton1.Enabled = radioButton2.Enabled = radioButton3.Enabled = !active;
            comboBoxMode.Enabled = !active;
            buttonSaveSettings.Enabled = comboBoxSettings.Enabled = !active;
            btnControlStartStop.Enabled = active && (transmitByteCounter > 0 && transmitByteCounter <= 32);
            statusFrequency.Visible = active;
            groupControl.Enabled = active;
            groupConfig.BackColor = (!active ? c_GuiActiveColor : SystemColors.Control);
            groupControl.BackColor = (active ? c_GuiActiveColor : SystemColors.Control);
            if (active)
            {
                //groupConfig.BackColor = Color.LawnGreen;
                buttonActivateRadio.Text = "Deactivate radio";
                statusRadio.Text = "Radio: Activated";
                statusRadio.BackColor = Color.GreenYellow;
                m_StartConfig.readUsbPacket(lastInPacket);
                checkBoxRecvRadioCom.Checked = true;
            }
            else
            {
                //groupConfig.BackColor = SystemColors.Control;
                buttonActivateRadio.Text = "Activate radio";
                statusRadio.Text = "Radio: Deactivated";
                statusRadio.BackColor = Color.Salmon;
            }
            m_StartConfig.Active = active;
        }
        private void runDataComparison()
        {
            int byteDif;
            int packetLength = lastInPacketLength - 5;
            m_DataComparatorPayloadCounter++;
            m_DataComparatorBitCounter += packetLength * 8;

            // Handle the first byte as a special case, to support the Ignore PID function
            byteDif = (int)lastInPacket[2] ^ (int)transmitPacketBuffer[0];
            for (int bit = 0; bit < 8; bit++)
            {
                if (!checkBoxIgnorePid.Checked || bit >= 2)
                {
                    if ((byteDif & 0x00000001) != 0) m_DataComparatorBitErrorCounter++;
                }
                byteDif >>= 1;
            }

            // Handle the remaining bytes as normal. 
            for (int i = 1; i < packetLength; i++)
            {
                byteDif = (int)lastInPacket[i+2] ^ (int)transmitPacketBuffer[i];
                for (int bit = 0; bit < 8; bit++)
                {
                    if ((byteDif & 0x00000001) != 0) m_DataComparatorBitErrorCounter++;
                    byteDif >>= 1;
                }
            }
            labelControl.Text = "Analyzed payloads:\n" + m_DataComparatorPayloadCounter.ToString() + "\nBit error ratio: " + ((double)(m_DataComparatorBitErrorCounter*100) / (double)m_DataComparatorBitCounter).ToString("0.0000") + "%";
        }
        private void ProcessUsbPackage()
        {
            //Console.Write("USB event: " + lastInPacket[0].ToString("X") + "\r\n");
            if (lastInPacketLength > 0)
            {
                switch (lastInPacket[0])
                {
                    case 0xa1:          // Firmware package sent
                        //txtFirmwareVersion.Text = lastInPacket[2].ToString("X") + "." + lastInPacket[3].ToString("X2");
                        //chkLed1.Enabled = chkLed2.Enabled = chkLed3.Enabled = (lastInPacket[4] == 1);
                        if (lastInPacket[5] == 1) usbGetRadioSettings();
                        AddListItem(true);
                        break;
                    case 0xa2:          // Button pressed on basic features board
                        AddListItem(true);
                        break;
                    case 0xb0:
                        String outString = "RX:";
                        for (int i = 0; i < (lastInPacketLength-2); i++)
                            outString += " " + lastInPacket[i+2].ToString("X2");
                        Console.Write(outString + "\r\n");
                        radioPacketFrequencyCounter++;
                        if (CustomRadioFunctionality()) AddListItem(true);
                        break;
                    case 0xb1:          // Radio configuration received
                        guiActivateRadio(true);
                        usbAllowRadioCom(true);
                        AddListItem(true);
                        break;
                    case 0xb2:
                        guiActivateRadio(false);
                        AddListItem(true);
                        break;
                    case 0xb3:
                        AddListItem(true);
                        break;
                    case 0xc0:
                        //Label1.Text = ((int)lastInPacket[2] * 40).ToString();
                        break;
                    case 0xC1:
                        int tmp_var = (int)lastInPacket[lastInPacketLength-3] * 65536 + (int)lastInPacket[lastInPacketLength-2] * 256 + (int)lastInPacket[lastInPacketLength-1];
                        float tmp_frequency = 4000000.0f /3.0f / (float)tmp_var;
                        statusFrequency.Text = "RF frequency: " + tmp_frequency.ToString("0.00") + " Hz";
                        statusFrequency.BackColor = Color.GreenYellow;
                        frequencyCounterTimeout = 0;
                        if (m_DataComparatorRunning) runDataComparison();
                        // Radio packet stuff
                        radioPacketFrequencyCounter++;
                        if (CustomRadioFunctionality()) AddListItem(true);
                        break;
                    case 0xC2:
                        labelDataDump.Text = "ACK Received!!";
                        labelDataDump.Text += "\r\nACK flags: " + lastInPacket[2].ToString("X");
                        break;
                    default:
                        MessageBox.Show("Unknown USB-package received!");
                        break;
                }
                lastInPacketLength = 0;
            }
            else MessageBox.Show("Errorstuff going on!");
        }
        public void UsbPackageReceived(object target, DataReceivedArgs e)
        {
            if (this.listPackages.InvokeRequired)
            {
                UsbPackRecv d = new UsbPackRecv(UsbPackageReceived);
                this.Invoke(d, new object[] { target, e });//, new object[] { text });
            }
            else
            {
                lastInPacket = e.Buffer;
                lastInPacketLength = e.Count;
                ProcessUsbPackage();
            }
        }
        private void listPackages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioPacketFrequency < MAX_PACKETDETAILS_UPDATE_FREQ && listPackages.SelectedIndex >= 0) UpdatePackageDetails((DataPacket)listPackages.Items[listPackages.SelectedIndex]);
        }
        public void UpdatePackageDetails(DataPacket thePck)
        {
            labelPckSize.Text = thePck.DataSize.ToString();
            labelPckCounter.Text = ((int)thePck.PckCtr).ToString();
            switch (thePck.Command)
            {
                case 0xa1:
                    labelPckType.Text = "Firmware version of the dongle";
                    textBoxPckContent.Text = "Firmware version: " + thePck.Data(0).ToString("X") + "." + thePck.Data(1).ToString("X2");
                    textBoxPckContent.Text += "\nDongle type: " + (thePck.Data(2) == 1 ? "nRF24LU1+" : "nRF24LU1");
                    break;
                case 0xa2:
                    labelPckType.Text = "Button update from the BFB";
                    textBoxPckContent.Text = "Button 1: " + ((thePck.Data(0) & 0x04) > 0 ? "On" : "Off") + "\nButton 2: " + ((thePck.Data(0) & 0x02) > 0 ? "On" : "Off") + "\nButton 3: " + ((thePck.Data(0) & 0x01) > 0 ? "On" : "Off");
                    break;
                case 0xb0:
                    labelPckType.Text = "Radio data received on the dongle";
                    textBoxPckContent.Text = "Data: ";
                    for (int i = 0; i < thePck.DataSize; i++)
                        textBoxPckContent.Text += ((int)thePck.Data(i)).ToString() + ", ";
                    textBoxPckContent.Text += "\nBin:";
                    for (int i = 0; i < thePck.DataSize; i++)
                    {
                        textBoxPckContent.Text += " b'";
                        for (int b = 7; b >= 0; b--)
                            textBoxPckContent.Text += (((int)thePck.Data(i) & (1 << b)) == 0 ? "0" : "1");
                    }
                    break;
                case 0xb1:
                    labelPckType.Text = "Confirmation of radio settings";
                    textBoxPckContent.Text = "Channel: \t\t" + ((int)thePck.Data(0)).ToString() + "\nMode:\t\t" + ((int)thePck.Data(1)).ToString();
                    textBoxPckContent.Text += "\nPayloadsize:\t" + ((int)thePck.Data(2)).ToString();
                    textBoxPckContent.Text += "\nCRC:\t\t" + (thePck.Data(3) > 0 ? (thePck.Data(3) == 2 ? "On, 16-bit" : "On, 8-bit") : "Off");
                    textBoxPckContent.Text += "\nBitrate:\t\t" + (thePck.Data(4) == 2 ? "2 MBit" : (thePck.Data(4) == 1 ? "1 MBit" : "250 KBit")) + "\nAddress:\t\t";
                    for (int i = 6; i < thePck.DataSize; i++)
                        textBoxPckContent.Text += "0x" + ((int)thePck.Data(i)).ToString("X2") + (i < (thePck.DataSize - 1) ? ", " : "");
                    break;
                case 0xb2:
                    labelPckType.Text = "Radio deactivated";
                    textBoxPckContent.Text = "";
                    break;
                case 0xb3:
                    labelPckType.Text = "Radio data sent";
                    if( thePck.Data(0) == 2 ) textBoxPckContent.Text = "The packet was sent by the dongle. Whether or not it was received is unknown in shockburst mode.";
                    else textBoxPckContent.Text = (thePck.Data(0)==1 ? "The packet was acknowledged by a receiver." : "The packet was not acknowledged.");
                    break;
                case 0xc0:
                    labelPckType.Text = "Radio data received on the dongle";
                    textBoxPckContent.Text = "Data: ";
                    for (int i = 0; i < thePck.DataSize; i++)
                        textBoxPckContent.Text += ((int)thePck.Data(i)).ToString() + ", ";
                    textBoxPckContent.Text += "\nBin:";
                    for (int i = 0; i < thePck.DataSize; i++)
                    {
                        textBoxPckContent.Text += " b'";
                        for (int b = 7; b >= 0; b--)
                            textBoxPckContent.Text += (((int)thePck.Data(i) & (1 << b)) == 0 ? "0" : "1");
                    }
                    break;
                case 0xc1:
                    labelPckType.Text = "Radio data received on the dongle";
                    textBoxPckContent.Text = "Data: ";
                    for (int i = 3; i < thePck.DataSize; i++)
                        textBoxPckContent.Text += ((int)thePck.Data(i-3)).ToString() + ", ";
                    textBoxPckContent.Text += "\nBin:";
                    for (int i = 3; i < thePck.DataSize; i++)
                    {
                        textBoxPckContent.Text += " b'";
                        for (int b = 7; b >= 0; b--)
                            textBoxPckContent.Text += (((int)thePck.Data(i-3) & (1 << b)) == 0 ? "0" : "1");
                    }
                    break;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RequestFirmwareVersion();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (numericChannel.Enabled)
                usbActivateRadio();
            else
                usbDeactivateRadio();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listPackages.Items.Clear();
            labelListSize.Text = "Received packets: 0";
            labelPckCounter.Text = labelPckSize.Text = labelPckType.Text = textBoxPckContent.Text = "";
        }

        private void timerPckFreq_Tick(object sender, EventArgs e)
        {
            /*float oldRadioPacketFrequency = radioPacketFrequency;
            radioPacketFrequency = ((float)radioPacketFrequencyCounter / (float)timerPckFreq.Interval * 1000.0f);
            if (radioPacketFrequency > 0.0f) statusFrequency.BackColor = Color.GreenYellow;
            else statusFrequency.BackColor = Color.Pink;
            statusFrequency.Text = "RF frequency: " + radioPacketFrequency.ToString() + " Hz";
            if (oldRadioPacketFrequency > MAX_PACKETDETAILS_UPDATE_FREQ && radioPacketFrequency < MAX_PACKETDETAILS_UPDATE_FREQ) listPackages_SelectedIndexChanged(null, null);
            radioPacketFrequencyCounter = 0;*/
            frequencyCounterTimeout++;
            if (frequencyCounterTimeout > 2)
            {
                statusFrequency.Text = "RF frequency: 0.0 Hz";
                statusFrequency.BackColor = Color.Pink;
            }
        }
        private void numericAddressLength_ValueChanged(object sender, EventArgs e)
        {
            numericAddress4.Enabled = numericAddressLength.Value > 3;
            numericAddress5.Enabled = numericAddressLength.Value > 4;
        }

        private void buttonSaveFile_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }
        private void checkBoxRecvRadioCom_CheckedChanged(object sender, EventArgs e)
        {
            usbAllowRadioCom(checkBoxRecvRadioCom.Checked);
            checkBoxUpdateList.Enabled = checkBoxRecvRadioCom.Checked;
        }
        private void checkBoxUpdateList_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void timerReconnect_Tick(object sender, EventArgs e)
        {
            InitUsb();
        }

        private void saveFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string tmpString;
            System.IO.Stream fileStream = saveFileDialog1.OpenFile();
            for (int i = 0; i < listPackages.Items.Count; i++)
            {
                tmpString = listPackages.Items[i].ToString();
                for (int j = 0; j < tmpString.Length; j++) fileStream.WriteByte((byte)tmpString[j]);
                fileStream.WriteByte((byte)'\n');
            }
            fileStream.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("Sniffer Demo beta\nCopyright 2008 Nordic Semiconductor\nAuthor: Torbjørn Øvrebekk", "About");
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
        private void updateRadioConfigGui(RadioConfig a_TheConfig)
        {
            numericChannel.Value = a_TheConfig.Channel;
            numericPayloadLength.Value = a_TheConfig.PayloadLength;
            numericAddressLength.Value = a_TheConfig.AddressLength;
            numericAddress1.Value = a_TheConfig[0];
            numericAddress2.Value = a_TheConfig[1];
            numericAddress3.Value = a_TheConfig[2];
            numericAddress4.Value = a_TheConfig[3];
            numericAddress5.Value = a_TheConfig[4];
            comboBoxCrc.SelectedIndex = a_TheConfig.CRC;
            comboBoxBitrate.SelectedIndex = a_TheConfig.BitRate;
            //switch (a_TheConfig.RadioMode) { case 0: radioButton1.Checked = true; break; case 1: radioButton2.Checked = true; break; case 2: radioButton3.Checked = true; break; }
            comboBoxMode.SelectedIndex = a_TheConfig.RadioMode;
            numericPayloadLength.Enabled = (comboBoxMode.SelectedIndex < 2);
            comboBoxCrc.Enabled = (comboBoxMode.SelectedIndex < 3);
        }
        private void radioGuiChanged(object sender, EventArgs e)
        {
            if (true)
            {
                m_StartConfig.Channel = (int)numericChannel.Value;
                m_StartConfig.PayloadLength = (int)numericPayloadLength.Value;
                m_StartConfig.AddressLength = (int)numericAddressLength.Value;
                m_StartConfig[0] = (byte)numericAddress1.Value;
                m_StartConfig[1] = (byte)numericAddress2.Value;
                m_StartConfig[2] = (byte)numericAddress3.Value;
                m_StartConfig[3] = (byte)numericAddress4.Value;
                m_StartConfig[4] = (byte)numericAddress5.Value;
                m_StartConfig.CRC = comboBoxCrc.SelectedIndex;
                m_StartConfig.BitRate = comboBoxBitrate.SelectedIndex;
                m_StartConfig.RadioMode = comboBoxMode.SelectedIndex;
                numericPayloadLength.Enabled = (comboBoxMode.SelectedIndex < 2);
                comboBoxCrc.Enabled = (comboBoxMode.SelectedIndex < 3);
                /*if (radioButton1.Checked) m_StartConfig.RadioMode = 0;
                else if (radioButton2.Checked) m_StartConfig.RadioMode = 1;
                else m_StartConfig.RadioMode = 2;*/
            }
        }
        private void updateSettingComboBox()
        {
            FileInfo[] files = m_ConfigDataDir.GetFiles();
            comboBoxSettings.Items.Clear();
            comboBoxSettings.Items.Add("default");
            foreach (FileInfo file in files)
            {
                if (file.Extension == ".dat" && file.Name != "default.dat") comboBoxSettings.Items.Add(file.Name.Substring(0, file.Name.Length-4));
            }
            comboBoxSettings.SelectedIndex = 0;
        }

        private void buttonSaveSettings_Click(object sender, EventArgs e)
        {
            saveFileDialog2.ShowDialog();
            
        }
        private void saveFileDialog2_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            m_StartConfig.saveData(saveFileDialog2.FileName);
            int tmp1 = saveFileDialog2.FileName.LastIndexOf('\\')+1;
            int tmp2 = saveFileDialog2.FileName.LastIndexOf('.');
            string tmpString = saveFileDialog2.FileName.Substring(tmp1, tmp2 - tmp1);
            updateSettingComboBox();
            for (int i = 1; i < comboBoxSettings.Items.Count; i++)
                if (comboBoxSettings.Items[i].ToString() == tmpString)
                    comboBoxSettings.SelectedIndex = i;

        }

        private void comboBoxSettings_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_StartConfig.loadData(m_ConfigDataDir.FullName + "\\" + comboBoxSettings.Items[comboBoxSettings.SelectedIndex].ToString() + ".dat");
            updateRadioConfigGui(m_StartConfig);
        }
        private void buttonFirmware_Click(object sender, EventArgs e)
        {
            usbCmd_[0] = 0x70;          // Enter bootloader mode
            mEpWriter.Write(usbCmd_, 0, 1, 100);
            setConnectStatus(t_ConnectionType.NOT_CONNECTED);
        }

        private void InitDeviceType()
        {
            usbWrCmd_[0] = (byte)USB_COMMANDS.CMD_FIRMWARE_VERSION;
            mEpWriter.Write(usbWrCmd_, 0, 1, 100);
            mEpReader.Read(usbRdCmd_, 0, 2, 100);
            if ((usbRdCmd_[1] & 0x01) == 0x01)
            {
                bIsLU1P_F32_ = true;
                flash_size_ = 32 * 1024;
            }
            else
            {
                bIsLU1P_F32_ = false;
                flash_size_ = 16 * 1024;
            }
            num_flash_pages_ = flash_size_ / FLASH_PAGE_SIZE;
            // Number of pages between MIDFLASH_START and the bootloader is NUM_FLASH_PAGES - 5:
            midflash_npages_ = num_flash_pages_ - 5;
            flashBuf_ = new byte[flash_size_];
        }
        private void FlashPageProgram(byte[] pageBuf, int nPage)
        {
            usbWrCmd_[0] = (byte)USB_COMMANDS.CMD_FLASH_WRITE_INIT;
            usbWrCmd_[1] = (byte)nPage;
            mEpWriter.Write(usbWrCmd_, 0, 2, 100);
            mEpReader.Read(usbRdCmd_, 0, 1, 100);
            for (int i = 0; i < NUM_FLASH_BLOCKS; i++)
            {
                mEpWriter.Write(pageBuf, i * USB_EP1_SIZE, USB_EP1_SIZE, 100);
                mEpReader.Read(usbRdCmd_, 0, 1, 100);
            }
        }
        private TimeSpan FlashProgram(int nStartPage, int nPages)
        {
            statusUsb.Text = "Dongle: Programming flash...";
            Update();
            DateTime t1 = DateTime.Now;
            for (int i = nStartPage; i < (nStartPage + nPages); i++)
            {
                if (!(blOptions_SkipFF && Utils.IsFlashBufEmpty(flashBuf_, (uint)(i * FLASH_PAGE_SIZE), FLASH_PAGE_SIZE)))
                {
                    byte[] pageBuf = new byte[FLASH_PAGE_SIZE];
                    Array.Copy(flashBuf_, i * FLASH_PAGE_SIZE, pageBuf, 0, FLASH_PAGE_SIZE);
                    FlashPageProgram(pageBuf, i);
                }
                //progressBar1.Value++;
            }
            DateTime t2 = DateTime.Now;
            //progressBar1.Value = 0;
            return new TimeSpan(t2.Day - t1.Day, t2.Hour - t1.Hour, t2.Minute - t1.Minute, t2.Second - t1.Second, t2.Millisecond - t1.Millisecond);
        }
        private void FlashPageVerify(byte[] pageBuf, int nPage)
        {
            for (int i = 0; i < NUM_FLASH_BLOCKS; i++)
            {
                
                uint nBlock = (uint)(nPage * NUM_FLASH_BLOCKS + i);
                if (bIsLU1P_F32_)
                {
                    usbWrCmd_[0] = (byte)USB_COMMANDS.CMD_FLASH_SELECT_HALF;
                    usbWrCmd_[1] = (byte)(nBlock >> 8);
                    mEpWriter.Write(usbWrCmd_, 0, 2, 100);
                    mEpReader.Read(usbRdCmd_, 0, 1, 100);
                }
                
                usbWrCmd_[0] = (byte)USB_COMMANDS.CMD_FLASH_READ;
                usbWrCmd_[1] = (byte)nBlock;
                mEpWriter.Write(usbWrCmd_, 0, 2, 100);
                mEpReader.Read(usbRdCmd_, 0, USB_EP1_SIZE, 100);
                for (int n = 0; n < USB_EP1_SIZE; n++)
                {
                    if (usbRdCmd_[n] != pageBuf[i * USB_EP1_SIZE + n])
                    {
                        int failAddr = nPage * FLASH_PAGE_SIZE + i * USB_EP1_SIZE + n;
                        byte failByte = usbRdCmd_[n];
                        string msg = "The Flash contents does not match the file contents\nAddress = ";
                        msg += "0x" + failAddr.ToString("X4") + "\nExpected 0x" + flashBuf_[failAddr].ToString("X2") + ", got 0x" + failByte.ToString("X2");
                        throw new SystemException(msg);
                    }
                }
            }
        }
        private void FlashVerify(int nStartPage, int nPages)
        {
            statusUsb.Text = "Dongle: Verifying flash...";
            //Update();
            //DateTime t1 = DateTime.Now;
            for (int i = nStartPage; i < (nStartPage + nPages); i++)
            {
                if (!(blOptions_SkipFF && !blOptions_VerifySkipped && Utils.IsFlashBufEmpty(flashBuf_, (uint)(i * FLASH_PAGE_SIZE), FLASH_PAGE_SIZE)))
                {
                    byte[] pageBuf = new byte[FLASH_PAGE_SIZE];
                    Array.Copy(flashBuf_, i * FLASH_PAGE_SIZE, pageBuf, 0, FLASH_PAGE_SIZE);
                    FlashPageVerify(pageBuf, i);
                }
                //progressBar1.Value++;
            }
            //DateTime t2 = DateTime.Now;
            //return null;// new TimeSpan(t2.Day - t1.Day, t2.Hour - t1.Hour, t2.Minute - t1.Minute, t2.Second - t1.Second, t2.Millisecond - t1.Millisecond);
        }  
        private void button2_Click_2(object sender, EventArgs e)
        {
            try
            {
                uint highAddr = 0;
                Utils.FillByteBuf(flashBuf_, 0xFF);
                HexFile hexFile = new HexFile("C:\\Keil\\C51\\Examples\\Nordic\\nRF24LU1\\snifferdemo\\Firmware\\build\\snifferdemo.hex");
                hexFile.Read(flashBuf_, 0, ref highAddr);
                //
                // First program and verify the flash pages above page 0 and below the bootloader
                // (last four pages of the flash). The TimeSpan stuff is used for showing how
                // long the programming takes to the user:
                FlashProgram(MIDFLASH_START, midflash_npages_);
                if (blOptions_VerifyAfterProgramming)
                {
                    FlashVerify(MIDFLASH_START, midflash_npages_);
                }
                //
                // Then program page 0 and the pages containing the bootloader:
                FlashProgram(0, MIDFLASH_START);
                if (highAddr > (midflash_npages_ + MIDFLASH_START) * FLASH_PAGE_SIZE)
                    FlashProgram(midflash_npages_ + MIDFLASH_START, num_flash_pages_ - midflash_npages_ - MIDFLASH_START);
                if (blOptions_VerifyAfterProgramming)
                {
                    FlashVerify(0, MIDFLASH_START);
                    if (highAddr > (midflash_npages_ + MIDFLASH_START) * FLASH_PAGE_SIZE)
                        FlashVerify(midflash_npages_ + MIDFLASH_START, num_flash_pages_ - midflash_npages_ - MIDFLASH_START);
                }
                /*if (!ProgramRDISMB())
                {
                    throw new SystemException("There was an error turning on readback disable of flash");
                }*/

                // Reset
                usbWrCmd_[0] = (byte)USB_COMMANDS.CMD_TEMP_RESET;
                mEpWriter.Write(usbWrCmd_, 0, 1, 100);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Program Flash", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        private void analyzeTransmitText()
        {
            bool textValid;
            transmitByteCounter = 0;

            int mode = 0, currentNumber = 0;
            for (int i = 0; i < textBoxTransmit.Text.Length; i++)
            {
                int currentByte = textBoxTransmit.Text[i];
                if (mode == 0) mode = (transmitByteHexInputMode ? 1 : 2);
                switch (mode)
                {
                    case 0:
                        if (currentByte == 'h' || currentByte == 'H')
                            mode = 1;//HEXMODE
                        else if (currentByte >= '0' && currentByte <= '9')
                        {
                            currentNumber = (currentByte - 48);
                            mode = 2;//NORMALMODE
                        }
                        break;
                    case 1://HEX
                        currentNumber *= 16;
                        if (currentByte >= '0' && currentByte <= '9')
                            currentNumber += (currentByte - 48);
                        else if (currentByte >= 'a' && currentByte <= 'f')
                            currentNumber += (currentByte + 10 - 'a');
                        else if (currentByte >= 'A' && currentByte <= 'F')
                            currentNumber += (currentByte + 10 - 'A');
                        else
                        {
                            currentNumber /= 16;
                            mode = 3;
                            i--;
                        }
                        break;
                    case 2://NORMAL
                        if (currentByte >= '0' && currentByte <= '9')
                        {
                            currentNumber *= 10;
                            currentNumber += (currentByte - 48);
                        }
                        else
                        {
                            mode = 3;
                            i--;
                        }
                        break;
                    case 3:
                        if (currentByte == ',' || currentByte == ' ')
                        {
                            if (currentNumber > 255)
                            {
                                textValid = false;
                                currentNumber = 255;
                            }
                            transmitPacketBuffer[transmitByteCounter++] = (byte)currentNumber;
                            currentNumber = 0;
                            mode = 0;
                        }
                        break;
                }
                if (transmitByteCounter > 32) return;
            }
            if (currentNumber > 0 && currentNumber < 256) transmitPacketBuffer[transmitByteCounter++] = (byte)currentNumber;
            labelControl.Text = "Payload length: " + transmitByteCounter.ToString() + "\n";
            labelControl.Text += "Payload (decimal): ";
            for (int i = 0; i < transmitByteCounter; i++)
                labelControl.Text += transmitPacketBuffer[i].ToString() + " ";
            labelControl.Text += "\nPayload (HEX): ";
            for (int i = 0; i < transmitByteCounter; i++)
                labelControl.Text += "0x" + transmitPacketBuffer[i].ToString("X2") + " ";
            btnControlStartStop.Enabled = (transmitByteCounter > 0 && transmitByteCounter <= 32);
        }
        private void textBoxTransmit_TextChanged(object sender, EventArgs e)
        {
            analyzeTransmitText();
        }

        private void buttonTransmit_Click(object sender, EventArgs e)
        {
            switch (m_RadioControlMode)
            {
                case 0:
                    if (transmitByteCounter > 0)
                    {
                        usbCmd_[0] = 0x90; // Transmit data
                        usbCmd_[1] = (byte)transmitByteCounter;
                        for (int i = 0; i < transmitByteCounter; i++)
                            usbCmd_[i + 2] = transmitPacketBuffer[i];
                        mEpWriter.Write(usbCmd_, 0, transmitByteCounter + 2, 100);
                    }
                    break;
                case 1:
                    numericIntTime.Enabled = timerIntervalTransmit.Enabled;
                    if (!timerIntervalTransmit.Enabled) timerIntervalTransmit.Interval = (int)numericIntTime.Value;
                    btnControlStartStop.Text = c_ConButtonText[1, (numericIntTime.Enabled ? 0 : 1)];
                    timerIntervalTransmit.Enabled = !timerIntervalTransmit.Enabled;
                    textBoxTransmit.Enabled = radioControl0.Enabled = radioControl1.Enabled = radioControl2.Enabled = radioControl3.Enabled = !timerIntervalTransmit.Enabled;
                    break;
                case 2:
                    if (!txSweep.Enabled) txSweepMode = 1;
                    txSweep.Enabled = !txSweep.Enabled;
                    btnControlStartStop.Text = c_ConButtonText[2, (txSweep.Enabled ? 1 : 0)];
                    textBoxTransmit.Enabled = radioControl0.Enabled = radioControl1.Enabled = radioControl2.Enabled = radioControl3.Enabled = !txSweep.Enabled;
                    break;
                case 3:
                    if (!m_DataComparatorRunning)
                    {
                        m_UpdateListCheckboxPrevStatus = checkBoxUpdateList.Checked;
                        checkBoxUpdateList.Checked = false;
                        labelControl.BackColor = c_GuiActiveColor;
                        labelControl.Text = "Analyzed payloads:\n0\nBit error ratio: 0.0000%";
                        m_DataComparatorBitCounter = m_DataComparatorBitErrorCounter = m_DataComparatorPayloadCounter = 0;
                        m_DataComparatorRunning = true;
                    }
                    else
                    {
                        checkBoxUpdateList.Checked = m_UpdateListCheckboxPrevStatus;
                        labelControl.BackColor = SystemColors.Control;
                        m_DataComparatorRunning = false;
                    }
                    btnControlStartStop.Text = c_ConButtonText[3, (m_DataComparatorRunning ? 1 : 0)];
                    textBoxTransmit.Enabled = radioControl0.Enabled = radioControl1.Enabled = radioControl2.Enabled = radioControl3.Enabled = !m_DataComparatorRunning;
                    break;
            }
        }

        private void timerIntervalTransmit_Tick(object sender, EventArgs e)
        {
            if (transmitByteCounter > 0)
            {
                usbCmd_[0] = 0x90; // Transmit data
                usbCmd_[1] = (byte)transmitByteCounter;
                for (int i = 0; i < transmitByteCounter; i++)
                    usbCmd_[i + 2] = transmitPacketBuffer[i];
                mEpWriter.Write(usbCmd_, 0, transmitByteCounter + 2, 100);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonResetDongle_Click(object sender, EventArgs e)
        {
            usbCmd_[0] = 0x71;  // Reset dongle
            mEpWriter.Write(usbCmd_, 0, 1, 100);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void buttonClearList_Click(object sender, EventArgs e)
        {
            listPackages.Items.Clear();
            labelListSize.Text = "List size: 0"; 
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            MessageBox.Show("Sniffer Demo v1.0\nNordic Semiconductor 2010\nAuthor: Torbjørn Øvrebekk\n", "About");
        }

        private void buttonSaveFile_Click_1(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void txSweep_Tick(object sender, EventArgs e)
        {
            switch (txSweepMode)
            {
                case 0:
                    //listPackages.Items.Add("0");
                    button2_Click(this, null);
                    break;
                case 1:
                    //listPackages.Items.Add("1");
                    usbCmd_[0] = 0x90; // Transmit data
                    usbCmd_[1] = (byte)transmitByteCounter;
                    for (int i = 0; i < transmitByteCounter; i++)
                        usbCmd_[i + 2] = transmitPacketBuffer[i];
                    mEpWriter.Write(usbCmd_, 0, transmitByteCounter + 2, 100);
                    break;
                case 2:
                    //listPackages.Items.Add("2");
                    button2_Click(this, null);
                    break;
                case 3:
                    //listPackages.Items.Add("3");
                    if (numericChannel.Value < numericChannel.Maximum) numericChannel.Value++;
                    else numericChannel.Value = numericChannel.Minimum;
                    radioGuiChanged(this, null);
                    //updateRadioConfigGui(m_StartConfig);
                    break;
            }
            txSweepMode++;
            if (txSweepMode > 3) txSweepMode = 0;
        }

        private void sendDataDump(int offset, int length, bool invert)
        {
            byte[] usbCmd_ = new byte[length + 3];
            if (isConnected == t_ConnectionType.CONNECTED_NORMAL)
            {
                usbCmd_[0] = 0xA0;
                if(offset != 0xFF)
                {
                    usbCmd_[1] = (byte)(offset & 0xFF);
                    usbCmd_[2] = (byte)(length & 0xFF);
                    if(invert)
                    {
                        for (int i = 0; i < length; i++) usbCmd_[3 + i] = (byte)((255 - offset - i) & 0xFF);
                    }
                    else
                    {
                        for (int i = 0; i < length; i++) usbCmd_[3 + i] = (byte)((offset + i) & 0xFF);
                    }
                    mEpWriter.Write(usbCmd_, 0, length + 3, 100);

                }
                else
                {
                    usbCmd_[1] = 0xFF;
                    usbCmd_[2] = 0xFF;
                    mEpWriter.Write(usbCmd_, 0, 3, 100);
                }
            }
            Thread.Sleep(5);
        }

        private void buttonSendDataDump_Click(object sender, EventArgs e)
        {
            sendDataDump(0, 50, false);
            sendDataDump(50, 50, false);
            sendDataDump(100, 50, false);
            //sendDataDump(75, 25);
            //sendDataDump(100, 25);
            //sendDataDump(125, 25);
            sendDataDump(0xFF, 0xFF, false);
            labelDataDump.Text = "Data dump message sent. Waiting for response....";
        }

        private void buttonDataDump2_Click(object sender, EventArgs e)
        {
            sendDataDump(0, 50, true);
            sendDataDump(50, 50, true);
            sendDataDump(100, 50, true);
            //sendDataDump(75, 25);
            //sendDataDump(100, 25);
            //sendDataDump(125, 25);
            sendDataDump(0xFF, 0xFF, true);
            labelDataDump.Text = "Data dump message 2 sent. Waiting for response....";
        }

        private void btnComparator_Click(object sender, EventArgs e)
        {

        }

        private void radioControls_CheckedChanged(object sender, EventArgs e)
        {
            m_RadioControlMode = Convert.ToInt32(((RadioButton)sender).Tag);
            if (((RadioButton)sender).Checked)
            {
                // Enter mode
                switch (m_RadioControlMode)
                {
                    case 0:
                        break;

                    case 1:
                        numericIntTime.Enabled = true;
                        break;

                    case 2:
                        break;

                    case 3:
                        checkBoxIgnorePid.Enabled = true;
                        break;
                }
            }
            else
            {
                // Leave mode
                switch (m_RadioControlMode)
                {
                    case 0:
                        break;

                    case 1:
                        numericIntTime.Enabled = false;
                        break;

                    case 2:
                        break;

                    case 3:
                        checkBoxIgnorePid.Enabled = false;
                        break;
                }
            }
            labelControl.Text = c_ConLabelStarttext[m_RadioControlMode];
            btnControlStartStop.Text = c_ConButtonText[m_RadioControlMode, 0];
        }

        private void labelTransmit_Click(object sender, EventArgs e)
        {
            transmitByteHexInputMode = !transmitByteHexInputMode;
            if (transmitByteHexInputMode) labelTransmit.Text = "HEX";
            else labelTransmit.Text = "DEC";
            analyzeTransmitText();
        }

        private void comboBoxListDisp_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_ListHexDisplayMode = (comboBoxListDisp.SelectedIndex == 1);  
        }

    }
    public class RadioConfig
    {
        byte[] m_Address;
        int m_AddressLength;
        int m_Channel;
        int m_PayloadLength;
        int m_CRC, m_BitRate, m_RadioMode;
        bool m_RadioActive;
        public RadioConfig()
        {
            m_Address = new byte[5]{0x22,0x33,0x44,0x55,0x01};
            m_AddressLength = 5;
            m_Channel = 2;
            m_PayloadLength = 1;
            m_CRC = 2;
            m_BitRate = 1;
            m_RadioMode = 2;
            m_RadioActive = false;
        }
        public int AddressLength
        {
            set { if (value >= 3 && value <= 5)m_AddressLength = value; else m_AddressLength = 5; }
            get { return m_AddressLength; }
        }
        public byte this[int index]
        {
            set { if (index >= 0 && index < 5)m_Address[index] = value; }
            get { if (index >= 0 && index < 5) return m_Address[index]; else return 0; }
        }
        public int Channel
        {
            set { if (value >= 0 && value < 126)m_Channel = value; else m_Channel = 0; }
            get { return m_Channel; }
        }
        public int PayloadLength
        {
            set { if (value >= 1 && value <= 32)m_PayloadLength = value; else m_PayloadLength = 1; }
            get { return m_PayloadLength; }
        }
        public int CRC
        {
            set { if (value >= 0 && value <= 2)m_CRC = value; else m_CRC = 2; }
            get { return m_CRC; }
        }
        public int BitRate
        {
            set { if (value >= 0 && value <= 2)m_BitRate = value; else m_BitRate = 2; }
            get { return m_BitRate; }
        }
        public int RadioMode
        {
            set { if (value >= 0 && value <= 3)m_RadioMode = value; else m_RadioMode = 2; }
            get { return m_RadioMode; }
        }
        public bool Active
        {
            set { m_RadioActive = value; }
            get { return m_RadioActive; }
        }
        public void prepareUsbPacket(byte[] usbBuffer)
        {
            usbBuffer[0] = 0x60;
            usbBuffer[1] = (byte)m_Channel;
            usbBuffer[2] = (byte)m_RadioMode;
            usbBuffer[3] = (byte)m_PayloadLength;
            usbBuffer[4] = (byte)m_CRC;
            usbBuffer[5] = (byte)(m_BitRate);
            usbBuffer[6] = (byte)m_AddressLength;
            for (int i = 0; i < m_AddressLength; i++)
                usbBuffer[7 + i] = m_Address[i];
        }
        public void readUsbPacket(byte[] usbBuffer)
        {
            m_Channel = usbBuffer[2];
            m_RadioMode = usbBuffer[3];
            m_PayloadLength = usbBuffer[4];
            m_CRC = usbBuffer[5];
            m_BitRate = usbBuffer[6];
            m_AddressLength = usbBuffer[7];
            for( int i = 0; i < m_AddressLength; i++ )
                m_Address[i] = usbBuffer[8+i];
        }
        public void saveData(string fileName)
        {
            Stream stream = null;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                for( int i = 0; i < 5; i++ )
                    formatter.Serialize(stream, m_Address[i]);
                formatter.Serialize(stream, m_AddressLength);
                formatter.Serialize(stream, m_Channel);
                formatter.Serialize(stream, m_PayloadLength);
                formatter.Serialize(stream, m_CRC);
                formatter.Serialize(stream, m_BitRate);
                formatter.Serialize(stream, m_RadioMode);
            }
            catch
            {
                // do nothing, just ignore any possible errors
            }
            finally
            {
                if (null != stream)
                    stream.Close();
            }
        }
        public void loadData(string fileName)
        {
            Stream stream = null;
            try
            {
                IFormatter formatter = new BinaryFormatter();
                stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                for (int i = 0; i < 5; i++)
                     m_Address[i] = (byte)formatter.Deserialize(stream);
                m_AddressLength = (int)formatter.Deserialize(stream);
                m_Channel = (int)formatter.Deserialize(stream);
                m_PayloadLength = (int)formatter.Deserialize(stream);
                m_CRC = (int)formatter.Deserialize(stream);
                m_BitRate = (int)formatter.Deserialize(stream);
                m_RadioMode = (int)formatter.Deserialize(stream);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());
                // do nothing, just ignore any possible errors
            }
            finally
            {
                if (null != stream)
                    stream.Close();
            }
        }
    }


    public class DataPacket
    {
        byte[] m_Data;
        int m_DataSize;
        public DataPacket(byte[] data, int size)
        {
            m_Data = new byte[size];
            for (int i = 0; i < size; i++) m_Data[i] = data[i];
            m_DataSize = size;
        }
        public byte Command
        {
            get { return m_Data[0]; }
        }
        public byte PckCtr
        {
            get { return m_Data[1]; }
        }
        public int DataSize
        {
            get { return m_DataSize - 2; }
        }
        public byte Data(int a)
        {
            if (a >= 0 && a < m_DataSize - 2)
                return m_Data[a + 2];
            else return 0;
        }

        public override string ToString()
        {
            string returnValue = "";
            switch (m_Data[0])
            {
                case 0xa1:
                    returnValue = "Firmware version: " + m_Data[2].ToString("X") + "." + m_Data[3].ToString("X2");
                    returnValue += " - " + (m_Data[4] == 1 ? "nRF24LU1+" : "nRF24LU1");
                    break;
                case 0xa2:
                    returnValue = "BFB-Buttons: ";
                    returnValue += ((m_Data[2] & 0x04) > 0 ? "1" : "0");
                    returnValue += ((m_Data[2] & 0x02) > 0 ? "1" : "0");
                    returnValue += ((m_Data[2] & 0x01) > 0 ? "1" : "0");
                    break;
                case 0xb0:
                    returnValue = "RX:";
                    for (int i = 0; i < (m_DataSize - 2); i++)
                        returnValue += "\t" + m_Data[i + 2].ToString("X2");
                    break;
                case 0xb1:
                    returnValue = "Radio activated";
                    break;
                case 0xb2:
                    returnValue = "Radio deactivated";
                    break;
                case 0xb3:
                    if (m_Data[2] == 0) returnValue = "TX: No ack";
                    else if (m_Data[1] == 1) returnValue = "TX: Ack";
                    else returnValue = "TX: Packet sent";
                    break;
                case 0xc1:
                    returnValue = "RX:";
                    for (int i = 0; i < (m_DataSize - 5); i++)
                        returnValue += "\t" + m_Data[i + 2].ToString("X2");
                    break;
            }
            return returnValue;
        }
    }
}