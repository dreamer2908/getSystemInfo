using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net.Mail;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Diagnostics.Eventing.Reader;

namespace systemResourceAlerter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            readSettings();
            loadSettings();

            timer1.Interval = 1000;
            timer1.Start();

            if (autoStart)
            {
                startEmailAlert();
            }

            allowShowUI = !autoHide;
            notifyIcon1.Visible = autoHide;
        }

        string email_host = "";
        int email_port = 25;
        bool email_ssl = false;
        string email_from = "";
        string email_user = "";
        bool email_login = true;
        string email_password = "";
        List<string> email_to = new List<string>();
        string email_subject = "";
        string email_subject_log = "";

        Queue<double> cpuUsageHistory = new Queue<double>();
        Queue<double> ramUsageHistory = new Queue<double>();

        double cpuUsageLast = 0;
        double ramUsageLast = 0;

        PerformanceCounter cpuCounter;

        int cpuHistoryMax = 60;
        int ramHistoryMax = 60;

        double cpuThreshold = 90.0;
        double ramThreshold = 90.0;

        bool alertInProgress = false;
        DateTime alertBegin = DateTime.MinValue;

        DateTime lastEmailTimestamp = DateTime.MinValue;
        double delayBetweenEmails = 900; // seconds

        bool autoStart = false;
        bool autoHide = false;

        bool allowShowUI = false;

        bool forwardEventLogs = false;
        bool eventLogType1 = false;
        bool eventLogType2 = false;
        bool eventLogType3 = false;
        bool eventLogType4 = false;
        bool eventLogLevel1 = false;
        bool eventLogLevel2 = false;
        bool eventLogLevel3 = false;
        bool eventLogLevel4 = false;

        DateTime lastLogEntryTime = DateTime.Now; // .Subtract(new TimeSpan(1, 00, 0));

        #region misc
        private double queueCalcAverage(Queue<double> history)
        {
            double re = 0;

            double[] array = history.ToArray();
            re = array.Sum() / array.Length;

            return re;
        }

        private string queueCalcAverageString(Queue<double> history)
        {
            double t = queueCalcAverage(history);
            return string.Format("{0:0.00}%", t);
        }

        public static string getNowStringForFilename()
        {
            return DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        }

        public static string getNowString()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string formatDateTime(DateTime d)
        {
            return d.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string formatTimeSpan(TimeSpan s)
        {
            if (s.TotalDays >= 1)
            {
                return s.ToString(@"dd\.hh\:mm\:ss");
            }
            else if (s.TotalHours >= 1)
            {
                return s.ToString(@"hh\:mm\:ss");
            }
            else
            {
                return s.ToString(@"hh\:mm\:ss");
            }
        }
        #endregion

        #region CPU & RAM
        private void cpuCounterInit()
        {
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            double t = cpuCounter.NextValue(); // the first reading is always 0
            System.Threading.Thread.Sleep(1000); // wait a second to get a valid reading
            t = cpuCounter.NextValue(); // this time it will be correct
        }

        private double getCpuUsage()
        {
            double t = 0;

            try
            {
                if (cpuCounter == null)
                {
                    cpuCounterInit();
                }
                t = cpuCounter.NextValue();

                Console.WriteLine("CPU Usage: {0:0.00}%", t);
            }
            catch (System.InvalidOperationException)
            {
                Console.WriteLine("CPU Usage: N/A");
                cpuCounterInit();
            }

            return t;
        }

        private void queueCpuUsage()
        {
            double t = getCpuUsage();
            cpuUsageHistory.Enqueue(t);
            cpuUsageLast = t;

            while (cpuUsageHistory.Count > cpuHistoryMax) cpuUsageHistory.Dequeue();
        }

        private double getRamUsage()
        {
            var comInfo = new Microsoft.VisualBasic.Devices.ComputerInfo();
            long total = (long)comInfo.TotalPhysicalMemory;
            long available = (long)comInfo.AvailablePhysicalMemory;
            long used = total - available;
            return 100.0 * used / total;
        }

        private void queueRamUsage()
        {
            double t = getRamUsage();
            ramUsageHistory.Enqueue(t);
            ramUsageLast = t;

            while (ramUsageHistory.Count > ramHistoryMax) ramUsageHistory.Dequeue();
        }

        private void checkThreshold()
        {
            double avgCPU = queueCalcAverage(cpuUsageHistory);
            double avgRAM = queueCalcAverage(ramUsageHistory);
            var now = DateTime.Now;

            if ((avgCPU > cpuThreshold && cpuUsageHistory.Count >= cpuHistoryMax) || (avgRAM > ramThreshold && ramUsageHistory.Count >= ramHistoryMax))
            {
                // set alertBegin if this is the beginning of an alert
                if (!alertInProgress)
                {
                    alertBegin = now;
                    labelEditText(lblStatusBar, "Alert in progress!");
                }
                alertInProgress = true;
            } 
            else
            {
                if (alertInProgress)
                {
                    labelEditText(lblStatusBar, "Alert cleared!");
                }

                alertInProgress = false;
                alertBegin = DateTime.MinValue;
            }
        }
        #endregion

        #region email
        // single receipent
        private void sendEmail(string email_to, string email_subject, string email_body)
        {
            sendEmail(email_host, email_port, email_ssl, email_from, email_user, email_password, email_to, email_subject, email_body);
        }

        // multiple receipents
        private void sendEmail(List<string> email_to, string email_subject, string email_body)
        {
            sendEmail(email_host, email_port, email_ssl, email_from, email_user, email_password, email_to, email_subject, email_body);
        }

        // single receipent
        private void sendEmail(string email_host, int email_port, bool email_ssl, string email_from, string email_user, string email_password, string email_to, string email_subject, string email_body)
        {
            List<string> to = new List<string>();
            to.Add(email_to);
            sendEmail(email_host, email_port, email_ssl, email_from, email_user, email_password, to, email_subject, email_body);
        }

        // multiple receipents
        private void sendEmail(string email_host, int email_port, bool email_ssl, string email_from, string email_user, string email_password, List<string> email_to, string email_subject, string email_body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(email_host);

                mail.From = new MailAddress(email_from);
                foreach (string em in email_to)
                {
                    mail.To.Add(em);
                }
                mail.Subject = email_subject;
                mail.Body = email_body;

                SmtpServer.Port = email_port;
                SmtpServer.Credentials = new System.Net.NetworkCredential(email_user, email_password);
                SmtpServer.EnableSsl = email_ssl;

                SmtpServer.Send(mail);
                // MessageBox.Show("mail Send");
                Console.WriteLine("mail Send");

                labelEditText(lblStatusBar, "Email sent OK!");
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.ToString());
                Console.WriteLine(ex.ToString());

                labelEditText(lblStatusBar, "Failed to send email!");
            }
        }

        private void sendEmailAlert(bool ignoreDelay=false, bool ignoreAlertStatus=false, List<string> custom_to=null)
        {
            // only send email if more than delayBetweenEmails seconds has passed since last email
            // or when delay is asked to be ignored
            double secondSinceLastEmail = (DateTime.Now - lastEmailTimestamp).TotalSeconds;

            if ((alertInProgress || ignoreAlertStatus) && (ignoreDelay || secondSinceLastEmail >= delayBetweenEmails))
            {
                string email_body = "At system time: " + getNowString();
                double avgCPU = queueCalcAverage(cpuUsageHistory);
                double avgRAM = queueCalcAverage(ramUsageHistory);
                var alertDuration = (DateTime.Now - alertBegin);

                email_body += "\n\n" + string.Format("Average CPU usage over {1} seconds period is {0:0.00}%.", avgCPU, cpuUsageHistory.Count);
                email_body += "\n" + string.Format("Average RAM usage over {1} seconds period is {0:0.00}%.", avgRAM, ramUsageHistory.Count);

                if (alertInProgress)
                {
                    email_body += "\n\n" + string.Format("Alert detected at: {0}. \nElapsed Time: {1}\n", formatDateTime(alertBegin), formatTimeSpan(alertDuration));
                } else
                {
                    email_body += "\n\n" + string.Format("No alert detected.\n");
                }

                lastEmailTimestamp = DateTime.Now;
                if (custom_to != null)
                {
                    sendEmail(custom_to, email_subject, email_body);
                }
                else
                {
                    sendEmail(email_to, email_subject, email_body);
                }
            }
        }

        private void sendEmailForwardEventLog(List<string> custom_to = null)
        {
            List<myEventEntry> myEventEntries = readEventLog();

            // MessageBox.Show(string.Format("myEventEntries count = {0}", myEventEntries.Count));

            foreach (var me in myEventEntries)
            {
                string email_body = "Windows Event Log:\n";

                email_body += string.Format("\nCategory: {0}", me.logType);
                email_body += string.Format("\nLevel: {0}", me.level);
                email_body += string.Format("\nTimestamp: {0}", me.timestamp);
                email_body += string.Format("\nSource: {0}", me.source);
                email_body += string.Format("\nEvent ID: {0}", me.eventID);
                email_body += string.Format("\nMessage: \n{0}", me.message);

                // MessageBox.Show(email_body);

                if (custom_to != null)
                {
                    sendEmail(custom_to, email_subject_log, email_body);
                }
                else
                {
                    sendEmail(email_to, email_subject_log, email_body);
                }
            }
        }
        #endregion

        #region Event Log

        struct myEventEntry
        {
            public string logType { get; set; }
            public string level { get; set; }
            public DateTime timestamp { get; set; }
            public string source { get; set; }
            public int eventID { get; set; }
            public string message { get; set; }
            public string computer { get; set; }
            // EventLogEntry entry { get; set; }
        }

        private List<myEventEntry> readEventLog()
        {
            List<myEventEntry> result = new List<myEventEntry>();
            DateTime endTime = lastLogEntryTime;

            List<string> logTypes = new List<string>();
            if (eventLogType1) logTypes.Add("Application");
            if (eventLogType2) logTypes.Add("Security");
            // if (eventLogType3) logTypes.Add("Setup"); // Setup category needs a different approach
            if (eventLogType4) logTypes.Add("System");

            List<EventLogEntryType> logLevels = new List<EventLogEntryType>();
            List<StandardEventLevel> logLevels2 = new List<StandardEventLevel>();

            if (eventLogLevel1)
            {
                logLevels.Add(EventLogEntryType.Error);
                logLevels2.Add(StandardEventLevel.Error);
            }
            if (eventLogLevel2)
            {
                logLevels.Add(EventLogEntryType.Warning);
                logLevels2.Add(StandardEventLevel.Warning);
            }
            if (eventLogLevel3)
            {
                logLevels.Add(EventLogEntryType.Information);
                logLevels2.Add(StandardEventLevel.Informational);
                logLevels2.Add(StandardEventLevel.LogAlways);
            }
            if (eventLogLevel4)
            {
                logLevels.Add(EventLogEntryType.SuccessAudit);
                logLevels.Add(EventLogEntryType.FailureAudit);
            }

            var logNames = getEventLogNames();

            // query for all but Setup category
            foreach (string logType in logTypes)
            {
                //if (!logNames.Contains(logType))
                //{
                //    Console.WriteLine("Log category not found: {0}.", logType);
                //    continue;
                //}

                EventLog log = new EventLog(logType);
                int length = log.Entries.Count;

                // read from the end, get entries younger than startTime
                for (int e = length - 1; e >= 0; e--)
                {
                    EventLogEntry entry = log.Entries[e];
                    DateTime timestamp = entry.TimeGenerated;

                    if (timestamp > lastLogEntryTime)
                    {
                        var level = entry.EntryType;

                        if (logLevels.Contains(level))
                        {
                            myEventEntry me = new myEventEntry
                            {
                                logType = logType,
                                level = level.ToString(),
                                timestamp = timestamp,
                                source = entry.Source,
                                eventID = (UInt16)entry.InstanceId,
                                message = entry.Message,
                                computer = entry.MachineName
                            };

                            result.Add(me);
                        }

                        if (timestamp > endTime) endTime = timestamp;
                    }
                    else
                    {
                        break; // stop reading this event file when reaching startTime
                    }
                }
            }

            // now query for Setup category
            if (eventLogType3)
            {
                string logType = "Setup";
                EventLogQuery query = new EventLogQuery(logType, PathType.LogName);
                query.ReverseDirection = true; // this tells it to start with newest first
                EventLogReader reader = new EventLogReader(query);

                EventRecord eventRecord;

                while ((eventRecord = reader.ReadEvent()) != null)
                {
                    // each eventRecord is an item from the event log
                    DateTime timestamp = (DateTime)eventRecord.TimeCreated;

                    if (timestamp > lastLogEntryTime)
                    {
                        var level = (StandardEventLevel)eventRecord.Level;

                        if (logLevels2.Contains(level))
                        {
                            myEventEntry me = new myEventEntry
                            {
                                logType = logType,
                                level = level.ToString(),
                                timestamp = timestamp,
                                source = eventRecord.ProviderName,
                                eventID = eventRecord.Id,
                                message = eventRecord.FormatDescription(),
                                computer = eventRecord.MachineName
                            };

                            result.Add(me);
                        }

                        if (timestamp > endTime) endTime = timestamp;
                    }
                    else
                    {
                        break; // stop reading this event file when reaching startTime
                    }
                }
            }

            lastLogEntryTime = endTime;

            return result;
        }

        private List<string> getEventLogNames()
        {
            var session = new System.Diagnostics.Eventing.Reader.EventLogSession();
            List<string> logNames = new List<string>(session.GetLogNames());
            return logNames;
        }
        #endregion

        #region Settings
        private void applySettings()
        {
            email_host = txtEmailHost.Text;
            email_port = (int)numEmailPort.Value;
            email_ssl = chbEmailSsl.Checked;
            email_from = txtEmailFrom.Text;
            email_user = txtEmailUser.Text;
            email_login = chbEmailLogin.Checked;
            email_password = txtEmailPassword.Text;
            email_subject = txtEmailSubject.Text;

            email_to.Clear();
            for (int i = 0; i < lsvReceiver.Items.Count; i++)
            {
                email_to.Add(lsvReceiver.Items[i].Text);
            }

            cpuHistoryMax = (int)numMaxHistory.Value;
            ramHistoryMax = (int)numMaxHistory.Value;

            cpuThreshold = (double)numCpuThreshold.Value;
            ramThreshold = (double)numRamThreshold.Value;

            delayBetweenEmails = (int)numDelayBetweenEmails.Value;

            autoStart = chbAutoStart.Checked;
            autoHide = chbAutoHide.Checked;

            email_subject_log = txtEmailSubjectLog.Text;

            forwardEventLogs = chbForwardEventLogs.Checked;
            eventLogType1 = chbEventLogType1.Checked;
            eventLogType2 = chbEventLogType2.Checked;
            eventLogType3 = chbEventLogType3.Checked;
            eventLogType4 = chbEventLogType4.Checked;
            eventLogLevel1 = chbEventLogLevel1.Checked;
            eventLogLevel2 = chbEventLogLevel2.Checked;
            eventLogLevel3 = chbEventLogLevel3.Checked;
            eventLogLevel4 = chbEventLogLevel4.Checked;
        }

        private void loadSettings()
        {
            txtEmailHost.Text = email_host;
            numEmailPort.Value = email_port;
            chbEmailSsl.Checked = email_ssl;
            txtEmailFrom.Text = email_from;
            txtEmailUser.Text = email_user;
            chbEmailLogin.Checked = email_login;
            txtEmailPassword.Text = email_password;
            txtEmailSubject.Text = email_subject;
            numMaxHistory.Value = cpuHistoryMax;
            numMaxHistory.Value = ramHistoryMax;
            numCpuThreshold.Value = (decimal)cpuThreshold;
            numRamThreshold.Value = (decimal)ramThreshold;
            numDelayBetweenEmails.Value = (decimal)delayBetweenEmails;
            chbAutoStart.Checked = autoStart;
            chbAutoHide.Checked = autoHide;

            lsvReceiver.Items.Clear();
            foreach (var item in email_to)
            {
                lsvReceiver.Items.Add(item);
            }

            txtEmailSubjectLog.Text = email_subject_log;

            chbForwardEventLogs.Checked = forwardEventLogs;
            chbEventLogType1.Checked = eventLogType1;
            chbEventLogType2.Checked = eventLogType2;
            chbEventLogType3.Checked = eventLogType3;
            chbEventLogType4.Checked = eventLogType4;
            chbEventLogLevel1.Checked = eventLogLevel1;
            chbEventLogLevel2.Checked = eventLogLevel2;
            chbEventLogLevel3.Checked = eventLogLevel3;
            chbEventLogLevel4.Checked = eventLogLevel4;

            enableDisableEventControls();
        }

        private void writeSettings()
        {
            Settings.Set("email_host", email_host);
            Settings.Set("email_port", email_port);
            Settings.Set("email_ssl", email_ssl);
            Settings.Set("email_from", email_from);
            Settings.Set("email_user", email_user);
            Settings.Set("email_login", email_login);
            Settings.Set("email_password", email_password);
            Settings.Set("email_subject", email_subject);
            Settings.Set("cpuHistoryMax", cpuHistoryMax);
            Settings.Set("ramHistoryMax", ramHistoryMax);
            Settings.Set("cpuThreshold", (int)cpuThreshold);
            Settings.Set("ramThreshold", (int)ramThreshold);
            Settings.Set("delayBetweenEmails", (int)delayBetweenEmails);
            Settings.Set("autoStart", autoStart);
            Settings.Set("autoHide", autoHide);

            Settings.Set("email_to", string.Join(",", email_to));

            Settings.Set("forwardEventLogs", forwardEventLogs);
            Settings.Set("eventLogType1", eventLogType1);
            Settings.Set("eventLogType2", eventLogType2);
            Settings.Set("eventLogType3", eventLogType3);
            Settings.Set("eventLogType4", eventLogType4);
            Settings.Set("eventLogLevel1", eventLogLevel1);
            Settings.Set("eventLogLevel2", eventLogLevel2);
            Settings.Set("eventLogLevel3", eventLogLevel3);
            Settings.Set("eventLogLevel4", eventLogLevel4);


            Settings.Set("email_subject_log", email_subject_log);
        }

        private void readSettings()
        {
            email_host = Settings.Get("email_host", "");
            email_port = Settings.Get("email_port", 25);
            email_ssl = Settings.Get("email_ssl", false);
            email_from = Settings.Get("email_from", "");
            email_user = Settings.Get("email_user", "");
            email_login = Settings.Get("email_login", true);
            email_password = Settings.Get("email_password", "");
            email_subject = Settings.Get("email_subject", "");
            cpuHistoryMax = Settings.Get("cpuHistoryMax", 60);
            ramHistoryMax = Settings.Get("ramHistoryMax", 60);
            cpuThreshold = Settings.Get("cpuThreshold", 90);
            ramThreshold = Settings.Get("ramThreshold", 90);
            delayBetweenEmails = Settings.Get("delayBetweenEmails", 900);
            autoStart = Settings.Get("autoStart", false);
            autoHide = Settings.Get("autoHide", false);

            string email_tos = Settings.Get("email_to", "");
            foreach (var s in email_tos.Split(new string[] { "," }, StringSplitOptions.None))
            {
                var trim = s.Trim();
                if (trim.Length > 0)
                {
                    email_to.Add(trim);
                }
            }

            forwardEventLogs = Settings.Get("forwardEventLogs", false);
            eventLogType1 = Settings.Get("eventLogType1", false);
            eventLogType2 = Settings.Get("eventLogType2", false);
            eventLogType3 = Settings.Get("eventLogType3", false);
            eventLogType4 = Settings.Get("eventLogType4", false);
            eventLogLevel1 = Settings.Get("eventLogLevel1", false);
            eventLogLevel2 = Settings.Get("eventLogLevel2", false);
            eventLogLevel3 = Settings.Get("eventLogLevel3", false);
            eventLogLevel4 = Settings.Get("eventLogLevel4", false);

            email_subject_log = Settings.Get("email_subject_log", "");
        }
        #endregion

        #region UI stuff
        private void timer1_Tick(object sender, EventArgs e)
        {
            queueCpuUsage();
            queueRamUsage();
            updateUiStats();
            checkThreshold();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            sendEmailAlert();
            sendEmailForwardEventLog();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            allowShowUI = true;
            Show();
            notifyIcon1.Visible = false;
        }

        private void updateUiStats()
        {
            if (cpuUsageHistory.Count > 0)
            {
                double nowCPU = cpuUsageLast;
                double avgCPU = queueCalcAverage(cpuUsageHistory);
                labelEditText(lblCpuCurrent, string.Format("{0:0.00}%", nowCPU));
                labelEditText(lblCpuAvg, string.Format("{0:0.00}%", avgCPU));
            }

            if (ramUsageHistory.Count > 0)
            {
                double nowRAM = ramUsageLast;
                double avgRAM = queueCalcAverage(ramUsageHistory);
                labelEditText(lblRamCurrent, string.Format("{0:0.00}%", nowRAM));
                labelEditText(lblRamAvg, string.Format("{0:0.00}%", avgRAM));
            }
        }

        private void labelEditText(Label b, string text)
        {
            if (b.InvokeRequired)
            {
                // It's on a different thread, so use Invoke.
                b.BeginInvoke(new MethodInvoker(() =>
                {
                    b.Text = text;
                }));
            }
            else
            {
                // It's on the same thread, no need for Invoke
                b.Text = text;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            applySettings();
            writeSettings();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            writeSettings();
        }

        private void btnReceiverAdd_Click(object sender, EventArgs e)
        {
            string newEmail = Interaction.InputBox("Enter a new receipent email address", "Add email", "admin@mail.com");
            lsvReceiver.Items.Add(newEmail);
        }

        private void btnReceiverDelete_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem eachItem in lsvReceiver.SelectedItems)
            {
                lsvReceiver.Items.Remove(eachItem);
            }
        }

        private void btnReceiverTest_Click(object sender, EventArgs e)
        {
            List<string> custom_to = new List<string>();
            for (int i = 0; i < lsvReceiver.Items.Count; i++)
            {
                custom_to.Add(lsvReceiver.Items[i].Text);
            }

            sendEmailAlert(true, true, custom_to);
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            if (btnStartStop.Text.EndsWith("Start"))
            {
                startEmailAlert();
            }
            else
            {
                stopEmailAlert();
            }
        }

        private void stopEmailAlert()
        {
            timer2.Stop();
            btnStartStop.Text = "&Start";
        }

        private void startEmailAlert()
        {
            timer2.Start();
            btnStartStop.Text = "&Stop";
        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            hideToTray();
        }

        private void hideToTray()
        {
            allowShowUI = false;
            Hide();
            notifyIcon1.Visible = true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void SetVisibleCore(bool value)
        {
            base.SetVisibleCore(allowShowUI ? value : allowShowUI);
        }

        private void chbForwardEventLogs_CheckedChanged(object sender, EventArgs e)
        {
            enableDisableEventControls();
        }

        private void enableDisableEventControls()
        {
            chbEventLogType1.Enabled = chbEventLogType2.Enabled = chbEventLogType3.Enabled = chbEventLogType4.Enabled = chbForwardEventLogs.Checked;
            chbEventLogLevel1.Enabled = chbEventLogLevel2.Enabled = chbEventLogLevel3.Enabled = chbForwardEventLogs.Checked;
        }
        #endregion
    }

}
