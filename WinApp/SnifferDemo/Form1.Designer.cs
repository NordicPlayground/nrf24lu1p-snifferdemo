namespace SnifferDemo
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonActivateRadio = new System.Windows.Forms.Button();
            this.numericChannel = new System.Windows.Forms.NumericUpDown();
            this.numericAddressLength = new System.Windows.Forms.NumericUpDown();
            this.numericAddress1 = new System.Windows.Forms.NumericUpDown();
            this.numericAddress2 = new System.Windows.Forms.NumericUpDown();
            this.numericAddress3 = new System.Windows.Forms.NumericUpDown();
            this.numericAddress4 = new System.Windows.Forms.NumericUpDown();
            this.numericAddress5 = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.numericPayloadLength = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.comboBoxCrc = new System.Windows.Forms.ComboBox();
            this.comboBoxBitrate = new System.Windows.Forms.ComboBox();
            this.timerPckFreq = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusUsb = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusRadio = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusFrequency = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupConfig = new System.Windows.Forms.GroupBox();
            this.comboBoxMode = new System.Windows.Forms.ComboBox();
            this.buttonSaveSettings = new System.Windows.Forms.Button();
            this.comboBoxSettings = new System.Windows.Forms.ComboBox();
            this.numericIntTime = new System.Windows.Forms.NumericUpDown();
            this.labelTransmit = new System.Windows.Forms.Label();
            this.btnControlStartStop = new System.Windows.Forms.Button();
            this.textBoxTransmit = new System.Windows.Forms.TextBox();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.buttonResetDongle = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.buttonFirmware = new System.Windows.Forms.Button();
            this.timerReconnect = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.timerIntervalTransmit = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.comboBoxListDisp = new System.Windows.Forms.ComboBox();
            this.checkBoxUpdateList = new System.Windows.Forms.CheckBox();
            this.checkBoxRecvRadioCom = new System.Windows.Forms.CheckBox();
            this.buttonSaveFile = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.labelListSize = new System.Windows.Forms.Label();
            this.listPackages = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.labelPckType = new System.Windows.Forms.Label();
            this.labelPckSize = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxPckContent = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.labelPckCounter = new System.Windows.Forms.Label();
            this.buttonClearList = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.labelDataDump = new System.Windows.Forms.Label();
            this.buttonSendDataDump = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txSweep = new System.Windows.Forms.Timer(this.components);
            this.labelControl = new System.Windows.Forms.Label();
            this.groupControl = new System.Windows.Forms.GroupBox();
            this.checkBoxIgnorePid = new System.Windows.Forms.CheckBox();
            this.radioControl3 = new System.Windows.Forms.RadioButton();
            this.radioControl2 = new System.Windows.Forms.RadioButton();
            this.radioControl1 = new System.Windows.Forms.RadioButton();
            this.radioControl0 = new System.Windows.Forms.RadioButton();
            this.buttonDataDump2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericChannel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAddressLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAddress1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAddress2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAddress3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAddress4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAddress5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPayloadLength)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericIntTime)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(67, 17);
            this.label9.TabIndex = 20;
            this.label9.Text = "Channel:";
            // 
            // buttonActivateRadio
            // 
            this.buttonActivateRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonActivateRadio.Location = new System.Drawing.Point(12, 210);
            this.buttonActivateRadio.Name = "buttonActivateRadio";
            this.buttonActivateRadio.Size = new System.Drawing.Size(320, 23);
            this.buttonActivateRadio.TabIndex = 24;
            this.buttonActivateRadio.Text = "Activate Radio";
            this.buttonActivateRadio.UseVisualStyleBackColor = true;
            this.buttonActivateRadio.Click += new System.EventHandler(this.button2_Click);
            // 
            // numericChannel
            // 
            this.numericChannel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericChannel.Location = new System.Drawing.Point(98, 30);
            this.numericChannel.Maximum = new decimal(new int[] {
            125,
            0,
            0,
            0});
            this.numericChannel.Name = "numericChannel";
            this.numericChannel.Size = new System.Drawing.Size(38, 20);
            this.numericChannel.TabIndex = 25;
            this.numericChannel.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericChannel.Leave += new System.EventHandler(this.radioGuiChanged);
            // 
            // numericAddressLength
            // 
            this.numericAddressLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericAddressLength.Location = new System.Drawing.Point(98, 82);
            this.numericAddressLength.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericAddressLength.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.numericAddressLength.Name = "numericAddressLength";
            this.numericAddressLength.Size = new System.Drawing.Size(38, 20);
            this.numericAddressLength.TabIndex = 26;
            this.numericAddressLength.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericAddressLength.ValueChanged += new System.EventHandler(this.numericAddressLength_ValueChanged);
            this.numericAddressLength.Leave += new System.EventHandler(this.radioGuiChanged);
            // 
            // numericAddress1
            // 
            this.numericAddress1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericAddress1.Hexadecimal = true;
            this.numericAddress1.Location = new System.Drawing.Point(98, 108);
            this.numericAddress1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericAddress1.Name = "numericAddress1";
            this.numericAddress1.Size = new System.Drawing.Size(38, 20);
            this.numericAddress1.TabIndex = 27;
            this.numericAddress1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericAddress1.Value = new decimal(new int[] {
            34,
            0,
            0,
            0});
            this.numericAddress1.Leave += new System.EventHandler(this.radioGuiChanged);
            // 
            // numericAddress2
            // 
            this.numericAddress2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericAddress2.Hexadecimal = true;
            this.numericAddress2.Location = new System.Drawing.Point(142, 107);
            this.numericAddress2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericAddress2.Name = "numericAddress2";
            this.numericAddress2.Size = new System.Drawing.Size(38, 20);
            this.numericAddress2.TabIndex = 28;
            this.numericAddress2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericAddress2.Value = new decimal(new int[] {
            51,
            0,
            0,
            0});
            this.numericAddress2.Leave += new System.EventHandler(this.radioGuiChanged);
            // 
            // numericAddress3
            // 
            this.numericAddress3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericAddress3.Hexadecimal = true;
            this.numericAddress3.Location = new System.Drawing.Point(186, 107);
            this.numericAddress3.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericAddress3.Name = "numericAddress3";
            this.numericAddress3.Size = new System.Drawing.Size(38, 20);
            this.numericAddress3.TabIndex = 29;
            this.numericAddress3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericAddress3.Value = new decimal(new int[] {
            68,
            0,
            0,
            0});
            this.numericAddress3.Leave += new System.EventHandler(this.radioGuiChanged);
            // 
            // numericAddress4
            // 
            this.numericAddress4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericAddress4.Hexadecimal = true;
            this.numericAddress4.Location = new System.Drawing.Point(230, 107);
            this.numericAddress4.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericAddress4.Name = "numericAddress4";
            this.numericAddress4.Size = new System.Drawing.Size(38, 20);
            this.numericAddress4.TabIndex = 30;
            this.numericAddress4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericAddress4.Value = new decimal(new int[] {
            85,
            0,
            0,
            0});
            this.numericAddress4.Leave += new System.EventHandler(this.radioGuiChanged);
            // 
            // numericAddress5
            // 
            this.numericAddress5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericAddress5.Hexadecimal = true;
            this.numericAddress5.Location = new System.Drawing.Point(274, 107);
            this.numericAddress5.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.numericAddress5.Name = "numericAddress5";
            this.numericAddress5.Size = new System.Drawing.Size(38, 20);
            this.numericAddress5.TabIndex = 31;
            this.numericAddress5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericAddress5.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericAddress5.Leave += new System.EventHandler(this.radioGuiChanged);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(6, 85);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(101, 17);
            this.label10.TabIndex = 32;
            this.label10.Text = "Address length:";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 111);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 17);
            this.label11.TabIndex = 33;
            this.label11.Text = "Address:";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(6, 137);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 17);
            this.label12.TabIndex = 34;
            this.label12.Text = "Radio mode:";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(6, 59);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(86, 17);
            this.label13.TabIndex = 37;
            this.label13.Text = "Payload length:";
            // 
            // numericPayloadLength
            // 
            this.numericPayloadLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericPayloadLength.Location = new System.Drawing.Point(98, 56);
            this.numericPayloadLength.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.numericPayloadLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericPayloadLength.Name = "numericPayloadLength";
            this.numericPayloadLength.Size = new System.Drawing.Size(38, 20);
            this.numericPayloadLength.TabIndex = 36;
            this.numericPayloadLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericPayloadLength.Leave += new System.EventHandler(this.radioGuiChanged);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(165, 33);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(45, 17);
            this.label14.TabIndex = 38;
            this.label14.Text = "CRC:";
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(165, 59);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(45, 18);
            this.label15.TabIndex = 39;
            this.label15.Text = "Bitrate:";
            // 
            // comboBoxCrc
            // 
            this.comboBoxCrc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxCrc.FormattingEnabled = true;
            this.comboBoxCrc.Items.AddRange(new object[] {
            "Off",
            "On, 8-bit",
            "On, 16-bit"});
            this.comboBoxCrc.Location = new System.Drawing.Point(230, 29);
            this.comboBoxCrc.Name = "comboBoxCrc";
            this.comboBoxCrc.Size = new System.Drawing.Size(82, 21);
            this.comboBoxCrc.TabIndex = 40;
            this.comboBoxCrc.Leave += new System.EventHandler(this.radioGuiChanged);
            // 
            // comboBoxBitrate
            // 
            this.comboBoxBitrate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxBitrate.FormattingEnabled = true;
            this.comboBoxBitrate.Items.AddRange(new object[] {
            "250 KBit",
            "1 MBit",
            "2 MBit"});
            this.comboBoxBitrate.Location = new System.Drawing.Point(230, 56);
            this.comboBoxBitrate.Name = "comboBoxBitrate";
            this.comboBoxBitrate.Size = new System.Drawing.Size(82, 21);
            this.comboBoxBitrate.TabIndex = 41;
            this.comboBoxBitrate.Leave += new System.EventHandler(this.radioGuiChanged);
            // 
            // timerPckFreq
            // 
            this.timerPckFreq.Enabled = true;
            this.timerPckFreq.Interval = 500;
            this.timerPckFreq.Tick += new System.EventHandler(this.timerPckFreq_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.AllowItemReorder = true;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusUsb,
            this.statusRadio,
            this.statusFrequency});
            this.statusStrip1.Location = new System.Drawing.Point(0, 554);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1018, 22);
            this.statusStrip1.TabIndex = 50;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusUsb
            // 
            this.statusUsb.ActiveLinkColor = System.Drawing.Color.Red;
            this.statusUsb.AutoSize = false;
            this.statusUsb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.statusUsb.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusUsb.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.statusUsb.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.statusUsb.Name = "statusUsb";
            this.statusUsb.Size = new System.Drawing.Size(180, 17);
            this.statusUsb.Text = "Dongle: Attempting to connect";
            // 
            // statusRadio
            // 
            this.statusRadio.AutoSize = false;
            this.statusRadio.BackColor = System.Drawing.Color.Silver;
            this.statusRadio.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusRadio.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.statusRadio.Enabled = false;
            this.statusRadio.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.statusRadio.Name = "statusRadio";
            this.statusRadio.Size = new System.Drawing.Size(180, 17);
            this.statusRadio.Text = "Radio: Deactivated";
            // 
            // statusFrequency
            // 
            this.statusFrequency.AutoSize = false;
            this.statusFrequency.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.statusFrequency.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.statusFrequency.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken;
            this.statusFrequency.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.statusFrequency.Name = "statusFrequency";
            this.statusFrequency.Size = new System.Drawing.Size(150, 17);
            this.statusFrequency.Text = "RF-Frequency: 0 Hz";
            this.statusFrequency.Visible = false;
            // 
            // groupConfig
            // 
            this.groupConfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(219)))), ((int)(((byte)(134)))));
            this.groupConfig.Controls.Add(this.comboBoxMode);
            this.groupConfig.Controls.Add(this.buttonSaveSettings);
            this.groupConfig.Controls.Add(this.comboBoxSettings);
            this.groupConfig.Controls.Add(this.label9);
            this.groupConfig.Controls.Add(this.numericChannel);
            this.groupConfig.Controls.Add(this.comboBoxCrc);
            this.groupConfig.Controls.Add(this.numericAddressLength);
            this.groupConfig.Controls.Add(this.label14);
            this.groupConfig.Controls.Add(this.comboBoxBitrate);
            this.groupConfig.Controls.Add(this.numericAddress1);
            this.groupConfig.Controls.Add(this.label15);
            this.groupConfig.Controls.Add(this.numericAddress2);
            this.groupConfig.Controls.Add(this.numericAddress3);
            this.groupConfig.Controls.Add(this.numericAddress4);
            this.groupConfig.Controls.Add(this.numericAddress5);
            this.groupConfig.Controls.Add(this.label13);
            this.groupConfig.Controls.Add(this.label10);
            this.groupConfig.Controls.Add(this.numericPayloadLength);
            this.groupConfig.Controls.Add(this.label11);
            this.groupConfig.Controls.Add(this.label12);
            this.groupConfig.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupConfig.Location = new System.Drawing.Point(12, 6);
            this.groupConfig.Name = "groupConfig";
            this.groupConfig.Size = new System.Drawing.Size(320, 198);
            this.groupConfig.TabIndex = 51;
            this.groupConfig.TabStop = false;
            this.groupConfig.Text = "Radio Configuration";
            this.groupConfig.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // comboBoxMode
            // 
            this.comboBoxMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxMode.FormattingEnabled = true;
            this.comboBoxMode.Items.AddRange(new object[] {
            "Shockburst",
            "Enh. shockburst",
            "Enh. shockburst w/dynamic payload length",
            "Auto mode"});
            this.comboBoxMode.Location = new System.Drawing.Point(97, 134);
            this.comboBoxMode.Name = "comboBoxMode";
            this.comboBoxMode.Size = new System.Drawing.Size(214, 21);
            this.comboBoxMode.TabIndex = 63;
            this.comboBoxMode.Leave += new System.EventHandler(this.radioGuiChanged);
            // 
            // buttonSaveSettings
            // 
            this.buttonSaveSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveSettings.Location = new System.Drawing.Point(5, 171);
            this.buttonSaveSettings.Name = "buttonSaveSettings";
            this.buttonSaveSettings.Size = new System.Drawing.Size(86, 21);
            this.buttonSaveSettings.TabIndex = 57;
            this.buttonSaveSettings.Text = "Save settings";
            this.buttonSaveSettings.UseVisualStyleBackColor = true;
            this.buttonSaveSettings.Click += new System.EventHandler(this.buttonSaveSettings_Click);
            // 
            // comboBoxSettings
            // 
            this.comboBoxSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxSettings.FormattingEnabled = true;
            this.comboBoxSettings.Location = new System.Drawing.Point(97, 171);
            this.comboBoxSettings.Name = "comboBoxSettings";
            this.comboBoxSettings.Size = new System.Drawing.Size(214, 21);
            this.comboBoxSettings.TabIndex = 56;
            this.comboBoxSettings.SelectedIndexChanged += new System.EventHandler(this.comboBoxSettings_SelectedIndexChanged);
            // 
            // numericIntTime
            // 
            this.numericIntTime.Enabled = false;
            this.numericIntTime.Location = new System.Drawing.Point(234, 42);
            this.numericIntTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericIntTime.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericIntTime.Name = "numericIntTime";
            this.numericIntTime.Size = new System.Drawing.Size(64, 20);
            this.numericIntTime.TabIndex = 62;
            this.numericIntTime.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // labelTransmit
            // 
            this.labelTransmit.BackColor = System.Drawing.Color.Transparent;
            this.labelTransmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTransmit.Location = new System.Drawing.Point(273, 157);
            this.labelTransmit.Name = "labelTransmit";
            this.labelTransmit.Size = new System.Drawing.Size(41, 19);
            this.labelTransmit.TabIndex = 60;
            this.labelTransmit.Text = "HEX";
            this.labelTransmit.Click += new System.EventHandler(this.labelTransmit_Click);
            // 
            // btnControlStartStop
            // 
            this.btnControlStartStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnControlStartStop.Location = new System.Drawing.Point(5, 131);
            this.btnControlStartStop.Name = "btnControlStartStop";
            this.btnControlStartStop.Size = new System.Drawing.Size(309, 22);
            this.btnControlStartStop.TabIndex = 59;
            this.btnControlStartStop.Text = "Transmit single packet";
            this.btnControlStartStop.UseVisualStyleBackColor = true;
            this.btnControlStartStop.Click += new System.EventHandler(this.buttonTransmit_Click);
            // 
            // textBoxTransmit
            // 
            this.textBoxTransmit.Location = new System.Drawing.Point(5, 156);
            this.textBoxTransmit.Name = "textBoxTransmit";
            this.textBoxTransmit.Size = new System.Drawing.Size(270, 20);
            this.textBoxTransmit.TabIndex = 58;
            this.textBoxTransmit.TextChanged += new System.EventHandler(this.textBoxTransmit_TextChanged);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "*.txt";
            this.saveFileDialog1.Filter = "Text files|*.txt";
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // buttonResetDongle
            // 
            this.buttonResetDongle.Location = new System.Drawing.Point(181, 492);
            this.buttonResetDongle.Name = "buttonResetDongle";
            this.buttonResetDongle.Size = new System.Drawing.Size(124, 21);
            this.buttonResetDongle.TabIndex = 58;
            this.buttonResetDongle.Text = "Reset Dongle";
            this.buttonResetDongle.UseVisualStyleBackColor = true;
            this.buttonResetDongle.Visible = false;
            this.buttonResetDongle.Click += new System.EventHandler(this.buttonResetDongle_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(311, 492);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 22);
            this.button2.TabIndex = 55;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_2);
            // 
            // buttonFirmware
            // 
            this.buttonFirmware.Location = new System.Drawing.Point(6, 490);
            this.buttonFirmware.Name = "buttonFirmware";
            this.buttonFirmware.Size = new System.Drawing.Size(169, 23);
            this.buttonFirmware.TabIndex = 54;
            this.buttonFirmware.Text = "Update Firmware";
            this.buttonFirmware.UseVisualStyleBackColor = true;
            this.buttonFirmware.Click += new System.EventHandler(this.buttonFirmware_Click);
            // 
            // timerReconnect
            // 
            this.timerReconnect.Interval = 470;
            this.timerReconnect.Tick += new System.EventHandler(this.timerReconnect_Tick);
            // 
            // saveFileDialog2
            // 
            this.saveFileDialog2.DefaultExt = "*.dat";
            this.saveFileDialog2.Filter = "Radio config data|*.dat";
            this.saveFileDialog2.InitialDirectory = "configdata";
            this.saveFileDialog2.RestoreDirectory = true;
            this.saveFileDialog2.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog2_FileOk);
            // 
            // timerIntervalTransmit
            // 
            this.timerIntervalTransmit.Tick += new System.EventHandler(this.timerIntervalTransmit_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(338, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(680, 545);
            this.tabControl1.TabIndex = 54;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.comboBoxListDisp);
            this.tabPage1.Controls.Add(this.checkBoxUpdateList);
            this.tabPage1.Controls.Add(this.checkBoxRecvRadioCom);
            this.tabPage1.Controls.Add(this.buttonSaveFile);
            this.tabPage1.Controls.Add(this.label17);
            this.tabPage1.Controls.Add(this.labelListSize);
            this.tabPage1.Controls.Add(this.listPackages);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.labelPckType);
            this.tabPage1.Controls.Add(this.labelPckSize);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.textBoxPckContent);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.labelPckCounter);
            this.tabPage1.Controls.Add(this.buttonClearList);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(672, 519);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Received Packets";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // comboBoxListDisp
            // 
            this.comboBoxListDisp.FormattingEnabled = true;
            this.comboBoxListDisp.Items.AddRange(new object[] {
            "Dec",
            "Hex"});
            this.comboBoxListDisp.Location = new System.Drawing.Point(316, 493);
            this.comboBoxListDisp.Name = "comboBoxListDisp";
            this.comboBoxListDisp.Size = new System.Drawing.Size(47, 21);
            this.comboBoxListDisp.TabIndex = 73;
            this.comboBoxListDisp.Visible = false;
            this.comboBoxListDisp.SelectedIndexChanged += new System.EventHandler(this.comboBoxListDisp_SelectedIndexChanged);
            // 
            // checkBoxUpdateList
            // 
            this.checkBoxUpdateList.AutoSize = true;
            this.checkBoxUpdateList.Checked = true;
            this.checkBoxUpdateList.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxUpdateList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxUpdateList.Location = new System.Drawing.Point(234, 496);
            this.checkBoxUpdateList.Name = "checkBoxUpdateList";
            this.checkBoxUpdateList.Size = new System.Drawing.Size(76, 17);
            this.checkBoxUpdateList.TabIndex = 72;
            this.checkBoxUpdateList.Text = "Update list";
            this.checkBoxUpdateList.UseVisualStyleBackColor = true;
            // 
            // checkBoxRecvRadioCom
            // 
            this.checkBoxRecvRadioCom.AutoSize = true;
            this.checkBoxRecvRadioCom.Checked = true;
            this.checkBoxRecvRadioCom.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRecvRadioCom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxRecvRadioCom.Location = new System.Drawing.Point(145, 496);
            this.checkBoxRecvRadioCom.Name = "checkBoxRecvRadioCom";
            this.checkBoxRecvRadioCom.Size = new System.Drawing.Size(83, 17);
            this.checkBoxRecvRadioCom.TabIndex = 70;
            this.checkBoxRecvRadioCom.Text = "Receive RF";
            this.checkBoxRecvRadioCom.UseVisualStyleBackColor = true;
            // 
            // buttonSaveFile
            // 
            this.buttonSaveFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSaveFile.Location = new System.Drawing.Point(74, 493);
            this.buttonSaveFile.Name = "buttonSaveFile";
            this.buttonSaveFile.Size = new System.Drawing.Size(65, 20);
            this.buttonSaveFile.TabIndex = 69;
            this.buttonSaveFile.Text = "Save list";
            this.buttonSaveFile.UseVisualStyleBackColor = true;
            this.buttonSaveFile.Click += new System.EventHandler(this.buttonSaveFile_Click_1);
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.SystemColors.Control;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(3, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(425, 14);
            this.label17.TabIndex = 68;
            this.label17.Text = "Incomming USB-packets";
            // 
            // labelListSize
            // 
            this.labelListSize.BackColor = System.Drawing.Color.Transparent;
            this.labelListSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelListSize.Location = new System.Drawing.Point(373, 497);
            this.labelListSize.Name = "labelListSize";
            this.labelListSize.Size = new System.Drawing.Size(76, 19);
            this.labelListSize.TabIndex = 57;
            this.labelListSize.Text = "List size: 0";
            // 
            // listPackages
            // 
            this.listPackages.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listPackages.FormattingEnabled = true;
            this.listPackages.Location = new System.Drawing.Point(6, 18);
            this.listPackages.Name = "listPackages";
            this.listPackages.Size = new System.Drawing.Size(443, 459);
            this.listPackages.TabIndex = 56;
            this.listPackages.SelectedIndexChanged += new System.EventHandler(this.listPackages_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(455, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 16);
            this.label4.TabIndex = 58;
            this.label4.Text = "Packet details";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(455, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 16);
            this.label2.TabIndex = 59;
            this.label2.Text = "Type:";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(455, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(51, 16);
            this.label5.TabIndex = 60;
            this.label5.Text = "Length:";
            // 
            // labelPckType
            // 
            this.labelPckType.BackColor = System.Drawing.Color.White;
            this.labelPckType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPckType.Location = new System.Drawing.Point(512, 19);
            this.labelPckType.Name = "labelPckType";
            this.labelPckType.Size = new System.Drawing.Size(155, 16);
            this.labelPckType.TabIndex = 61;
            // 
            // labelPckSize
            // 
            this.labelPckSize.BackColor = System.Drawing.Color.White;
            this.labelPckSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPckSize.Location = new System.Drawing.Point(512, 46);
            this.labelPckSize.Name = "labelPckSize";
            this.labelPckSize.Size = new System.Drawing.Size(56, 16);
            this.labelPckSize.TabIndex = 62;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(455, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(212, 16);
            this.label8.TabIndex = 63;
            this.label8.Text = "Content:";
            // 
            // textBoxPckContent
            // 
            this.textBoxPckContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPckContent.Location = new System.Drawing.Point(455, 91);
            this.textBoxPckContent.Name = "textBoxPckContent";
            this.textBoxPckContent.Size = new System.Drawing.Size(212, 421);
            this.textBoxPckContent.TabIndex = 64;
            this.textBoxPckContent.Text = "";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(574, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 16);
            this.label6.TabIndex = 65;
            this.label6.Text = "Counter:";
            // 
            // labelPckCounter
            // 
            this.labelPckCounter.BackColor = System.Drawing.Color.White;
            this.labelPckCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPckCounter.Location = new System.Drawing.Point(631, 46);
            this.labelPckCounter.Name = "labelPckCounter";
            this.labelPckCounter.Size = new System.Drawing.Size(36, 16);
            this.labelPckCounter.TabIndex = 66;
            // 
            // buttonClearList
            // 
            this.buttonClearList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonClearList.Location = new System.Drawing.Point(3, 493);
            this.buttonClearList.Name = "buttonClearList";
            this.buttonClearList.Size = new System.Drawing.Size(65, 20);
            this.buttonClearList.TabIndex = 67;
            this.buttonClearList.Text = "Clear List";
            this.buttonClearList.UseVisualStyleBackColor = true;
            this.buttonClearList.Click += new System.EventHandler(this.buttonClearList_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.buttonDataDump2);
            this.tabPage2.Controls.Add(this.labelDataDump);
            this.tabPage2.Controls.Add(this.buttonSendDataDump);
            this.tabPage2.Controls.Add(this.buttonResetDongle);
            this.tabPage2.Controls.Add(this.buttonFirmware);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(672, 519);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Custom Area";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // labelDataDump
            // 
            this.labelDataDump.Location = new System.Drawing.Point(6, 42);
            this.labelDataDump.Name = "labelDataDump";
            this.labelDataDump.Size = new System.Drawing.Size(232, 38);
            this.labelDataDump.TabIndex = 60;
            this.labelDataDump.Text = "label1";
            // 
            // buttonSendDataDump
            // 
            this.buttonSendDataDump.Location = new System.Drawing.Point(6, 8);
            this.buttonSendDataDump.Name = "buttonSendDataDump";
            this.buttonSendDataDump.Size = new System.Drawing.Size(232, 23);
            this.buttonSendDataDump.TabIndex = 59;
            this.buttonSendDataDump.Text = "Send Data Dump (0,1,2,....,149)";
            this.buttonSendDataDump.UseVisualStyleBackColor = true;
            this.buttonSendDataDump.Click += new System.EventHandler(this.buttonSendDataDump_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 531);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 20);
            this.button1.TabIndex = 71;
            this.button1.Text = "About";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // txSweep
            // 
            this.txSweep.Interval = 200;
            this.txSweep.Tick += new System.EventHandler(this.txSweep_Tick);
            // 
            // labelControl
            // 
            this.labelControl.BackColor = System.Drawing.Color.Transparent;
            this.labelControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl.Location = new System.Drawing.Point(3, 179);
            this.labelControl.Name = "labelControl";
            this.labelControl.Size = new System.Drawing.Size(307, 100);
            this.labelControl.TabIndex = 64;
            this.labelControl.Text = "Analyzed payloads: 0";
            // 
            // groupControl
            // 
            this.groupControl.Controls.Add(this.checkBoxIgnorePid);
            this.groupControl.Controls.Add(this.radioControl3);
            this.groupControl.Controls.Add(this.radioControl2);
            this.groupControl.Controls.Add(this.radioControl1);
            this.groupControl.Controls.Add(this.radioControl0);
            this.groupControl.Controls.Add(this.btnControlStartStop);
            this.groupControl.Controls.Add(this.labelControl);
            this.groupControl.Controls.Add(this.numericIntTime);
            this.groupControl.Controls.Add(this.textBoxTransmit);
            this.groupControl.Controls.Add(this.labelTransmit);
            this.groupControl.Enabled = false;
            this.groupControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl.Location = new System.Drawing.Point(12, 239);
            this.groupControl.Name = "groupControl";
            this.groupControl.Size = new System.Drawing.Size(320, 286);
            this.groupControl.TabIndex = 73;
            this.groupControl.TabStop = false;
            this.groupControl.Text = "Radio control";
            // 
            // checkBoxIgnorePid
            // 
            this.checkBoxIgnorePid.AutoSize = true;
            this.checkBoxIgnorePid.Checked = true;
            this.checkBoxIgnorePid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxIgnorePid.Enabled = false;
            this.checkBoxIgnorePid.Location = new System.Drawing.Point(234, 88);
            this.checkBoxIgnorePid.Margin = new System.Windows.Forms.Padding(2);
            this.checkBoxIgnorePid.Name = "checkBoxIgnorePid";
            this.checkBoxIgnorePid.Size = new System.Drawing.Size(87, 17);
            this.checkBoxIgnorePid.TabIndex = 77;
            this.checkBoxIgnorePid.Text = "Ignore PID";
            this.checkBoxIgnorePid.UseVisualStyleBackColor = true;
            // 
            // radioControl3
            // 
            this.radioControl3.AutoSize = true;
            this.radioControl3.Location = new System.Drawing.Point(6, 88);
            this.radioControl3.Name = "radioControl3";
            this.radioControl3.Size = new System.Drawing.Size(189, 17);
            this.radioControl3.TabIndex = 76;
            this.radioControl3.Tag = "3";
            this.radioControl3.Text = "Received packet comparator";
            this.radioControl3.UseVisualStyleBackColor = true;
            this.radioControl3.CheckedChanged += new System.EventHandler(this.radioControls_CheckedChanged);
            // 
            // radioControl2
            // 
            this.radioControl2.AutoSize = true;
            this.radioControl2.Location = new System.Drawing.Point(6, 65);
            this.radioControl2.Name = "radioControl2";
            this.radioControl2.Size = new System.Drawing.Size(81, 17);
            this.radioControl2.TabIndex = 75;
            this.radioControl2.Tag = "2";
            this.radioControl2.Text = "TX sweep";
            this.radioControl2.UseVisualStyleBackColor = true;
            this.radioControl2.CheckedChanged += new System.EventHandler(this.radioControls_CheckedChanged);
            // 
            // radioControl1
            // 
            this.radioControl1.AutoSize = true;
            this.radioControl1.Location = new System.Drawing.Point(6, 42);
            this.radioControl1.Name = "radioControl1";
            this.radioControl1.Size = new System.Drawing.Size(116, 17);
            this.radioControl1.TabIndex = 74;
            this.radioControl1.Tag = "1";
            this.radioControl1.Text = "Interval transmit";
            this.radioControl1.UseVisualStyleBackColor = true;
            this.radioControl1.CheckedChanged += new System.EventHandler(this.radioControls_CheckedChanged);
            // 
            // radioControl0
            // 
            this.radioControl0.AutoSize = true;
            this.radioControl0.Checked = true;
            this.radioControl0.Location = new System.Drawing.Point(6, 19);
            this.radioControl0.Name = "radioControl0";
            this.radioControl0.Size = new System.Drawing.Size(153, 17);
            this.radioControl0.TabIndex = 73;
            this.radioControl0.TabStop = true;
            this.radioControl0.Tag = "0";
            this.radioControl0.Text = "Transmit single packet";
            this.radioControl0.UseVisualStyleBackColor = true;
            this.radioControl0.CheckedChanged += new System.EventHandler(this.radioControls_CheckedChanged);
            // 
            // buttonDataDump2
            // 
            this.buttonDataDump2.Location = new System.Drawing.Point(244, 8);
            this.buttonDataDump2.Name = "buttonDataDump2";
            this.buttonDataDump2.Size = new System.Drawing.Size(232, 23);
            this.buttonDataDump2.TabIndex = 61;
            this.buttonDataDump2.Text = "Send Data Dump (FF, FE, ...)";
            this.buttonDataDump2.UseVisualStyleBackColor = true;
            this.buttonDataDump2.Click += new System.EventHandler(this.buttonDataDump2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 576);
            this.Controls.Add(this.groupControl);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonActivateRadio);
            this.Controls.Add(this.groupConfig);
            this.Controls.Add(this.statusStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.Text = "Sniffer Demo v1.0";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.numericChannel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAddressLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAddress1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAddress2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAddress3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAddress4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericAddress5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericPayloadLength)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupConfig.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericIntTime)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.groupControl.ResumeLayout(false);
            this.groupControl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button buttonActivateRadio;
        private System.Windows.Forms.NumericUpDown numericChannel;
        private System.Windows.Forms.NumericUpDown numericAddressLength;
        private System.Windows.Forms.NumericUpDown numericAddress1;
        private System.Windows.Forms.NumericUpDown numericAddress2;
        private System.Windows.Forms.NumericUpDown numericAddress3;
        private System.Windows.Forms.NumericUpDown numericAddress4;
        private System.Windows.Forms.NumericUpDown numericAddress5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numericPayloadLength;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox comboBoxCrc;
        private System.Windows.Forms.ComboBox comboBoxBitrate;
        private System.Windows.Forms.Timer timerPckFreq;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusUsb;
        private System.Windows.Forms.ToolStripStatusLabel statusRadio;
        private System.Windows.Forms.GroupBox groupConfig;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripStatusLabel statusFrequency;
        private System.Windows.Forms.Timer timerReconnect;
        private System.Windows.Forms.Button buttonSaveSettings;
        private System.Windows.Forms.ComboBox comboBoxSettings;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.Button buttonFirmware;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnControlStartStop;
        private System.Windows.Forms.TextBox textBoxTransmit;
        private System.Windows.Forms.Label labelTransmit;
        private System.Windows.Forms.NumericUpDown numericIntTime;
        private System.Windows.Forms.Timer timerIntervalTransmit;
        private System.Windows.Forms.Button buttonResetDongle;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.CheckBox checkBoxUpdateList;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBoxRecvRadioCom;
        private System.Windows.Forms.Button buttonSaveFile;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label labelListSize;
        private System.Windows.Forms.ListBox listPackages;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label labelPckType;
        private System.Windows.Forms.Label labelPckSize;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox textBoxPckContent;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelPckCounter;
        private System.Windows.Forms.Button buttonClearList;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Timer txSweep;
        private System.Windows.Forms.ComboBox comboBoxMode;
        private System.Windows.Forms.ComboBox comboBoxListDisp;
        private System.Windows.Forms.Label labelControl;
        private System.Windows.Forms.GroupBox groupControl;
        private System.Windows.Forms.RadioButton radioControl3;
        private System.Windows.Forms.RadioButton radioControl2;
        private System.Windows.Forms.RadioButton radioControl1;
        private System.Windows.Forms.RadioButton radioControl0;
        private System.Windows.Forms.CheckBox checkBoxIgnorePid;
        private System.Windows.Forms.Button buttonSendDataDump;
        private System.Windows.Forms.Label labelDataDump;
        private System.Windows.Forms.Button buttonDataDump2;
    }
}

