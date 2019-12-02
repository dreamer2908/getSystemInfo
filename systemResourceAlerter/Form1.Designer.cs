namespace systemResourceAlerter
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblRamAvg = new System.Windows.Forms.Label();
            this.lblRamCurrent = new System.Windows.Forms.Label();
            this.lblCpuAvg = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblCpuCurrent = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.numMaxHistory = new System.Windows.Forms.NumericUpDown();
            this.numRamThreshold = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.numCpuThreshold = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
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
            this.btnExit = new System.Windows.Forms.Button();
            this.btnHide = new System.Windows.Forms.Button();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.chbAutoStart = new System.Windows.Forms.CheckBox();
            this.lblStatusBar = new System.Windows.Forms.Label();
            this.chbAutoHide = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRamThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCpuThreshold)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEmailPort)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelayBetweenEmails)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 15000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "System Resources Alerter";
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
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
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(218, 157);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Resources";
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
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(130, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(29, 13);
            this.label9.TabIndex = 4;
            this.label9.Text = "Avg:";
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
            // lblRamCurrent
            // 
            this.lblRamCurrent.AutoSize = true;
            this.lblRamCurrent.Location = new System.Drawing.Point(82, 42);
            this.lblRamCurrent.Name = "lblRamCurrent";
            this.lblRamCurrent.Size = new System.Drawing.Size(42, 13);
            this.lblRamCurrent.TabIndex = 3;
            this.lblRamCurrent.Text = "98.76%";
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
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 42);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "RAM Usage:";
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "CPU Usage:";
            // 
            // numMaxHistory
            // 
            this.numMaxHistory.Location = new System.Drawing.Point(111, 122);
            this.numMaxHistory.Name = "numMaxHistory";
            this.numMaxHistory.Size = new System.Drawing.Size(59, 20);
            this.numMaxHistory.TabIndex = 3;
            this.numMaxHistory.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
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
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(176, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(24, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "sec";
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Averaging Period:";
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "RAM Threshold:";
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
            this.groupBox2.Location = new System.Drawing.Point(238, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(279, 157);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Mail Server";
            // 
            // chbEmailSsl
            // 
            this.chbEmailSsl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbEmailSsl.AutoSize = true;
            this.chbEmailSsl.Location = new System.Drawing.Point(227, 64);
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
            this.txtEmailPassword.Size = new System.Drawing.Size(183, 20);
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
            this.txtEmailUser.Size = new System.Drawing.Size(183, 20);
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
            this.txtEmailHost.Size = new System.Drawing.Size(183, 20);
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
            this.txtEmailFrom.Size = new System.Drawing.Size(183, 20);
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
            this.groupBox3.Location = new System.Drawing.Point(13, 177);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(504, 178);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Email";
            // 
            // txtEmailSubject
            // 
            this.txtEmailSubject.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmailSubject.Location = new System.Drawing.Point(85, 151);
            this.txtEmailSubject.Name = "txtEmailSubject";
            this.txtEmailSubject.Size = new System.Drawing.Size(332, 20);
            this.txtEmailSubject.TabIndex = 18;
            // 
            // label19
            // 
            this.label19.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(7, 154);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(74, 13);
            this.label19.TabIndex = 6;
            this.label19.Text = "Email Subject:";
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
            this.label17.Location = new System.Drawing.Point(451, 20);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 13);
            this.label17.TabIndex = 4;
            this.label17.Text = "seconds";
            // 
            // numDelayBetweenEmails
            // 
            this.numDelayBetweenEmails.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.numDelayBetweenEmails.Location = new System.Drawing.Point(379, 18);
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
            this.label16.Location = new System.Drawing.Point(261, 20);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(112, 13);
            this.label16.TabIndex = 2;
            this.label16.Text = "Delay between alerts: ";
            // 
            // btnReceiverTest
            // 
            this.btnReceiverTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReceiverTest.Location = new System.Drawing.Point(423, 101);
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
            this.btnReceiverDelete.Location = new System.Drawing.Point(423, 72);
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
            this.btnReceiverAdd.Location = new System.Drawing.Point(423, 43);
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
            this.lsvReceiver.Location = new System.Drawing.Point(7, 43);
            this.lsvReceiver.Name = "lsvReceiver";
            this.lsvReceiver.Size = new System.Drawing.Size(410, 102);
            this.lsvReceiver.TabIndex = 12;
            this.lsvReceiver.UseCompatibleStateImageBehavior = false;
            this.lsvReceiver.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 330;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Location = new System.Drawing.Point(441, 361);
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
            this.btnHide.Location = new System.Drawing.Point(360, 361);
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
            this.btnStartStop.Location = new System.Drawing.Point(279, 361);
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
            this.btnApply.Location = new System.Drawing.Point(198, 361);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "&Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // chbAutoStart
            // 
            this.chbAutoStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbAutoStart.AutoSize = true;
            this.chbAutoStart.Location = new System.Drawing.Point(424, 131);
            this.chbAutoStart.Name = "chbAutoStart";
            this.chbAutoStart.Size = new System.Drawing.Size(73, 17);
            this.chbAutoStart.TabIndex = 16;
            this.chbAutoStart.Text = "Auto Start";
            this.chbAutoStart.UseVisualStyleBackColor = true;
            // 
            // lblStatusBar
            // 
            this.lblStatusBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStatusBar.AutoSize = true;
            this.lblStatusBar.Location = new System.Drawing.Point(20, 371);
            this.lblStatusBar.Name = "lblStatusBar";
            this.lblStatusBar.Size = new System.Drawing.Size(37, 13);
            this.lblStatusBar.TabIndex = 7;
            this.lblStatusBar.Text = "Status";
            // 
            // chbAutoHide
            // 
            this.chbAutoHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbAutoHide.AutoSize = true;
            this.chbAutoHide.Location = new System.Drawing.Point(424, 153);
            this.chbAutoHide.Name = "chbAutoHide";
            this.chbAutoHide.Size = new System.Drawing.Size(73, 17);
            this.chbAutoHide.TabIndex = 17;
            this.chbAutoHide.Text = "Auto Hide";
            this.chbAutoHide.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 396);
            this.Controls.Add(this.lblStatusBar);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.btnHide);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(544, 435);
            this.Name = "Form1";
            this.Text = "System Resources Alerter";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRamThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCpuThreshold)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numEmailPort)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDelayBetweenEmails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numMaxHistory;
        private System.Windows.Forms.NumericUpDown numRamThreshold;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numCpuThreshold;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblRamAvg;
        private System.Windows.Forms.Label lblRamCurrent;
        private System.Windows.Forms.Label lblCpuAvg;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblCpuCurrent;
        private System.Windows.Forms.Label label7;
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
    }
}

