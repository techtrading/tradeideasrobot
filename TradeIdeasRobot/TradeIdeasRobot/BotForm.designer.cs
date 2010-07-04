namespace TradeIdeasRobot
{
    partial class BotForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BotForm));
            this.label1 = new System.Windows.Forms.Label();
            this.tbTIUser = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbTIPassword = new System.Windows.Forms.TextBox();
            this.listBoxOutput = new System.Windows.Forms.ListBox();
            this.btnAddStrat = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnUp = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.btnOpenAlerts = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.strategyGridView = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbSimHost = new System.Windows.Forms.TextBox();
            this.tbSimPort = new System.Windows.Forms.MaskedTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbIBPortNum = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbIBHost = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.cbBeepOnOrder = new System.Windows.Forms.CheckBox();
            this.label14 = new System.Windows.Forms.Label();
            this.cbDuplicates = new System.Windows.Forms.CheckBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tbCloseHrs = new System.Windows.Forms.MaskedTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbCloseMins = new System.Windows.Forms.MaskedTextBox();
            this.tbStartHrs = new System.Windows.Forms.MaskedTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbStartMins = new System.Windows.Forms.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cbTransmit = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbQuoteTimeout = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnLoginAll = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tradeIdeasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWindowsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveWindowsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.newAlertMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnTest = new System.Windows.Forms.Button();
            this.axStatusBarX1 = new AxTradeIdeasWindow.AxStatusBarX();
            this.axTws1 = new AxTWSLib.AxTws();
            this.axTwsSim = new AxTWSLib.AxTws();
            this.axTradeIdeasWindowX1 = new AxTradeIdeasWindow.AxTradeIdeasWindowX();
            this.tabControl.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.strategyGridView)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axStatusBarX1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTws1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTwsSim)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTradeIdeasWindowX1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "User:";
            // 
            // tbTIUser
            // 
            this.tbTIUser.Location = new System.Drawing.Point(59, 13);
            this.tbTIUser.Name = "tbTIUser";
            this.tbTIUser.Size = new System.Drawing.Size(100, 20);
            this.tbTIUser.TabIndex = 1;
            this.tbTIUser.TextChanged += new System.EventHandler(this.loginTextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Pwd:";
            // 
            // tbTIPassword
            // 
            this.tbTIPassword.Location = new System.Drawing.Point(58, 39);
            this.tbTIPassword.Name = "tbTIPassword";
            this.tbTIPassword.PasswordChar = '*';
            this.tbTIPassword.Size = new System.Drawing.Size(100, 20);
            this.tbTIPassword.TabIndex = 3;
            this.tbTIPassword.TextChanged += new System.EventHandler(this.loginTextChanged);
            // 
            // listBoxOutput
            // 
            this.listBoxOutput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listBoxOutput.FormattingEnabled = true;
            this.listBoxOutput.HorizontalScrollbar = true;
            this.listBoxOutput.Location = new System.Drawing.Point(0, 387);
            this.listBoxOutput.Name = "listBoxOutput";
            this.listBoxOutput.Size = new System.Drawing.Size(582, 121);
            this.listBoxOutput.TabIndex = 5;
            // 
            // btnAddStrat
            // 
            this.btnAddStrat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddStrat.Location = new System.Drawing.Point(13, 0);
            this.btnAddStrat.Name = "btnAddStrat";
            this.btnAddStrat.Size = new System.Drawing.Size(75, 23);
            this.btnAddStrat.TabIndex = 7;
            this.btnAddStrat.Text = "Add...";
            this.btnAddStrat.UseVisualStyleBackColor = true;
            this.btnAddStrat.Click += new System.EventHandler(this.btnAddStrat_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Location = new System.Drawing.Point(0, 59);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(582, 326);
            this.tabControl.TabIndex = 8;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel2);
            this.tabPage2.Controls.Add(this.strategyGridView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(574, 300);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Strategies";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.btnDown);
            this.panel2.Controls.Add(this.btnUp);
            this.panel2.Controls.Add(this.btnAddStrat);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnLoad);
            this.panel2.Controls.Add(this.btnOpenAlerts);
            this.panel2.Controls.Add(this.btnEdit);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Location = new System.Drawing.Point(473, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(98, 288);
            this.panel2.TabIndex = 14;
            // 
            // btnDown
            // 
            this.btnDown.Location = new System.Drawing.Point(13, 186);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(75, 23);
            this.btnDown.TabIndex = 15;
            this.btnDown.Text = "Down";
            this.btnDown.UseVisualStyleBackColor = true;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnUp
            // 
            this.btnUp.Location = new System.Drawing.Point(13, 157);
            this.btnUp.Name = "btnUp";
            this.btnUp.Size = new System.Drawing.Size(75, 23);
            this.btnUp.TabIndex = 14;
            this.btnUp.Text = "Up";
            this.btnUp.UseVisualStyleBackColor = true;
            this.btnUp.Click += new System.EventHandler(this.btnUp_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(13, 265);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoad.Location = new System.Drawing.Point(13, 236);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 10;
            this.btnLoad.Text = "Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // btnOpenAlerts
            // 
            this.btnOpenAlerts.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenAlerts.Location = new System.Drawing.Point(13, 87);
            this.btnOpenAlerts.Name = "btnOpenAlerts";
            this.btnOpenAlerts.Size = new System.Drawing.Size(75, 23);
            this.btnOpenAlerts.TabIndex = 13;
            this.btnOpenAlerts.Text = "Edit Alerts";
            this.btnOpenAlerts.UseVisualStyleBackColor = true;
            this.btnOpenAlerts.Click += new System.EventHandler(this.btnOpenAlerts_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.Location = new System.Drawing.Point(13, 29);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 11;
            this.btnEdit.Text = "Edit...";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Location = new System.Drawing.Point(13, 58);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 12;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // strategyGridView
            // 
            this.strategyGridView.AllowUserToAddRows = false;
            this.strategyGridView.AllowUserToDeleteRows = false;
            this.strategyGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.strategyGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.strategyGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.strategyGridView.Location = new System.Drawing.Point(4, 17);
            this.strategyGridView.Name = "strategyGridView";
            this.strategyGridView.ShowEditingIcon = false;
            this.strategyGridView.Size = new System.Drawing.Size(464, 278);
            this.strategyGridView.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(574, 300);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Login";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbSimHost);
            this.groupBox3.Controls.Add(this.tbSimPort);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(207, 91);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(190, 83);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Simulator";
            // 
            // tbSimHost
            // 
            this.tbSimHost.Location = new System.Drawing.Point(54, 19);
            this.tbSimHost.Name = "tbSimHost";
            this.tbSimHost.Size = new System.Drawing.Size(100, 20);
            this.tbSimHost.TabIndex = 9;
            this.tbSimHost.Text = "127.0.0.1";
            this.tbSimHost.TextChanged += new System.EventHandler(this.loginTextChanged);
            // 
            // tbSimPort
            // 
            this.tbSimPort.Location = new System.Drawing.Point(53, 46);
            this.tbSimPort.Mask = "00000";
            this.tbSimPort.Name = "tbSimPort";
            this.tbSimPort.PromptChar = ' ';
            this.tbSimPort.Size = new System.Drawing.Size(100, 20);
            this.tbSimPort.TabIndex = 13;
            this.tbSimPort.Text = "7499";
            this.tbSimPort.ValidatingType = typeof(int);
            this.tbSimPort.TextChanged += new System.EventHandler(this.loginTextChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(7, 22);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(32, 13);
            this.label12.TabIndex = 8;
            this.label12.Text = "Host:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(9, 49);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Port:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbIBPortNum);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.tbIBHost);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(9, 87);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(192, 87);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "InteractiveBrokers";
            // 
            // tbIBPortNum
            // 
            this.tbIBPortNum.Location = new System.Drawing.Point(55, 46);
            this.tbIBPortNum.Mask = "00000";
            this.tbIBPortNum.Name = "tbIBPortNum";
            this.tbIBPortNum.PromptChar = ' ';
            this.tbIBPortNum.Size = new System.Drawing.Size(100, 20);
            this.tbIBPortNum.TabIndex = 7;
            this.tbIBPortNum.Text = "7496";
            this.tbIBPortNum.ValidatingType = typeof(int);
            this.tbIBPortNum.TextChanged += new System.EventHandler(this.loginTextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Port:";
            // 
            // tbIBHost
            // 
            this.tbIBHost.Location = new System.Drawing.Point(56, 19);
            this.tbIBHost.Name = "tbIBHost";
            this.tbIBHost.Size = new System.Drawing.Size(100, 20);
            this.tbIBHost.TabIndex = 1;
            this.tbIBHost.Text = "127.0.0.1";
            this.tbIBHost.TextChanged += new System.EventHandler(this.loginTextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Host:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tbTIPassword);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tbTIUser);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(195, 70);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Trade-Ideas";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.axTradeIdeasWindowX1);
            this.tabPage3.Controls.Add(this.axTwsSim);
            this.tabPage3.Controls.Add(this.axTws1);
            this.tabPage3.Controls.Add(this.cbBeepOnOrder);
            this.tabPage3.Controls.Add(this.label14);
            this.tabPage3.Controls.Add(this.cbDuplicates);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.groupBox4);
            this.tabPage3.Controls.Add(this.cbTransmit);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.tbQuoteTimeout);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(574, 300);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Settings";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // cbBeepOnOrder
            // 
            this.cbBeepOnOrder.AutoSize = true;
            this.cbBeepOnOrder.Location = new System.Drawing.Point(150, 86);
            this.cbBeepOnOrder.Name = "cbBeepOnOrder";
            this.cbBeepOnOrder.Size = new System.Drawing.Size(15, 14);
            this.cbBeepOnOrder.TabIndex = 19;
            this.cbBeepOnOrder.UseVisualStyleBackColor = true;
            this.cbBeepOnOrder.CheckedChanged += new System.EventHandler(this.settingsTextChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 86);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "Beep on order:";
            // 
            // cbDuplicates
            // 
            this.cbDuplicates.AutoSize = true;
            this.cbDuplicates.Location = new System.Drawing.Point(151, 64);
            this.cbDuplicates.Name = "cbDuplicates";
            this.cbDuplicates.Size = new System.Drawing.Size(15, 14);
            this.cbDuplicates.TabIndex = 17;
            this.cbDuplicates.UseVisualStyleBackColor = true;
            this.cbDuplicates.CheckedChanged += new System.EventHandler(this.settingsTextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(9, 64);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(88, 13);
            this.label13.TabIndex = 16;
            this.label13.Text = "Allow Duplicates:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tbCloseHrs);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.tbCloseMins);
            this.groupBox4.Controls.Add(this.tbStartHrs);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.tbStartMins);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Location = new System.Drawing.Point(3, 106);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(200, 100);
            this.groupBox4.TabIndex = 15;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Market Hours";
            // 
            // tbCloseHrs
            // 
            this.tbCloseHrs.Location = new System.Drawing.Point(108, 48);
            this.tbCloseHrs.Mask = "00";
            this.tbCloseHrs.Name = "tbCloseHrs";
            this.tbCloseHrs.PromptChar = ' ';
            this.tbCloseHrs.Size = new System.Drawing.Size(27, 20);
            this.tbCloseHrs.TabIndex = 9;
            this.tbCloseHrs.TextChanged += new System.EventHandler(this.settingsTextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Market opens at:";
            // 
            // tbCloseMins
            // 
            this.tbCloseMins.Location = new System.Drawing.Point(153, 48);
            this.tbCloseMins.Mask = "00";
            this.tbCloseMins.Name = "tbCloseMins";
            this.tbCloseMins.PromptChar = ' ';
            this.tbCloseMins.Size = new System.Drawing.Size(27, 20);
            this.tbCloseMins.TabIndex = 11;
            this.tbCloseMins.TextChanged += new System.EventHandler(this.settingsTextChanged);
            // 
            // tbStartHrs
            // 
            this.tbStartHrs.Location = new System.Drawing.Point(108, 22);
            this.tbStartHrs.Mask = "00";
            this.tbStartHrs.Name = "tbStartHrs";
            this.tbStartHrs.PromptChar = ' ';
            this.tbStartHrs.Size = new System.Drawing.Size(27, 20);
            this.tbStartHrs.TabIndex = 5;
            this.tbStartHrs.TextChanged += new System.EventHandler(this.settingsTextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(141, 51);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(10, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = ":";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(141, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = ":";
            // 
            // tbStartMins
            // 
            this.tbStartMins.Location = new System.Drawing.Point(153, 22);
            this.tbStartMins.Mask = "00";
            this.tbStartMins.Name = "tbStartMins";
            this.tbStartMins.PromptChar = ' ';
            this.tbStartMins.Size = new System.Drawing.Size(27, 20);
            this.tbStartMins.TabIndex = 7;
            this.tbStartMins.TextChanged += new System.EventHandler(this.settingsTextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 51);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Market closes at:";
            // 
            // cbTransmit
            // 
            this.cbTransmit.AutoSize = true;
            this.cbTransmit.Location = new System.Drawing.Point(151, 43);
            this.cbTransmit.Name = "cbTransmit";
            this.cbTransmit.Size = new System.Drawing.Size(15, 14);
            this.cbTransmit.TabIndex = 3;
            this.cbTransmit.UseVisualStyleBackColor = true;
            this.cbTransmit.CheckedChanged += new System.EventHandler(this.settingsTextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 42);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Transmit Orders:";
            // 
            // tbQuoteTimeout
            // 
            this.tbQuoteTimeout.Location = new System.Drawing.Point(149, 12);
            this.tbQuoteTimeout.Mask = "00000";
            this.tbQuoteTimeout.Name = "tbQuoteTimeout";
            this.tbQuoteTimeout.PromptChar = ' ';
            this.tbQuoteTimeout.Size = new System.Drawing.Size(51, 20);
            this.tbQuoteTimeout.TabIndex = 1;
            this.tbQuoteTimeout.Text = "500";
            this.tbQuoteTimeout.TextChanged += new System.EventHandler(this.settingsTextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(127, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "TWS Quote Timeout (ms)";
            // 
            // btnLoginAll
            // 
            this.btnLoginAll.Location = new System.Drawing.Point(3, 4);
            this.btnLoginAll.Name = "btnLoginAll";
            this.btnLoginAll.Size = new System.Drawing.Size(75, 23);
            this.btnLoginAll.TabIndex = 9;
            this.btnLoginAll.Text = "Connect";
            this.btnLoginAll.UseVisualStyleBackColor = true;
            this.btnLoginAll.Click += new System.EventHandler(this.btnLoginAll_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.tradeIdeasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(582, 24);
            this.menuStrip1.TabIndex = 17;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveMenuItem,
            this.loadMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // saveMenuItem
            // 
            this.saveMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("saveMenuItem.Image")));
            this.saveMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveMenuItem.Name = "saveMenuItem";
            this.saveMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.saveMenuItem.Size = new System.Drawing.Size(138, 22);
            this.saveMenuItem.Text = "&Save";
            // 
            // loadMenuItem
            // 
            this.loadMenuItem.Name = "loadMenuItem";
            this.loadMenuItem.Size = new System.Drawing.Size(138, 22);
            this.loadMenuItem.Text = "Load layout";
            // 
            // tradeIdeasToolStripMenuItem
            // 
            this.tradeIdeasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openWindowsMenuItem,
            this.saveWindowsMenuItem,
            this.toolStripSeparator1,
            this.newAlertMenuItem});
            this.tradeIdeasToolStripMenuItem.Name = "tradeIdeasToolStripMenuItem";
            this.tradeIdeasToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.tradeIdeasToolStripMenuItem.Text = "&TradeIdeas";
            // 
            // openWindowsMenuItem
            // 
            this.openWindowsMenuItem.Name = "openWindowsMenuItem";
            this.openWindowsMenuItem.Size = new System.Drawing.Size(184, 22);
            this.openWindowsMenuItem.Text = "Open Alert Windows";
            this.openWindowsMenuItem.Click += new System.EventHandler(this.openWindowsMenuItem_Click);
            // 
            // saveWindowsMenuItem
            // 
            this.saveWindowsMenuItem.Name = "saveWindowsMenuItem";
            this.saveWindowsMenuItem.Size = new System.Drawing.Size(184, 22);
            this.saveWindowsMenuItem.Text = "Save Window Layout";
            this.saveWindowsMenuItem.Click += new System.EventHandler(this.saveWindowsMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // newAlertMenuItem
            // 
            this.newAlertMenuItem.Name = "newAlertMenuItem";
            this.newAlertMenuItem.Size = new System.Drawing.Size(184, 22);
            this.newAlertMenuItem.Text = "New Alert Window";
            this.newAlertMenuItem.Click += new System.EventHandler(this.newAlertMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.axStatusBarX1);
            this.panel1.Controls.Add(this.btnTest);
            this.panel1.Controls.Add(this.btnLoginAll);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(582, 29);
            this.panel1.TabIndex = 18;
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(253, 4);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 21;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // axStatusBarX1
            // 
            this.axStatusBarX1.Location = new System.Drawing.Point(495, 3);
            this.axStatusBarX1.Name = "axStatusBarX1";
            this.axStatusBarX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axStatusBarX1.OcxState")));
            this.axStatusBarX1.Size = new System.Drawing.Size(75, 23);
            this.axStatusBarX1.TabIndex = 22;
            // 
            // axTws1
            // 
            this.axTws1.Enabled = true;
            this.axTws1.Location = new System.Drawing.Point(358, 39);
            this.axTws1.Name = "axTws1";
            this.axTws1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTws1.OcxState")));
            this.axTws1.Size = new System.Drawing.Size(100, 50);
            this.axTws1.TabIndex = 20;
            // 
            // axTwsSim
            // 
            this.axTwsSim.Enabled = true;
            this.axTwsSim.Location = new System.Drawing.Point(373, 124);
            this.axTwsSim.Name = "axTwsSim";
            this.axTwsSim.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTwsSim.OcxState")));
            this.axTwsSim.Size = new System.Drawing.Size(100, 50);
            this.axTwsSim.TabIndex = 21;
            // 
            // axTradeIdeasWindowX1
            // 
            this.axTradeIdeasWindowX1.Location = new System.Drawing.Point(332, 200);
            this.axTradeIdeasWindowX1.Name = "axTradeIdeasWindowX1";
            this.axTradeIdeasWindowX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTradeIdeasWindowX1.OcxState")));
            this.axTradeIdeasWindowX1.Size = new System.Drawing.Size(75, 23);
            this.axTradeIdeasWindowX1.TabIndex = 22;
            this.axTradeIdeasWindowX1.Visible = false;
            // 
            // BotForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(582, 508);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.listBoxOutput);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "BotForm";
            this.Text = "Trade-Ideas Bot";
            this.Load += new System.EventHandler(this.BotForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BotForm_FormClosing);
            this.tabControl.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.strategyGridView)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axStatusBarX1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTws1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTwsSim)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTradeIdeasWindowX1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbTIUser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbTIPassword;
        private System.Windows.Forms.ListBox listBoxOutput;
        private System.Windows.Forms.Button btnAddStrat;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView strategyGridView;
        private System.Windows.Forms.Button btnLoginAll;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbIBHost;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.MaskedTextBox tbIBPortNum;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.MaskedTextBox tbQuoteTimeout;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cbTransmit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox tbStartMins;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MaskedTextBox tbStartHrs;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.MaskedTextBox tbCloseMins;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.MaskedTextBox tbCloseHrs;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbSimHost;
        private System.Windows.Forms.MaskedTextBox tbSimPort;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        //private AxTradeIdeasWindow.AxTradeIdeasWindowX axTradeIdeasWindowX1;
        //private AxTradeIdeasWindow.AxStatusBarX axStatusBarX1;
        private System.Windows.Forms.CheckBox cbDuplicates;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tradeIdeasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openWindowsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveWindowsMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem newAlertMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnOpenAlerts;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox cbBeepOnOrder;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Button btnUp;
		private System.Windows.Forms.Button btnTest;
        private AxTradeIdeasWindow.AxStatusBarX axStatusBarX1;
        private AxTradeIdeasWindow.AxTradeIdeasWindowX axTradeIdeasWindowX1;
        private AxTWSLib.AxTws axTwsSim;
        private AxTWSLib.AxTws axTws1;
    }
}

