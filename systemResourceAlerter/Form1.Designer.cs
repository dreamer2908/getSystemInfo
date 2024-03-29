﻿namespace systemResourceAlerter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sendSystemInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sendResourceStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.updateAppToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chbEmailSsl = new System.Windows.Forms.CheckBox();
            this.chbEmailLogin = new System.Windows.Forms.CheckBox();
            this.txtEmailPassword = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtEmailUser = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtEmailHost = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtEmailFrom = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.numEmailPort = new System.Windows.Forms.NumericUpDown();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chbAutoHide = new System.Windows.Forms.CheckBox();
            this.chbAutoStart = new System.Windows.Forms.CheckBox();
            this.txtEmailSubjectLog = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.txtEmailSubject = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.numDelayBetweenEmails = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.btnReceiverTest = new System.Windows.Forms.Button();
            this.btnReceiverDelete = new System.Windows.Forms.Button();
            this.btnReceiverAdd = new System.Windows.Forms.Button();
            this.lsvReceiver = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chbDailySystemInfoEmailEnable = new System.Windows.Forms.CheckBox();
            this.txtDailySystemInfoEmailTime = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.lblStatusBar = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnCheckOnlineUpdate = new System.Windows.Forms.Button();
            this.btnTestEventLog = new System.Windows.Forms.Button();
            this.lnkEventLogMessageBlackList = new System.Windows.Forms.LinkLabel();
            this.lnkEventLogTaskBlackList = new System.Windows.Forms.LinkLabel();
            this.lnkEventLogIdsBlackList = new System.Windows.Forms.LinkLabel();
            this.lnkEventLogSourceBlackList = new System.Windows.Forms.LinkLabel();
            this.lnkEventLogMessageWhiteList = new System.Windows.Forms.LinkLabel();
            this.lnkEventLogTaskWhiteList = new System.Windows.Forms.LinkLabel();
            this.lnkEventLogIdsWhiteList = new System.Windows.Forms.LinkLabel();
            this.lnkEventLogSourceWhiteList = new System.Windows.Forms.LinkLabel();
            this.chbEventLogLevel4 = new System.Windows.Forms.CheckBox();
            this.chbEventLogCategory4 = new System.Windows.Forms.CheckBox();
            this.chbEventLogLevel3 = new System.Windows.Forms.CheckBox();
            this.chbEventLogLevel2 = new System.Windows.Forms.CheckBox();
            this.chbEventLogCategory3 = new System.Windows.Forms.CheckBox();
            this.chbEventLogLevel1 = new System.Windows.Forms.CheckBox();
            this.chbEventLogMessageBlackList = new System.Windows.Forms.CheckBox();
            this.chbEventLogTaskBlackList = new System.Windows.Forms.CheckBox();
            this.chbEventLogIdsBlackList = new System.Windows.Forms.CheckBox();
            this.chbEventLogSourceBlackList = new System.Windows.Forms.CheckBox();
            this.chbEventLogCategory2 = new System.Windows.Forms.CheckBox();
            this.label21 = new System.Windows.Forms.Label();
            this.chbEventLogMessageWhiteList = new System.Windows.Forms.CheckBox();
            this.chbEventLogTaskWhiteList = new System.Windows.Forms.CheckBox();
            this.chbEventLogIdsWhiteList = new System.Windows.Forms.CheckBox();
            this.chbEventLogSourceWhiteList = new System.Windows.Forms.CheckBox();
            this.label26 = new System.Windows.Forms.Label();
            this.chbEventLogCategory1 = new System.Windows.Forms.CheckBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.chbForwardEventLogs = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.timer4 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numCpuThreshold = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numRamThreshold = new System.Windows.Forms.NumericUpDown();
            this.numMaxHistory = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCpuCurrent = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblCpuAvg = new System.Windows.Forms.Label();
            this.lblRamCurrent = new System.Windows.Forms.Label();
            this.lblRamAvg = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.numCheckLocalDiskUsagePeriod = new System.Windows.Forms.NumericUpDown();
            this.numOtherPartitionUsageThreshold = new System.Windows.Forms.NumericUpDown();
            this.label32 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.numSystemPartitionUsageThreshold = new System.Windows.Forms.NumericUpDown();
            this.label28 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.chbCheckLocalDiskSpaceEnable = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.numDiagnoseLocalDiskHealthPeriod = new System.Windows.Forms.NumericUpDown();
            this.label33 = new System.Windows.Forms.Label();
            this.chbDiagnoseLocalDiskHealthEnable = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.timer5 = new System.Windows.Forms.Timer(this.components);
            this.timer6 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEmailPort)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelayBetweenEmails)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCpuThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRamThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxHistory)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCheckLocalDiskUsagePeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOtherPartitionUsageThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSystemPartitionUsageThreshold)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiagnoseLocalDiskHealthPeriod)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tag = "Check CPU & RAM usage and update UI, warning level";
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 15000;
            this.timer2.Tag = "It starts event log reader";
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "System Resources Alerter";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sendSystemInfoToolStripMenuItem,
            this.sendResourceStatusToolStripMenuItem,
            this.toolStripMenuItem1,
            this.updateAppToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(199, 114);
            // 
            // sendSystemInfoToolStripMenuItem
            // 
            this.sendSystemInfoToolStripMenuItem.Name = "sendSystemInfoToolStripMenuItem";
            this.sendSystemInfoToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.sendSystemInfoToolStripMenuItem.Text = "Send System Info";
            this.sendSystemInfoToolStripMenuItem.Click += new System.EventHandler(this.sendSystemInfoToolStripMenuItem_Click);
            // 
            // sendResourceStatusToolStripMenuItem
            // 
            this.sendResourceStatusToolStripMenuItem.Name = "sendResourceStatusToolStripMenuItem";
            this.sendResourceStatusToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.sendResourceStatusToolStripMenuItem.Text = "Send Resource Status";
            this.sendResourceStatusToolStripMenuItem.Click += new System.EventHandler(this.sendResourceStatusToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(198, 22);
            this.toolStripMenuItem1.Text = "Send Disk Health Status";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // updateAppToolStripMenuItem
            // 
            this.updateAppToolStripMenuItem.Name = "updateAppToolStripMenuItem";
            this.updateAppToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.updateAppToolStripMenuItem.Text = "Update App";
            this.updateAppToolStripMenuItem.Click += new System.EventHandler(this.updateAppToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(198, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chbEmailSsl);
            this.groupBox2.Controls.Add(this.chbEmailLogin);
            this.groupBox2.Controls.Add(this.txtEmailPassword);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.txtEmailUser);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtEmailHost);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtEmailFrom);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.numEmailPort);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(484, 157);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mail Server";
            // 
            // chbEmailSsl
            // 
            this.chbEmailSsl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbEmailSsl.AutoSize = true;
            this.chbEmailSsl.Location = new System.Drawing.Point(432, 64);
            this.chbEmailSsl.Name = "chbEmailSsl";
            this.chbEmailSsl.Size = new System.Drawing.Size(46, 17);
            this.chbEmailSsl.TabIndex = 7;
            this.chbEmailSsl.Text = "SSL";
            this.chbEmailSsl.UseVisualStyleBackColor = true;
            // 
            // chbEmailLogin
            // 
            this.chbEmailLogin.AutoSize = true;
            this.chbEmailLogin.Checked = true;
            this.chbEmailLogin.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbEmailLogin.Location = new System.Drawing.Point(10, 86);
            this.chbEmailLogin.Name = "chbEmailLogin";
            this.chbEmailLogin.Size = new System.Drawing.Size(188, 17);
            this.chbEmailLogin.TabIndex = 8;
            this.chbEmailLogin.Text = "This server requires authentication";
            this.chbEmailLogin.UseVisualStyleBackColor = true;
            // 
            // txtEmailPassword
            // 
            this.txtEmailPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmailPassword.Location = new System.Drawing.Point(90, 127);
            this.txtEmailPassword.Name = "txtEmailPassword";
            this.txtEmailPassword.PasswordChar = '*';
            this.txtEmailPassword.Size = new System.Drawing.Size(388, 20);
            this.txtEmailPassword.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(28, 130);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(56, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "Password:";
            // 
            // txtEmailUser
            // 
            this.txtEmailUser.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmailUser.Location = new System.Drawing.Point(90, 105);
            this.txtEmailUser.Name = "txtEmailUser";
            this.txtEmailUser.Size = new System.Drawing.Size(388, 20);
            this.txtEmailUser.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(28, 108);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(58, 13);
            this.label12.TabIndex = 0;
            this.label12.Text = "Username:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 64);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Port:";
            // 
            // txtEmailHost
            // 
            this.txtEmailHost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmailHost.Location = new System.Drawing.Point(90, 39);
            this.txtEmailHost.Name = "txtEmailHost";
            this.txtEmailHost.Size = new System.Drawing.Size(388, 20);
            this.txtEmailHost.TabIndex = 5;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "SMTP Server:";
            // 
            // txtEmailFrom
            // 
            this.txtEmailFrom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmailFrom.Location = new System.Drawing.Point(90, 17);
            this.txtEmailFrom.Name = "txtEmailFrom";
            this.txtEmailFrom.Size = new System.Drawing.Size(388, 20);
            this.txtEmailFrom.TabIndex = 4;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(7, 20);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 13);
            this.label15.TabIndex = 0;
            this.label15.Text = "Sender";
            // 
            // numEmailPort
            // 
            this.numEmailPort.Location = new System.Drawing.Point(90, 63);
            this.numEmailPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numEmailPort.Name = "numEmailPort";
            this.numEmailPort.Size = new System.Drawing.Size(59, 20);
            this.numEmailPort.TabIndex = 6;
            this.numEmailPort.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.chbAutoHide);
            this.groupBox3.Controls.Add(this.chbAutoStart);
            this.groupBox3.Controls.Add(this.txtEmailSubjectLog);
            this.groupBox3.Controls.Add(this.label22);
            this.groupBox3.Controls.Add(this.txtEmailSubject);
            this.groupBox3.Controls.Add(this.label19);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.numDelayBetweenEmails);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.btnReceiverTest);
            this.groupBox3.Controls.Add(this.btnReceiverDelete);
            this.groupBox3.Controls.Add(this.btnReceiverAdd);
            this.groupBox3.Controls.Add(this.lsvReceiver);
            this.groupBox3.Location = new System.Drawing.Point(6, 169);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(484, 255);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Email";
            // 
            // chbAutoHide
            // 
            this.chbAutoHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbAutoHide.AutoSize = true;
            this.chbAutoHide.Location = new System.Drawing.Point(403, 154);
            this.chbAutoHide.Name = "chbAutoHide";
            this.chbAutoHide.Size = new System.Drawing.Size(73, 17);
            this.chbAutoHide.TabIndex = 17;
            this.chbAutoHide.Text = "Auto Hide";
            this.chbAutoHide.UseVisualStyleBackColor = true;
            // 
            // chbAutoStart
            // 
            this.chbAutoStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbAutoStart.AutoSize = true;
            this.chbAutoStart.Location = new System.Drawing.Point(404, 131);
            this.chbAutoStart.Name = "chbAutoStart";
            this.chbAutoStart.Size = new System.Drawing.Size(73, 17);
            this.chbAutoStart.TabIndex = 16;
            this.chbAutoStart.Text = "Auto Start";
            this.chbAutoStart.UseVisualStyleBackColor = true;
            // 
            // txtEmailSubjectLog
            // 
            this.txtEmailSubjectLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmailSubjectLog.Location = new System.Drawing.Point(168, 228);
            this.txtEmailSubjectLog.Name = "txtEmailSubjectLog";
            this.txtEmailSubjectLog.Size = new System.Drawing.Size(229, 20);
            this.txtEmailSubjectLog.TabIndex = 18;
            // 
            // label22
            // 
            this.label22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(7, 231);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(121, 13);
            this.label22.TabIndex = 6;
            this.label22.Text = "Email Subject (for Logs):";
            // 
            // txtEmailSubject
            // 
            this.txtEmailSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmailSubject.Location = new System.Drawing.Point(168, 202);
            this.txtEmailSubject.Name = "txtEmailSubject";
            this.txtEmailSubject.Size = new System.Drawing.Size(229, 20);
            this.txtEmailSubject.TabIndex = 18;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(7, 205);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(149, 13);
            this.label19.TabIndex = 6;
            this.label19.Text = "Email Subject (for Resources):";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(7, 20);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(137, 13);
            this.label18.TabIndex = 5;
            this.label18.Text = "Receipent email addresses:";
            // 
            // label17
            // 
            this.label17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(431, 20);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "seconds";
            // 
            // numDelayBetweenEmails
            // 
            this.numDelayBetweenEmails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numDelayBetweenEmails.Location = new System.Drawing.Point(359, 18);
            this.numDelayBetweenEmails.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numDelayBetweenEmails.Name = "numDelayBetweenEmails";
            this.numDelayBetweenEmails.Size = new System.Drawing.Size(66, 20);
            this.numDelayBetweenEmails.TabIndex = 11;
            this.numDelayBetweenEmails.Value = new decimal(new int[] {
            900,
            0,
            0,
            0});
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(202, 20);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(156, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Delay between resource alerts: ";
            // 
            // btnReceiverTest
            // 
            this.btnReceiverTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReceiverTest.Location = new System.Drawing.Point(403, 101);
            this.btnReceiverTest.Name = "btnReceiverTest";
            this.btnReceiverTest.Size = new System.Drawing.Size(75, 23);
            this.btnReceiverTest.TabIndex = 15;
            this.btnReceiverTest.Text = "Test";
            this.btnReceiverTest.UseVisualStyleBackColor = true;
            this.btnReceiverTest.Click += new System.EventHandler(this.btnReceiverTest_Click);
            // 
            // btnReceiverDelete
            // 
            this.btnReceiverDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReceiverDelete.Location = new System.Drawing.Point(403, 72);
            this.btnReceiverDelete.Name = "btnReceiverDelete";
            this.btnReceiverDelete.Size = new System.Drawing.Size(75, 23);
            this.btnReceiverDelete.TabIndex = 14;
            this.btnReceiverDelete.Text = "Delete";
            this.btnReceiverDelete.UseVisualStyleBackColor = true;
            this.btnReceiverDelete.Click += new System.EventHandler(this.btnReceiverDelete_Click);
            // 
            // btnReceiverAdd
            // 
            this.btnReceiverAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReceiverAdd.Location = new System.Drawing.Point(403, 43);
            this.btnReceiverAdd.Name = "btnReceiverAdd";
            this.btnReceiverAdd.Size = new System.Drawing.Size(75, 23);
            this.btnReceiverAdd.TabIndex = 13;
            this.btnReceiverAdd.Text = "Add...";
            this.btnReceiverAdd.UseVisualStyleBackColor = true;
            this.btnReceiverAdd.Click += new System.EventHandler(this.btnReceiverAdd_Click);
            // 
            // lsvReceiver
            // 
            this.lsvReceiver.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvReceiver.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.lsvReceiver.FullRowSelect = true;
            this.lsvReceiver.GridLines = true;
            this.lsvReceiver.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvReceiver.HideSelection = false;
            this.lsvReceiver.Location = new System.Drawing.Point(7, 44);
            this.lsvReceiver.Name = "lsvReceiver";
            this.lsvReceiver.Size = new System.Drawing.Size(390, 152);
            this.lsvReceiver.TabIndex = 12;
            this.lsvReceiver.UseCompatibleStateImageBehavior = false;
            this.lsvReceiver.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 330;
            // 
            // chbDailySystemInfoEmailEnable
            // 
            this.chbDailySystemInfoEmailEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbDailySystemInfoEmailEnable.AutoSize = true;
            this.chbDailySystemInfoEmailEnable.Location = new System.Drawing.Point(3, 407);
            this.chbDailySystemInfoEmailEnable.Name = "chbDailySystemInfoEmailEnable";
            this.chbDailySystemInfoEmailEnable.Size = new System.Drawing.Size(294, 17);
            this.chbDailySystemInfoEmailEnable.TabIndex = 19;
            this.chbDailySystemInfoEmailEnable.Text = "Send general system information daily at (hh:mm,[hh:mm])";
            this.chbDailySystemInfoEmailEnable.UseVisualStyleBackColor = true;
            // 
            // txtDailySystemInfoEmailTime
            // 
            this.txtDailySystemInfoEmailTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDailySystemInfoEmailTime.Location = new System.Drawing.Point(300, 405);
            this.txtDailySystemInfoEmailTime.Name = "txtDailySystemInfoEmailTime";
            this.txtDailySystemInfoEmailTime.Size = new System.Drawing.Size(190, 20);
            this.txtDailySystemInfoEmailTime.TabIndex = 18;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(441, 475);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "&Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnHide
            // 
            this.btnHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHide.Location = new System.Drawing.Point(360, 475);
            this.btnHide.Name = "btnHide";
            this.btnHide.Size = new System.Drawing.Size(75, 23);
            this.btnHide.TabIndex = 5;
            this.btnHide.Text = "&Hide";
            this.btnHide.UseVisualStyleBackColor = true;
            this.btnHide.Click += new System.EventHandler(this.btnHide_Click);
            // 
            // btnStartStop
            // 
            this.btnStartStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStartStop.Location = new System.Drawing.Point(279, 475);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(75, 23);
            this.btnStartStop.TabIndex = 4;
            this.btnStartStop.Text = "&Start";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // btnApply
            // 
            this.btnApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnApply.Location = new System.Drawing.Point(198, 475);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lblStatusBar
            // 
            this.lblStatusBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatusBar.AutoSize = true;
            this.lblStatusBar.Location = new System.Drawing.Point(20, 485);
            this.lblStatusBar.Name = "lblStatusBar";
            this.lblStatusBar.Size = new System.Drawing.Size(37, 13);
            this.lblStatusBar.TabIndex = 7;
            this.lblStatusBar.Text = "Status";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.btnCheckOnlineUpdate);
            this.groupBox4.Controls.Add(this.btnTestEventLog);
            this.groupBox4.Controls.Add(this.lnkEventLogMessageBlackList);
            this.groupBox4.Controls.Add(this.lnkEventLogTaskBlackList);
            this.groupBox4.Controls.Add(this.lnkEventLogIdsBlackList);
            this.groupBox4.Controls.Add(this.lnkEventLogSourceBlackList);
            this.groupBox4.Controls.Add(this.lnkEventLogMessageWhiteList);
            this.groupBox4.Controls.Add(this.lnkEventLogTaskWhiteList);
            this.groupBox4.Controls.Add(this.lnkEventLogIdsWhiteList);
            this.groupBox4.Controls.Add(this.lnkEventLogSourceWhiteList);
            this.groupBox4.Controls.Add(this.chbEventLogLevel4);
            this.groupBox4.Controls.Add(this.chbEventLogCategory4);
            this.groupBox4.Controls.Add(this.chbEventLogLevel3);
            this.groupBox4.Controls.Add(this.chbEventLogLevel2);
            this.groupBox4.Controls.Add(this.chbEventLogCategory3);
            this.groupBox4.Controls.Add(this.chbEventLogLevel1);
            this.groupBox4.Controls.Add(this.chbEventLogMessageBlackList);
            this.groupBox4.Controls.Add(this.chbEventLogTaskBlackList);
            this.groupBox4.Controls.Add(this.chbEventLogIdsBlackList);
            this.groupBox4.Controls.Add(this.chbEventLogSourceBlackList);
            this.groupBox4.Controls.Add(this.chbEventLogCategory2);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.chbEventLogMessageWhiteList);
            this.groupBox4.Controls.Add(this.chbEventLogTaskWhiteList);
            this.groupBox4.Controls.Add(this.chbEventLogIdsWhiteList);
            this.groupBox4.Controls.Add(this.chbEventLogSourceWhiteList);
            this.groupBox4.Controls.Add(this.label26);
            this.groupBox4.Controls.Add(this.chbEventLogCategory1);
            this.groupBox4.Controls.Add(this.label25);
            this.groupBox4.Controls.Add(this.label24);
            this.groupBox4.Controls.Add(this.label23);
            this.groupBox4.Controls.Add(this.label20);
            this.groupBox4.Controls.Add(this.chbForwardEventLogs);
            this.groupBox4.Location = new System.Drawing.Point(6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(484, 185);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Windows Event Logs";
            // 
            // btnCheckOnlineUpdate
            // 
            this.btnCheckOnlineUpdate.Location = new System.Drawing.Point(388, 142);
            this.btnCheckOnlineUpdate.Name = "btnCheckOnlineUpdate";
            this.btnCheckOnlineUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnCheckOnlineUpdate.TabIndex = 5;
            this.btnCheckOnlineUpdate.Text = "Update";
            this.btnCheckOnlineUpdate.UseVisualStyleBackColor = true;
            this.btnCheckOnlineUpdate.Visible = false;
            this.btnCheckOnlineUpdate.Click += new System.EventHandler(this.btnCheckOnlineUpdate_Click);
            // 
            // btnTestEventLog
            // 
            this.btnTestEventLog.Location = new System.Drawing.Point(388, 112);
            this.btnTestEventLog.Name = "btnTestEventLog";
            this.btnTestEventLog.Size = new System.Drawing.Size(75, 23);
            this.btnTestEventLog.TabIndex = 4;
            this.btnTestEventLog.Text = "Test";
            this.btnTestEventLog.UseVisualStyleBackColor = true;
            this.btnTestEventLog.Visible = false;
            this.btnTestEventLog.Click += new System.EventHandler(this.btnTestEventLog_Click);
            // 
            // lnkEventLogMessageBlackList
            // 
            this.lnkEventLogMessageBlackList.AutoSize = true;
            this.lnkEventLogMessageBlackList.Location = new System.Drawing.Point(213, 159);
            this.lnkEventLogMessageBlackList.Name = "lnkEventLogMessageBlackList";
            this.lnkEventLogMessageBlackList.Size = new System.Drawing.Size(49, 13);
            this.lnkEventLogMessageBlackList.TabIndex = 3;
            this.lnkEventLogMessageBlackList.TabStop = true;
            this.lnkEventLogMessageBlackList.Text = "Black list";
            this.lnkEventLogMessageBlackList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEventLogMessageBlackList_LinkClicked);
            // 
            // lnkEventLogTaskBlackList
            // 
            this.lnkEventLogTaskBlackList.AutoSize = true;
            this.lnkEventLogTaskBlackList.Location = new System.Drawing.Point(213, 136);
            this.lnkEventLogTaskBlackList.Name = "lnkEventLogTaskBlackList";
            this.lnkEventLogTaskBlackList.Size = new System.Drawing.Size(49, 13);
            this.lnkEventLogTaskBlackList.TabIndex = 3;
            this.lnkEventLogTaskBlackList.TabStop = true;
            this.lnkEventLogTaskBlackList.Text = "Black list";
            this.lnkEventLogTaskBlackList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEventLogTaskBlackList_LinkClicked);
            // 
            // lnkEventLogIdsBlackList
            // 
            this.lnkEventLogIdsBlackList.AutoSize = true;
            this.lnkEventLogIdsBlackList.Location = new System.Drawing.Point(213, 113);
            this.lnkEventLogIdsBlackList.Name = "lnkEventLogIdsBlackList";
            this.lnkEventLogIdsBlackList.Size = new System.Drawing.Size(49, 13);
            this.lnkEventLogIdsBlackList.TabIndex = 3;
            this.lnkEventLogIdsBlackList.TabStop = true;
            this.lnkEventLogIdsBlackList.Text = "Black list";
            this.lnkEventLogIdsBlackList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEventLogIdsBlackList_LinkClicked);
            // 
            // lnkEventLogSourceBlackList
            // 
            this.lnkEventLogSourceBlackList.AutoSize = true;
            this.lnkEventLogSourceBlackList.Location = new System.Drawing.Point(213, 90);
            this.lnkEventLogSourceBlackList.Name = "lnkEventLogSourceBlackList";
            this.lnkEventLogSourceBlackList.Size = new System.Drawing.Size(49, 13);
            this.lnkEventLogSourceBlackList.TabIndex = 3;
            this.lnkEventLogSourceBlackList.TabStop = true;
            this.lnkEventLogSourceBlackList.Text = "Black list";
            this.lnkEventLogSourceBlackList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEventLogSourceBlackList_LinkClicked);
            // 
            // lnkEventLogMessageWhiteList
            // 
            this.lnkEventLogMessageWhiteList.AutoSize = true;
            this.lnkEventLogMessageWhiteList.Location = new System.Drawing.Point(127, 159);
            this.lnkEventLogMessageWhiteList.Name = "lnkEventLogMessageWhiteList";
            this.lnkEventLogMessageWhiteList.Size = new System.Drawing.Size(50, 13);
            this.lnkEventLogMessageWhiteList.TabIndex = 3;
            this.lnkEventLogMessageWhiteList.TabStop = true;
            this.lnkEventLogMessageWhiteList.Text = "White list";
            this.lnkEventLogMessageWhiteList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEventLogMessageWhiteList_LinkClicked);
            // 
            // lnkEventLogTaskWhiteList
            // 
            this.lnkEventLogTaskWhiteList.AutoSize = true;
            this.lnkEventLogTaskWhiteList.Location = new System.Drawing.Point(127, 136);
            this.lnkEventLogTaskWhiteList.Name = "lnkEventLogTaskWhiteList";
            this.lnkEventLogTaskWhiteList.Size = new System.Drawing.Size(50, 13);
            this.lnkEventLogTaskWhiteList.TabIndex = 3;
            this.lnkEventLogTaskWhiteList.TabStop = true;
            this.lnkEventLogTaskWhiteList.Text = "White list";
            this.lnkEventLogTaskWhiteList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEventLogTaskWhiteList_LinkClicked);
            // 
            // lnkEventLogIdsWhiteList
            // 
            this.lnkEventLogIdsWhiteList.AutoSize = true;
            this.lnkEventLogIdsWhiteList.Location = new System.Drawing.Point(127, 113);
            this.lnkEventLogIdsWhiteList.Name = "lnkEventLogIdsWhiteList";
            this.lnkEventLogIdsWhiteList.Size = new System.Drawing.Size(50, 13);
            this.lnkEventLogIdsWhiteList.TabIndex = 3;
            this.lnkEventLogIdsWhiteList.TabStop = true;
            this.lnkEventLogIdsWhiteList.Text = "White list";
            this.lnkEventLogIdsWhiteList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEventLogIdsWhiteList_LinkClicked);
            // 
            // lnkEventLogSourceWhiteList
            // 
            this.lnkEventLogSourceWhiteList.AutoSize = true;
            this.lnkEventLogSourceWhiteList.Location = new System.Drawing.Point(127, 90);
            this.lnkEventLogSourceWhiteList.Name = "lnkEventLogSourceWhiteList";
            this.lnkEventLogSourceWhiteList.Size = new System.Drawing.Size(50, 13);
            this.lnkEventLogSourceWhiteList.TabIndex = 3;
            this.lnkEventLogSourceWhiteList.TabStop = true;
            this.lnkEventLogSourceWhiteList.Text = "White list";
            this.lnkEventLogSourceWhiteList.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEventLogSourceWhiteList_LinkClicked);
            // 
            // chbEventLogLevel4
            // 
            this.chbEventLogLevel4.AutoSize = true;
            this.chbEventLogLevel4.Location = new System.Drawing.Point(369, 66);
            this.chbEventLogLevel4.Name = "chbEventLogLevel4";
            this.chbEventLogLevel4.Size = new System.Drawing.Size(50, 17);
            this.chbEventLogLevel4.TabIndex = 2;
            this.chbEventLogLevel4.Text = "Audit";
            this.chbEventLogLevel4.UseVisualStyleBackColor = true;
            // 
            // chbEventLogCategory4
            // 
            this.chbEventLogCategory4.AutoSize = true;
            this.chbEventLogCategory4.Location = new System.Drawing.Point(369, 43);
            this.chbEventLogCategory4.Name = "chbEventLogCategory4";
            this.chbEventLogCategory4.Size = new System.Drawing.Size(60, 17);
            this.chbEventLogCategory4.TabIndex = 2;
            this.chbEventLogCategory4.Text = "System";
            this.chbEventLogCategory4.UseVisualStyleBackColor = true;
            // 
            // chbEventLogLevel3
            // 
            this.chbEventLogLevel3.AutoSize = true;
            this.chbEventLogLevel3.Location = new System.Drawing.Point(283, 66);
            this.chbEventLogLevel3.Name = "chbEventLogLevel3";
            this.chbEventLogLevel3.Size = new System.Drawing.Size(78, 17);
            this.chbEventLogLevel3.TabIndex = 2;
            this.chbEventLogLevel3.Text = "Information";
            this.chbEventLogLevel3.UseVisualStyleBackColor = true;
            // 
            // chbEventLogLevel2
            // 
            this.chbEventLogLevel2.AutoSize = true;
            this.chbEventLogLevel2.Location = new System.Drawing.Point(197, 66);
            this.chbEventLogLevel2.Name = "chbEventLogLevel2";
            this.chbEventLogLevel2.Size = new System.Drawing.Size(66, 17);
            this.chbEventLogLevel2.TabIndex = 2;
            this.chbEventLogLevel2.Text = "Warning";
            this.chbEventLogLevel2.UseVisualStyleBackColor = true;
            // 
            // chbEventLogCategory3
            // 
            this.chbEventLogCategory3.AutoSize = true;
            this.chbEventLogCategory3.Location = new System.Drawing.Point(283, 43);
            this.chbEventLogCategory3.Name = "chbEventLogCategory3";
            this.chbEventLogCategory3.Size = new System.Drawing.Size(54, 17);
            this.chbEventLogCategory3.TabIndex = 2;
            this.chbEventLogCategory3.Text = "Setup";
            this.chbEventLogCategory3.UseVisualStyleBackColor = true;
            // 
            // chbEventLogLevel1
            // 
            this.chbEventLogLevel1.AutoSize = true;
            this.chbEventLogLevel1.Location = new System.Drawing.Point(111, 66);
            this.chbEventLogLevel1.Name = "chbEventLogLevel1";
            this.chbEventLogLevel1.Size = new System.Drawing.Size(48, 17);
            this.chbEventLogLevel1.TabIndex = 2;
            this.chbEventLogLevel1.Text = "Error";
            this.chbEventLogLevel1.UseVisualStyleBackColor = true;
            // 
            // chbEventLogMessageBlackList
            // 
            this.chbEventLogMessageBlackList.AutoSize = true;
            this.chbEventLogMessageBlackList.Location = new System.Drawing.Point(197, 158);
            this.chbEventLogMessageBlackList.Name = "chbEventLogMessageBlackList";
            this.chbEventLogMessageBlackList.Size = new System.Drawing.Size(15, 14);
            this.chbEventLogMessageBlackList.TabIndex = 2;
            this.chbEventLogMessageBlackList.UseVisualStyleBackColor = true;
            // 
            // chbEventLogTaskBlackList
            // 
            this.chbEventLogTaskBlackList.AutoSize = true;
            this.chbEventLogTaskBlackList.Location = new System.Drawing.Point(197, 135);
            this.chbEventLogTaskBlackList.Name = "chbEventLogTaskBlackList";
            this.chbEventLogTaskBlackList.Size = new System.Drawing.Size(15, 14);
            this.chbEventLogTaskBlackList.TabIndex = 2;
            this.chbEventLogTaskBlackList.UseVisualStyleBackColor = true;
            // 
            // chbEventLogIdsBlackList
            // 
            this.chbEventLogIdsBlackList.AutoSize = true;
            this.chbEventLogIdsBlackList.Location = new System.Drawing.Point(197, 112);
            this.chbEventLogIdsBlackList.Name = "chbEventLogIdsBlackList";
            this.chbEventLogIdsBlackList.Size = new System.Drawing.Size(15, 14);
            this.chbEventLogIdsBlackList.TabIndex = 2;
            this.chbEventLogIdsBlackList.UseVisualStyleBackColor = true;
            // 
            // chbEventLogSourceBlackList
            // 
            this.chbEventLogSourceBlackList.AutoSize = true;
            this.chbEventLogSourceBlackList.Location = new System.Drawing.Point(197, 89);
            this.chbEventLogSourceBlackList.Name = "chbEventLogSourceBlackList";
            this.chbEventLogSourceBlackList.Size = new System.Drawing.Size(15, 14);
            this.chbEventLogSourceBlackList.TabIndex = 2;
            this.chbEventLogSourceBlackList.UseVisualStyleBackColor = true;
            // 
            // chbEventLogCategory2
            // 
            this.chbEventLogCategory2.AutoSize = true;
            this.chbEventLogCategory2.Location = new System.Drawing.Point(197, 43);
            this.chbEventLogCategory2.Name = "chbEventLogCategory2";
            this.chbEventLogCategory2.Size = new System.Drawing.Size(64, 17);
            this.chbEventLogCategory2.TabIndex = 2;
            this.chbEventLogCategory2.Text = "Security";
            this.chbEventLogCategory2.UseVisualStyleBackColor = true;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(33, 67);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(41, 13);
            this.label21.TabIndex = 1;
            this.label21.Text = "Levels:";
            // 
            // chbEventLogMessageWhiteList
            // 
            this.chbEventLogMessageWhiteList.AutoSize = true;
            this.chbEventLogMessageWhiteList.Location = new System.Drawing.Point(111, 158);
            this.chbEventLogMessageWhiteList.Name = "chbEventLogMessageWhiteList";
            this.chbEventLogMessageWhiteList.Size = new System.Drawing.Size(15, 14);
            this.chbEventLogMessageWhiteList.TabIndex = 2;
            this.chbEventLogMessageWhiteList.UseVisualStyleBackColor = true;
            // 
            // chbEventLogTaskWhiteList
            // 
            this.chbEventLogTaskWhiteList.AutoSize = true;
            this.chbEventLogTaskWhiteList.Location = new System.Drawing.Point(111, 135);
            this.chbEventLogTaskWhiteList.Name = "chbEventLogTaskWhiteList";
            this.chbEventLogTaskWhiteList.Size = new System.Drawing.Size(15, 14);
            this.chbEventLogTaskWhiteList.TabIndex = 2;
            this.chbEventLogTaskWhiteList.UseVisualStyleBackColor = true;
            // 
            // chbEventLogIdsWhiteList
            // 
            this.chbEventLogIdsWhiteList.AutoSize = true;
            this.chbEventLogIdsWhiteList.Location = new System.Drawing.Point(111, 112);
            this.chbEventLogIdsWhiteList.Name = "chbEventLogIdsWhiteList";
            this.chbEventLogIdsWhiteList.Size = new System.Drawing.Size(15, 14);
            this.chbEventLogIdsWhiteList.TabIndex = 2;
            this.chbEventLogIdsWhiteList.UseVisualStyleBackColor = true;
            // 
            // chbEventLogSourceWhiteList
            // 
            this.chbEventLogSourceWhiteList.AutoSize = true;
            this.chbEventLogSourceWhiteList.Location = new System.Drawing.Point(111, 89);
            this.chbEventLogSourceWhiteList.Name = "chbEventLogSourceWhiteList";
            this.chbEventLogSourceWhiteList.Size = new System.Drawing.Size(15, 14);
            this.chbEventLogSourceWhiteList.TabIndex = 2;
            this.chbEventLogSourceWhiteList.UseVisualStyleBackColor = true;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(33, 159);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(53, 13);
            this.label26.TabIndex = 1;
            this.label26.Text = "Message:";
            // 
            // chbEventLogCategory1
            // 
            this.chbEventLogCategory1.AutoSize = true;
            this.chbEventLogCategory1.Location = new System.Drawing.Point(111, 43);
            this.chbEventLogCategory1.Name = "chbEventLogCategory1";
            this.chbEventLogCategory1.Size = new System.Drawing.Size(78, 17);
            this.chbEventLogCategory1.TabIndex = 2;
            this.chbEventLogCategory1.Text = "Application";
            this.chbEventLogCategory1.UseVisualStyleBackColor = true;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(33, 136);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(39, 13);
            this.label25.TabIndex = 1;
            this.label25.Text = "Tasks:";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(33, 113);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(57, 13);
            this.label24.TabIndex = 1;
            this.label24.Text = "Event IDs:";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(33, 90);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(49, 13);
            this.label23.TabIndex = 1;
            this.label23.Text = "Sources:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(33, 44);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(60, 13);
            this.label20.TabIndex = 1;
            this.label20.Text = "Categories:";
            // 
            // chbForwardEventLogs
            // 
            this.chbForwardEventLogs.AutoSize = true;
            this.chbForwardEventLogs.Location = new System.Drawing.Point(10, 19);
            this.chbForwardEventLogs.Name = "chbForwardEventLogs";
            this.chbForwardEventLogs.Size = new System.Drawing.Size(155, 17);
            this.chbForwardEventLogs.TabIndex = 0;
            this.chbForwardEventLogs.Text = "Forward event logs to email";
            this.chbForwardEventLogs.UseVisualStyleBackColor = true;
            this.chbForwardEventLogs.CheckedChanged += new System.EventHandler(this.chbForwardEventLogs_CheckedChanged);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // timer3
            // 
            this.timer3.Interval = 15000;
            this.timer3.Tag = "It runs the daily general system info email";
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // timer4
            // 
            this.timer4.Interval = 900000;
            this.timer4.Tag = "Online updater";
            this.timer4.Tick += new System.EventHandler(this.timer4_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "CPU Threshold:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "RAM Threshold:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(176, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(15, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "%";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Averaging Period:";
            // 
            // numCpuThreshold
            // 
            this.numCpuThreshold.Location = new System.Drawing.Point(111, 70);
            this.numCpuThreshold.Name = "numCpuThreshold";
            this.numCpuThreshold.Size = new System.Drawing.Size(59, 20);
            this.numCpuThreshold.TabIndex = 1;
            this.numCpuThreshold.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(176, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(15, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "%";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(176, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "sec";
            // 
            // numRamThreshold
            // 
            this.numRamThreshold.Location = new System.Drawing.Point(111, 96);
            this.numRamThreshold.Name = "numRamThreshold";
            this.numRamThreshold.Size = new System.Drawing.Size(59, 20);
            this.numRamThreshold.TabIndex = 2;
            this.numRamThreshold.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // numMaxHistory
            // 
            this.numMaxHistory.Location = new System.Drawing.Point(111, 122);
            this.numMaxHistory.Maximum = new decimal(new int[] {
            86400,
            0,
            0,
            0});
            this.numMaxHistory.Name = "numMaxHistory";
            this.numMaxHistory.Size = new System.Drawing.Size(59, 20);
            this.numMaxHistory.TabIndex = 3;
            this.numMaxHistory.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "CPU Usage:";
            // 
            // lblCpuCurrent
            // 
            this.lblCpuCurrent.AutoSize = true;
            this.lblCpuCurrent.Location = new System.Drawing.Point(82, 20);
            this.lblCpuCurrent.Name = "lblCpuCurrent";
            this.lblCpuCurrent.Size = new System.Drawing.Size(42, 13);
            this.lblCpuCurrent.TabIndex = 3;
            this.lblCpuCurrent.Text = "98.76%";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "RAM Usage:";
            // 
            // lblCpuAvg
            // 
            this.lblCpuAvg.AutoSize = true;
            this.lblCpuAvg.Location = new System.Drawing.Point(165, 20);
            this.lblCpuAvg.Name = "lblCpuAvg";
            this.lblCpuAvg.Size = new System.Drawing.Size(42, 13);
            this.lblCpuAvg.TabIndex = 3;
            this.lblCpuAvg.Text = "98.76%";
            // 
            // lblRamCurrent
            // 
            this.lblRamCurrent.AutoSize = true;
            this.lblRamCurrent.Location = new System.Drawing.Point(82, 42);
            this.lblRamCurrent.Name = "lblRamCurrent";
            this.lblRamCurrent.Size = new System.Drawing.Size(42, 13);
            this.lblRamCurrent.TabIndex = 3;
            this.lblRamCurrent.Text = "98.76%";
            // 
            // lblRamAvg
            // 
            this.lblRamAvg.AutoSize = true;
            this.lblRamAvg.Location = new System.Drawing.Point(165, 42);
            this.lblRamAvg.Name = "lblRamAvg";
            this.lblRamAvg.Size = new System.Drawing.Size(42, 13);
            this.lblRamAvg.TabIndex = 3;
            this.lblRamAvg.Text = "98.76%";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(130, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Avg:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(130, 42);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "Avg:";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(13, 13);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(504, 456);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Transparent;
            this.tabPage3.Controls.Add(this.groupBox2);
            this.tabPage3.Controls.Add(this.groupBox3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(496, 430);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Email";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Transparent;
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.groupBox5);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.chbDailySystemInfoEmailEnable);
            this.tabPage1.Controls.Add(this.txtDailySystemInfoEmailTime);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(496, 430);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Resources";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.numCheckLocalDiskUsagePeriod);
            this.groupBox6.Controls.Add(this.numOtherPartitionUsageThreshold);
            this.groupBox6.Controls.Add(this.label32);
            this.groupBox6.Controls.Add(this.label27);
            this.groupBox6.Controls.Add(this.numSystemPartitionUsageThreshold);
            this.groupBox6.Controls.Add(this.label28);
            this.groupBox6.Controls.Add(this.label31);
            this.groupBox6.Controls.Add(this.label29);
            this.groupBox6.Controls.Add(this.label30);
            this.groupBox6.Controls.Add(this.chbCheckLocalDiskSpaceEnable);
            this.groupBox6.Location = new System.Drawing.Point(7, 170);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(483, 124);
            this.groupBox6.TabIndex = 21;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Storage Space";
            // 
            // numCheckLocalDiskUsagePeriod
            // 
            this.numCheckLocalDiskUsagePeriod.Location = new System.Drawing.Point(160, 95);
            this.numCheckLocalDiskUsagePeriod.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numCheckLocalDiskUsagePeriod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCheckLocalDiskUsagePeriod.Name = "numCheckLocalDiskUsagePeriod";
            this.numCheckLocalDiskUsagePeriod.Size = new System.Drawing.Size(59, 20);
            this.numCheckLocalDiskUsagePeriod.TabIndex = 8;
            this.numCheckLocalDiskUsagePeriod.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // numOtherPartitionUsageThreshold
            // 
            this.numOtherPartitionUsageThreshold.Location = new System.Drawing.Point(160, 69);
            this.numOtherPartitionUsageThreshold.Name = "numOtherPartitionUsageThreshold";
            this.numOtherPartitionUsageThreshold.Size = new System.Drawing.Size(59, 20);
            this.numOtherPartitionUsageThreshold.TabIndex = 8;
            this.numOtherPartitionUsageThreshold.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(225, 97);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(43, 13);
            this.label32.TabIndex = 3;
            this.label32.Text = "minutes";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(225, 71);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(15, 13);
            this.label27.TabIndex = 3;
            this.label27.Text = "%";
            // 
            // numSystemPartitionUsageThreshold
            // 
            this.numSystemPartitionUsageThreshold.Location = new System.Drawing.Point(160, 43);
            this.numSystemPartitionUsageThreshold.Name = "numSystemPartitionUsageThreshold";
            this.numSystemPartitionUsageThreshold.Size = new System.Drawing.Size(59, 20);
            this.numSystemPartitionUsageThreshold.TabIndex = 7;
            this.numSystemPartitionUsageThreshold.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(225, 45);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(15, 13);
            this.label28.TabIndex = 4;
            this.label28.Text = "%";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(7, 97);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(149, 13);
            this.label31.TabIndex = 5;
            this.label31.Text = "Check the space usage every";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(7, 71);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(144, 13);
            this.label29.TabIndex = 5;
            this.label29.Text = "Threshold for other partitions:";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(7, 45);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(147, 13);
            this.label30.TabIndex = 6;
            this.label30.Text = "Threshold for system partition:";
            // 
            // chbCheckLocalDiskSpaceEnable
            // 
            this.chbCheckLocalDiskSpaceEnable.AutoSize = true;
            this.chbCheckLocalDiskSpaceEnable.Checked = true;
            this.chbCheckLocalDiskSpaceEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbCheckLocalDiskSpaceEnable.Location = new System.Drawing.Point(7, 20);
            this.chbCheckLocalDiskSpaceEnable.Name = "chbCheckLocalDiskSpaceEnable";
            this.chbCheckLocalDiskSpaceEnable.Size = new System.Drawing.Size(298, 17);
            this.chbCheckLocalDiskSpaceEnable.TabIndex = 0;
            this.chbCheckLocalDiskSpaceEnable.Text = "Warn if local disks\' used space exceeds these thresholds:";
            this.chbCheckLocalDiskSpaceEnable.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.numDiagnoseLocalDiskHealthPeriod);
            this.groupBox5.Controls.Add(this.label33);
            this.groupBox5.Controls.Add(this.chbDiagnoseLocalDiskHealthEnable);
            this.groupBox5.Location = new System.Drawing.Point(231, 7);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(259, 156);
            this.groupBox5.TabIndex = 20;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Disk Health";
            // 
            // numDiagnoseLocalDiskHealthPeriod
            // 
            this.numDiagnoseLocalDiskHealthPeriod.Location = new System.Drawing.Point(59, 38);
            this.numDiagnoseLocalDiskHealthPeriod.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.numDiagnoseLocalDiskHealthPeriod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numDiagnoseLocalDiskHealthPeriod.Name = "numDiagnoseLocalDiskHealthPeriod";
            this.numDiagnoseLocalDiskHealthPeriod.Size = new System.Drawing.Size(59, 20);
            this.numDiagnoseLocalDiskHealthPeriod.TabIndex = 1;
            this.numDiagnoseLocalDiskHealthPeriod.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(25, 41);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(141, 13);
            this.label33.TabIndex = 2;
            this.label33.Text = "every                        minutes";
            // 
            // chbDiagnoseLocalDiskHealthEnable
            // 
            this.chbDiagnoseLocalDiskHealthEnable.AutoSize = true;
            this.chbDiagnoseLocalDiskHealthEnable.Checked = true;
            this.chbDiagnoseLocalDiskHealthEnable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbDiagnoseLocalDiskHealthEnable.Location = new System.Drawing.Point(6, 19);
            this.chbDiagnoseLocalDiskHealthEnable.Name = "chbDiagnoseLocalDiskHealthEnable";
            this.chbDiagnoseLocalDiskHealthEnable.Size = new System.Drawing.Size(247, 17);
            this.chbDiagnoseLocalDiskHealthEnable.TabIndex = 0;
            this.chbDiagnoseLocalDiskHealthEnable.Text = "Diagnose local disks\' health via S.M.A.R.T info";
            this.chbDiagnoseLocalDiskHealthEnable.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblRamAvg);
            this.groupBox1.Controls.Add(this.lblRamCurrent);
            this.groupBox1.Controls.Add(this.lblCpuAvg);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.lblCpuCurrent);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.numMaxHistory);
            this.groupBox1.Controls.Add(this.numRamThreshold);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numCpuThreshold);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(219, 157);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "CPU && RAM";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Transparent;
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(496, 430);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Event Logs";
            // 
            // timer5
            // 
            this.timer5.Tag = "Disk health";
            this.timer5.Tick += new System.EventHandler(this.timer5_Tick);
            // 
            // timer6
            // 
            this.timer6.Tag = "Disk space";
            this.timer6.Tick += new System.EventHandler(this.timer6_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 510);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.lblStatusBar);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.btnExit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(544, 549);
            this.Name = "Form1";
            this.Text = "System Resources Alerter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEmailPort)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelayBetweenEmails)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCpuThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRamThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxHistory)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCheckLocalDiskUsagePeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOtherPartitionUsageThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSystemPartitionUsageThreshold)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDiagnoseLocalDiskHealthPeriod)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtEmailFrom;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtEmailHost;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chbEmailLogin;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtEmailUser;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.CheckBox chbEmailSsl;
        private System.Windows.Forms.TextBox txtEmailPassword;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnHide;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnReceiverTest;
        private System.Windows.Forms.Button btnReceiverDelete;
        private System.Windows.Forms.Button btnReceiverAdd;
        private System.Windows.Forms.ListView lsvReceiver;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown numDelayBetweenEmails;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown numEmailPort;
        private System.Windows.Forms.TextBox txtEmailSubject;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.CheckBox chbAutoStart;
        private System.Windows.Forms.Label lblStatusBar;
        private System.Windows.Forms.CheckBox chbAutoHide;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chbForwardEventLogs;
        private System.Windows.Forms.CheckBox chbEventLogCategory4;
        private System.Windows.Forms.CheckBox chbEventLogCategory3;
        private System.Windows.Forms.CheckBox chbEventLogCategory2;
        private System.Windows.Forms.CheckBox chbEventLogCategory1;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.CheckBox chbEventLogLevel3;
        private System.Windows.Forms.CheckBox chbEventLogLevel2;
        private System.Windows.Forms.CheckBox chbEventLogLevel1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox txtEmailSubjectLog;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.CheckBox chbEventLogLevel4;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.LinkLabel lnkEventLogTaskBlackList;
        private System.Windows.Forms.LinkLabel lnkEventLogIdsBlackList;
        private System.Windows.Forms.LinkLabel lnkEventLogSourceBlackList;
        private System.Windows.Forms.LinkLabel lnkEventLogTaskWhiteList;
        private System.Windows.Forms.LinkLabel lnkEventLogIdsWhiteList;
        private System.Windows.Forms.LinkLabel lnkEventLogSourceWhiteList;
        private System.Windows.Forms.CheckBox chbEventLogTaskBlackList;
        private System.Windows.Forms.CheckBox chbEventLogIdsBlackList;
        private System.Windows.Forms.CheckBox chbEventLogSourceBlackList;
        private System.Windows.Forms.CheckBox chbEventLogTaskWhiteList;
        private System.Windows.Forms.CheckBox chbEventLogIdsWhiteList;
        private System.Windows.Forms.CheckBox chbEventLogSourceWhiteList;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btnTestEventLog;
        private System.Windows.Forms.LinkLabel lnkEventLogMessageBlackList;
        private System.Windows.Forms.LinkLabel lnkEventLogMessageWhiteList;
        private System.Windows.Forms.CheckBox chbEventLogMessageBlackList;
        private System.Windows.Forms.CheckBox chbEventLogMessageWhiteList;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.CheckBox chbDailySystemInfoEmailEnable;
        private System.Windows.Forms.TextBox txtDailySystemInfoEmailTime;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Timer timer4;
        private System.Windows.Forms.Button btnCheckOnlineUpdate;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sendSystemInfoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sendResourceStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateAppToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numCpuThreshold;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numRamThreshold;
        private System.Windows.Forms.NumericUpDown numMaxHistory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCpuCurrent;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblCpuAvg;
        private System.Windows.Forms.Label lblRamCurrent;
        private System.Windows.Forms.Label lblRamAvg;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.NumericUpDown numOtherPartitionUsageThreshold;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.NumericUpDown numSystemPartitionUsageThreshold;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.CheckBox chbCheckLocalDiskSpaceEnable;
        private System.Windows.Forms.NumericUpDown numDiagnoseLocalDiskHealthPeriod;
        private System.Windows.Forms.CheckBox chbDiagnoseLocalDiskHealthEnable;
        private System.Windows.Forms.NumericUpDown numCheckLocalDiskUsagePeriod;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Timer timer5;
        private System.Windows.Forms.Timer timer6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}

