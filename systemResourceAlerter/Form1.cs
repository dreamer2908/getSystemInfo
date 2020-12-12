using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Net.Mail;
using System.Diagnostics;
using Microsoft.VisualBasic;
using System.Diagnostics.Eventing.Reader;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;
using System.Text;
using System.IO;
using System.Drawing;
using System.Globalization;
using getSystemInfo_cli;

namespace systemResourceAlerter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Directory.SetCurrentDirectory(Path.GetDirectoryName(new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath));

            readSettings();
            loadSettings();

            timer1.Start();

            if (autoStart)
            {
                startEmailAlert();
            }

            timer3.Start();

            restartDiskTimers();

            allowShowUI = !autoHide;
            notifyIcon1.Visible = autoHide;

            if (autoStart && diagnoseLocalDiskHealthEnable)
            {
                diagnoseLocalDiskHealth();
                sendHddHealthAlert();
            }
        }

        #region Variables

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
        bool eventLogCategory1 = false;
        bool eventLogCategory2 = false;
        bool eventLogCategory3 = false;
        bool eventLogCategory4 = false;
        bool eventLogLevel1 = false;
        bool eventLogLevel2 = false;
        bool eventLogLevel3 = false;
        bool eventLogLevel4 = false;

        bool eventLogSourceWhiteListEnable = false;
        bool eventLogSourceBlackListEnable = false;
        List<string> eventLogSourceWhiteList = new List<string>();
        List<string> eventLogSourceBlackList = new List<string>();

        bool eventLogIdsWhiteListEnable = false;
        bool eventLogIdsBlackListEnable = false;
        List<string> eventLogIdsWhiteList = new List<string>();
        List<string> eventLogIdsBlackList = new List<string>();

        bool eventLogTaskWhiteListEnable = false;
        bool eventLogTaskBlackListEnable = false;
        List<string> eventLogTaskWhiteList = new List<string>();
        List<string> eventLogTaskBlackList = new List<string>();

        bool eventLogMessageWhiteListEnable = false;
        bool eventLogMessageBlackListEnable = false;
        List<string> eventLogMessageWhiteList = new List<string>();
        List<string> eventLogMessageBlackList = new List<string>();

        bool showTestButton = false;

        DateTime lastLogEntryTime = DateTime.Now; // .Subtract(new TimeSpan(1, 00, 0));
        TimeSpan lastLogEntryTime_maxDistance = new TimeSpan(24, 00, 0);

        Mutex mutexEventForward = new Mutex();

        string statusBarText = "No status.";

        bool dailySystemInfoEmailEnable = false;
        string dailySystemInfoEmailTime = string.Empty;

        string onlineUpdateUrl = string.Empty;
        string onlineUpdateDir = string.Empty;
        int onlineUpdatePeriod = 900; // seconds. set this to 0 or negative to disable update

        bool checkLocalDiskSpaceEnable = true;
        int checkLocalDiskSpacePeriod = 60; // minutes
        int systemPartitionThreshold = 90;
        int otherPartitionThreshold = 90;
        bool alert2InProgress = false;
        string alert2Message = string.Empty;

        bool diagnoseLocalDiskHealthEnable = true;
        int diagnoseLocalDiskHealthPeriod = 60; // minutes
        bool alert3InProgress = false;
        string alert3Message = string.Empty;

        #endregion

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

        public static string datetimeCommonFormatString = "yyyy-MM-dd HH:mm:ss";
        public static string getNowString()
        {
            return formatDateTime(DateTime.Now);
        }

        public static string formatDateTime(DateTime d)
        {
            return d.ToString(datetimeCommonFormatString);
        }

        public static DateTime parseDateTime(string s)
        {
            try
            {
                return DateTime.ParseExact(s, datetimeCommonFormatString, CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                return DateTime.MinValue;
            }
        }

        public static DateTime parseDateTime(string s, TimeSpan maxDistanceFromNow)
        {
            var d = parseDateTime(s);
            var now = DateTime.Now;
            if (now - d > maxDistanceFromNow)
            {
                d = now.Subtract(maxDistanceFromNow);
            }
            else if (d - now > maxDistanceFromNow)
            {
                d = now.Add(maxDistanceFromNow);
            }
            return d;
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

        private string convertTextToHtml(string input)
        {
            string[] lines = splitLines(input);

            StringBuilder sb = new StringBuilder();

            foreach (string text in lines)
            {
                string encoded = System.Net.WebUtility.HtmlEncode(text);
                sb.AppendLine("<pre>" + encoded + "</pre>");
            }

            return sb.ToString();
        }

        private string[] splitLines(string text)
        {
            string[] lines = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            return lines;
        }

        private static string[] customSplitLines(string text)
        {
            List<string> result = new List<string>();

            string empty = " "; // workaround for Outlook ignoring totally empty line

            string thisLine = empty;
            int i = 0;
            while (i < text.Length)
            {
                if (text[i] == '\n')
                {
                    result.Add(thisLine);
                    thisLine = empty;
                    i++;
                }
                else if (text[i] == '\r')
                {
                    result.Add(thisLine);
                    thisLine = empty;

                    if (text[i+1] == '\n')
                    {
                        i += 2;
                    }
                    else
                    {
                        i += 1;
                    }
                }
                else
                {
                    thisLine = thisLine + text[i].ToString();
                    i++;
                }
            }

            return result.ToArray();
        }

        private bool splitKeyValue(string line, ref string key, ref string value)
        {
            string[] parts = line.Split(new string[] { "=" }, 2, StringSplitOptions.None);
            if (parts.Length == 2)
            {
                key = parts[0];
                value = parts[1];
                return true;
            }
            return false;
        }

        static string addPaddingToLines(string text, string pad)
        {
            var bs = new StringBuilder();

            var input = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            foreach (var line in input)
            {
                bs.AppendLine(pad + line);
            }

            return bs.ToString();
        }
        #endregion

        #region Updater
        private string readTextFileFromHttp(string url)
        {
            string result = string.Empty;
            using (System.Net.WebClient webClient = new System.Net.WebClient())
            {
                try
                {
                    result = webClient.DownloadString(url);
                }
                catch (Exception ex) when (ex is ArgumentNullException || ex is System.Net.WebException || ex is NotSupportedException)
                {
                    // do nothing, will return empty string
                }
            }
            return result;
        }

        private bool downloadFileFromHttp(string url, string filePath)
        {
            using (System.Net.WebClient webClient = new System.Net.WebClient())
            {
                try
                {
                    webClient.DownloadFile(url, filePath);
                    return true;
                }
                catch (Exception ex) when (ex is ArgumentNullException || ex is System.Net.WebException || ex is NotSupportedException)
                {
                    return false;
                }
            }
        }

        private void checkOnlineForUpdate(bool debug=false)
        {
            string updateInfo = readTextFileFromHttp(onlineUpdateUrl);
            if (debug)
            {
                MessageBox.Show(updateInfo);
            }

            string newVer = string.Empty;
            List<string> urls = new List<string>();
            string runAfter = string.Empty;

            foreach (string line in splitLines(updateInfo))
            {
                string key = string.Empty;
                string value = string.Empty;
                if (splitKeyValue(line, ref key, ref value))
                {
                    if (key == "version")
                    {
                        newVer = value;
                    }
                    else if (key == "download")
                    {
                        urls.Add(value);
                    }
                    else if (key == "run")
                    {
                        runAfter = value;
                    }
                }
            }

            if (debug)
            {
                MessageBox.Show("newVer = " + newVer);
                MessageBox.Show("runAfter = " + runAfter);
                MessageBox.Show("urls = " + String.Join("\n", urls));
            }

            string appVer = Application.ProductVersion;

            if (string.Compare(newVer, appVer) > 0)
            {
                // new version available
                if (debug)
                {
                    MessageBox.Show("new version available");
                }

                Directory.CreateDirectory(onlineUpdateDir);
                foreach (string url in urls)
                {
                    downloadFileFromHttp(url, Path.Combine(onlineUpdateDir, Path.GetFileName(url)));
                }
                System.Diagnostics.Process.Start(Path.Combine(onlineUpdateDir, runAfter));
                Application.Exit();
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
        private void sendEmail(string email_to, string email_subject, string email_body, List<string> attachments = null)
        {
            sendEmail(email_host, email_port, email_ssl, email_from, email_user, email_password, email_to, email_subject, email_body, attachments);
        }

        // multiple receipents
        private void sendEmail(List<string> email_to, string email_subject, string email_body, List<string> attachments = null)
        {
            sendEmail(email_host, email_port, email_ssl, email_from, email_user, email_password, email_to, email_subject, email_body, attachments);
        }

        // single receipent
        private void sendEmail(string email_host, int email_port, bool email_ssl, string email_from, string email_user, string email_password, string email_to, string email_subject, string email_body, List<string> attachments = null)
        {
            List<string> to = new List<string>();
            to.Add(email_to);
            sendEmail(email_host, email_port, email_ssl, email_from, email_user, email_password, to, email_subject, email_body, attachments);
        }

        // multiple receipents
        private void sendEmail(string email_host, int email_port, bool email_ssl, string email_from, string email_user, string email_password, List<string> email_to, string email_subject, string email_body, List<string> attachments = null)
        {
            using (SmtpClient SmtpServer = new SmtpClient(email_host))
            {
                using (MailMessage mail = new MailMessage())
                {
                    try
                    {
                        mail.From = new MailAddress(email_from);
                        foreach (string em in email_to)
                        {
                            mail.To.Add(em);
                        }
                        mail.Subject = email_subject;
                        mail.IsBodyHtml = true;
                        mail.Body = convertTextToHtml(email_body);

                        // attach files
                        if (attachments != null)
                        {
                            foreach (var filename in attachments)
                            {
                                mail.Attachments.Add(new Attachment(filename));
                                // MessageBox.Show("Added attachment to email");
                            }
                        }

                        SmtpServer.Port = email_port;
                        SmtpServer.Credentials = new System.Net.NetworkCredential(email_user, email_password);
                        SmtpServer.EnableSsl = email_ssl;

                        SmtpServer.Send(mail);

                        // MessageBox.Show("mail Send");
                        Console.WriteLine("mail Send");

                        statusBarText = "Email sent OK!";
                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show(ex.ToString());
                        Console.WriteLine(ex.ToString());

                        statusBarText = "Failed to send email!";
                    }
                }
            }
        }

        private void sendEmailAlert(bool ignoreDelay=false, bool ignoreAlertStatus=false, List<string> custom_to=null)
        {
            // only send email if more than delayBetweenEmails seconds has passed since last email
            // or when delay is asked to be ignored
            double secondSinceLastEmail = (DateTime.Now - lastEmailTimestamp).TotalSeconds;

            if ((alertInProgress || alert2InProgress || ignoreAlertStatus) && (ignoreDelay || secondSinceLastEmail >= delayBetweenEmails))
            {
                string email_body = emailHeadline + "At system time: " + getNowString();
                double avgCPU = queueCalcAverage(cpuUsageHistory);
                double avgRAM = queueCalcAverage(ramUsageHistory);
                var alertDuration = (DateTime.Now - alertBegin);

                if (alertInProgress)
                {
                    email_body += "\n \n" + string.Format("Alert detected at: {0}!! \nElapsed Time: {1}", formatDateTime(alertBegin), formatTimeSpan(alertDuration));
                }
                else
                {
                    email_body += "\n \n" + string.Format("No alert detected about CPU and RAM.");
                }

                email_body += "\n \n" + string.Format("Average CPU usage over {1} seconds period is {0:0.00}%.", avgCPU, cpuUsageHistory.Count);
                email_body += "\n" + string.Format("Average RAM usage over {1} seconds period is {0:0.00}%.", avgRAM, ramUsageHistory.Count);

                if (alert2InProgress)
                {
                    email_body += "\n \n" + "Disk space alert detected!!\n \n" + alert2Message;
                }
                else
                {
                    email_body += "\n \n" + string.Format("No alert detected about local disk space.\n");
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

        private void sendHddHealthAlert(bool ignoreDelay = false, bool ignoreAlertStatus = false, List<string> custom_to = null)
        {
            if (alert3InProgress || ignoreAlertStatus)
            {
                string email_body = emailHeadline + "At system time: " + getNowString();
                double avgCPU = queueCalcAverage(cpuUsageHistory);
                double avgRAM = queueCalcAverage(ramUsageHistory);
                var alertDuration = (DateTime.Now - alertBegin);

                if (alert3InProgress)
                {
                    email_body += "\n \n" + "Disk health alert detected!!\n \n" + alert3Message;
                }
                else
                {
                    email_body += "\n \n" + string.Format("No alert detected about local disk health.\n");
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
            mutexEventForward.WaitOne();

            List<myEventEntry> myEventEntries = readEventLog();

            // MessageBox.Show(string.Format("myEventEntries count = {0}", myEventEntries.Count));

            if (myEventEntries.Count > 0)
            {
                string email_body = emailHeadline + writeEventForwardEmailBody(myEventEntries);

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

            mutexEventForward.ReleaseMutex();
        }

        private static string emailHeadline = "*** This is a system generated email, do not reply to this email id ***\n \n";
        private static string eventSeparator = "\n \n####################################################################\n \n";

        private static string writeEventForwardEmailBody(List<myEventEntry> myEventEntries)
        {
            List<String> body = new List<string>();

            body.Add(string.Format("Event Count: {0}", myEventEntries.Count));

            foreach (var me in myEventEntries)
            {
                body.Add(writeEventForwardEmailBody(me));
            }

            return string.Join(eventSeparator, body);
        }

        private static string writeEventForwardEmailBody(myEventEntry me)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Windows Event Log:");
            sb.AppendLine(" ");
            sb.AppendLine(string.Format("Category: {0}", me.category));
            sb.AppendLine(string.Format("Level: {0}", me.level));
            sb.AppendLine(string.Format("Timestamp: {0}", me.timestamp));
            sb.AppendLine(string.Format("Computer: {0}", me.computer));
            sb.AppendLine(string.Format("Source: {0}", me.source));
            sb.AppendLine(string.Format("Event ID: {0}", me.eventID));
            sb.AppendLine(string.Format("Task Category: {0}", me.taskCategory));
            sb.AppendLine("Message:\n ");
            sb.AppendLine(me.message);

            return sb.ToString();
        }

        private void sendDailySystemInfoEmail()
        {
            // try to take screenshot, attach it to email if ok
            string imgFilename = "screenshot.png";
            List<string> attachments = new List<string>();
            string screenshotMsg;
            bool screenshotOk = takeScreenShot(imgFilename);
            if (screenshotOk)
            {
                attachments.Add(imgFilename);
                // MessageBox.Show("Added image to attachment name list");
                screenshotMsg = "Automatic screenshot available. See the attachment.\n \n";
            }
            else
            {
                screenshotMsg = "Automatic screenshot failed. Session locked?\n \n";
            }

            string email_body = emailHeadline + screenshotMsg + getSystemInfo();
            // MessageBox.Show(email_body);

            sendEmail(email_to, email_subject, email_body, attachments);
        }
        #endregion

        #region Event Log

        struct myEventEntry
        {
            public string category { get; set; }
            public string level { get; set; }
            public DateTime timestamp { get; set; }
            public string source { get; set; }
            public int eventID { get; set; }
            public string message { get; set; }
            public string computer { get; set; }
            public string taskCategory { get; set; }
            // EventLogEntry entry { get; set; }
        }

        private List<myEventEntry> readEventLog()
        {
            List<myEventEntry> result = new List<myEventEntry>();
            DateTime endTime = lastLogEntryTime;

            List<string> logCategories = new List<string>();
            if (eventLogCategory1) logCategories.Add("Application");
            if (eventLogCategory2) logCategories.Add("Security");
            // if (eventLogCategory3) logTypes.Add("Setup"); // Setup category needs a different approach
            if (eventLogCategory4) logCategories.Add("System");

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
            foreach (string logCat in logCategories)
            {
                //if (!logNames.Contains(logType))
                //{
                //    Console.WriteLine("Log category not found: {0}.", logType);
                //    continue;
                //}

                using (EventLog log = new EventLog(logCat))
                {
                    int length = log.Entries.Count;

                    // read from the end, get entries younger than startTime
                    for (int e = length - 1; e >= 0; e--)
                    {
                        try
                        {
                            using (EventLogEntry entry = log.Entries[e])
                            {
                                DateTime timestamp = entry.TimeGenerated;

                                if (timestamp > lastLogEntryTime)
                                {
                                    var level = entry.EntryType;

                                    if (logLevels.Contains(level))
                                    {
                                        myEventEntry me = new myEventEntry
                                        {
                                            category = logCat,
                                            level = level.ToString(),
                                            timestamp = timestamp,
                                            source = entry.Source,
                                            eventID = (UInt16)entry.InstanceId,
                                            message = entry.Message,
                                            computer = entry.MachineName,
                                            taskCategory = entry.Category
                                        };

                                        bool accept = acceptRejectEventLog(me);

                                        if (accept) result.Add(me);
                                    }

                                    if (timestamp > endTime) endTime = timestamp;
                                }
                                else
                                {
                                    break; // stop reading this event file when reaching startTime
                                }
                            }
                        }
                        catch (System.ArgumentException ex)
                        {
                            Console.WriteLine("Error reading event log entry: {0}", ex);
                            break;
                        }
                    }
                }
            }

            // now query for Setup category
            if (eventLogCategory3)
            {
                string logCategoryName = "Setup";
                EventLogQuery query = new EventLogQuery(logCategoryName, PathType.LogName);
                query.ReverseDirection = true; // this tells it to start with newest first

                using (EventLogReader reader = new EventLogReader(query))
                {
                    EventRecord eventRecord;

                    while ((eventRecord = reader.ReadEvent()) != null)
                    {
                        using (eventRecord)
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
                                        category = logCategoryName,
                                        level = level.ToString(),
                                        timestamp = timestamp,
                                        source = eventRecord.ProviderName,
                                        eventID = eventRecord.Id,
                                        message = eventRecord.FormatDescription(),
                                        computer = eventRecord.MachineName,
                                        taskCategory = eventRecord.TaskDisplayName
                                    };

                                    bool accept = acceptRejectEventLog(me);

                                    if (accept) result.Add(me);
                                }

                                if (timestamp > endTime) endTime = timestamp;
                            }
                            else
                            {
                                break; // stop reading this event file when reaching startTime
                            }
                        }
                    }
                }
            }

            lastLogEntryTime = endTime;
            writeLastLogEntryTimeToSetting();

            return result;
        }

        private bool acceptRejectEventLog(myEventEntry me)
        {
            // reject the log if its source doesn't match the white list, or it matches the black list
            if (eventLogSourceWhiteListEnable && !textMatchWildcardList(eventLogSourceWhiteList, me.source))
            {
                return false;
            }
            if (eventLogSourceBlackListEnable && textMatchWildcardList(eventLogSourceBlackList, me.source))
            {
                return false;
            }

            // reject the log if its ID doesn't match the white list, or it matches the black list
            if (eventLogIdsWhiteListEnable && !textMatchWildcardList(eventLogIdsWhiteList, me.eventID.ToString(), false))
            {
                return false;
            }
            if (eventLogIdsBlackListEnable && textMatchWildcardList(eventLogIdsBlackList, me.eventID.ToString()))
            {
                return false;
            }

            // reject the log if its task category doesn't match the white list, or it matches the black list
            if (eventLogTaskWhiteListEnable && !textMatchWildcardList(eventLogTaskWhiteList, me.taskCategory))
            {
                return false;
            }
            if (eventLogTaskBlackListEnable && textMatchWildcardList(eventLogTaskBlackList, me.taskCategory))
            {
                return false;
            }

            // reject the log if its message doesn't match the white list, or it matches the black list
            if (eventLogMessageWhiteListEnable && !textMatchWildcardList(eventLogMessageWhiteList, me.message))
            {
                return false;
            }
            if (eventLogMessageBlackListEnable && textMatchWildcardList(eventLogMessageBlackList, me.message))
            {
                return false;
            }

            return true;
        }

        private bool textMatchWildcardList(List<string> list, string text, bool autoWarpInStar = true)
        {
            for (int i = 0; i < list.Count; i++)
            {
                string wildcard = list[i];
                if (autoWarpInStar) wildcard = "*" + wildcard + "*";

                if (LikeOperator.LikeString(text, wildcard, Microsoft.VisualBasic.CompareMethod.Text))
                {
                    return true;
                }
            }

            return false;
        }

        private List<string> getEventLogNames()
        {
            var session = new System.Diagnostics.Eventing.Reader.EventLogSession();
            List<string> logNames = new List<string>(session.GetLogNames());
            return logNames;
        }
        #endregion

        #region General System Info

        readonly string scheduler_timeFormat = "HH:mm";
        string scheduler_lastTime = string.Empty;

        private void schedulerLoop()
        {
            if (!dailySystemInfoEmailEnable) return;

            string now = DateTime.Now.ToString(scheduler_timeFormat);
            if (now == scheduler_lastTime) return; // skip this loop if it's still the same minute
            scheduler_lastTime = now;

            // MessageBox.Show("now = " + now);

            if (now == dailySystemInfoEmailTime)
            {
                // MessageBox.Show("scheduler running");
                statusBarText = "Scheduler running";
                sendDailySystemInfoEmail();
            }
        }

        private string getSystemInfo()
        {
            string output = string.Empty;
            string error = string.Empty;

            getSystemInfo_cli.systemInfo.executeTask("getSystemResources.exe", "", true, out output, out error);

            return output;
        }

        // return true on success, false on any exception
        private static bool takeScreenShot(string output = "screenshot.png")
        {
            int screenLeft = SystemInformation.VirtualScreen.Left;
            int screenTop = SystemInformation.VirtualScreen.Top;
            int screenWidth = SystemInformation.VirtualScreen.Width;
            int screenHeight = SystemInformation.VirtualScreen.Height;

            // Create a bitmap of the appropriate size to receive the full-screen screenshot.
            using (Bitmap bitmap = new Bitmap(screenWidth, screenHeight))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    try
                    {
                        g.CopyFromScreen(screenLeft, screenTop, 0, 0, bitmap.Size);
                        bitmap.Save(output);  // saves the image to file, format guessed from filename
                    }
                    catch (Exception ex) when (
                        ex is System.ComponentModel.Win32Exception ||
                        ex is ArgumentNullException ||
                        ex is System.Runtime.InteropServices.ExternalException
                    )
                    {
                        // Win32Exception when the session is locked
                        // ArgumentNullException when output filename is invalid
                        // ExternalException when it fails to output file for whatever reason
                        return false;
                    }
                }
            }
            return true;
        }
        #endregion

        #region Disk
        private void restartDiskTimers()
        {
            timer5.Stop();
            timer5.Interval = checkLocalDiskSpacePeriod * 60 * 1000;
            timer5.Start();
            timer6.Stop();
            timer6.Interval = diagnoseLocalDiskHealthPeriod * 60 * 1000;
            timer6.Start();
        }

        private void checkLocalDiskSpaceUsage()
        {
            bool alert = false;
            var messages = new StringBuilder();

            var allLogicalDrives = systemInfo.getDrive_allLogicalDrives();

            long fixedDriveTotalSize = 0;
            long fixedDriveAvailableSize = 0;

            foreach (var d in allLogicalDrives)
            {
                if (d.iType == 3) // fixed local disk
                {
                    fixedDriveTotalSize += d.size;
                    fixedDriveAvailableSize += d.free;

                    int threshold = (d.isSystemDrive) ? systemPartitionThreshold : otherPartitionThreshold;
                    long dUsedSize = d.size - d.free;
                    double dUsedPercent = 100.0 * dUsedSize / d.size;
                    if (dUsedPercent > threshold)
                    {
                        alert = true;
                        messages.AppendLine(string.Format("Drive: {0} ({1}). Capacity: {2}, {3} used ({4:0.00}%)", d.name, d.volumeName, misc.byteToHumanSize(d.size), misc.byteToHumanSize(dUsedSize), dUsedPercent));

                    }
                }
            }

            alert2InProgress = alert;
            alert2Message = messages.ToString();
        }

        private void diagnoseLocalDiskHealth()
        {
            var HDDs = systemInfo.getHDD_list(true);
            var sb = new StringBuilder();
            var sb2 = new StringBuilder();
            bool alert = false;

            sb2.AppendLine("Disk health summary:");
            sb.AppendFormat("Physical drive count: {0}\n", HDDs.Count);

            for (int i = 0; i < HDDs.Count; i++)
            {
                sb.AppendFormat("HDD #{0}:\n", i + 1);

                var HDD = HDDs[i];

                sb.AppendLine("    Address: " + HDD.addr);
                sb.AppendLine("    Model: " + HDD.model);
                sb.AppendLine("    Capacity: " + misc.byteToHumanSize(HDD.size));
                sb.AppendLine("    S.M.A.R.T info:");
                sb.AppendLine(addPaddingToLines(HDD.smart, "        "));
                sb.AppendLine();

                if (HDD.isOk == false) // note that it can also be null
                {
                    alert = true;
                }

                sb2.AppendFormat("HDD #{0}: {1}\n", i + 1, HDD.health);
            }

            sb2.Append("\nMore detail below.\n \n \n");

            alert3InProgress = alert;
            alert3Message = sb2.ToString() + sb.ToString();
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
            eventLogCategory1 = chbEventLogCategory1.Checked;
            eventLogCategory2 = chbEventLogCategory2.Checked;
            eventLogCategory3 = chbEventLogCategory3.Checked;
            eventLogCategory4 = chbEventLogCategory4.Checked;
            eventLogLevel1 = chbEventLogLevel1.Checked;
            eventLogLevel2 = chbEventLogLevel2.Checked;
            eventLogLevel3 = chbEventLogLevel3.Checked;
            eventLogLevel4 = chbEventLogLevel4.Checked;

            eventLogSourceWhiteListEnable = chbEventLogSourceWhiteList.Checked;
            eventLogSourceBlackListEnable = chbEventLogSourceBlackList.Checked;
            eventLogIdsWhiteListEnable = chbEventLogIdsWhiteList.Checked;
            eventLogIdsBlackListEnable = chbEventLogIdsBlackList.Checked;
            eventLogTaskWhiteListEnable = chbEventLogTaskWhiteList.Checked;
            eventLogTaskBlackListEnable = chbEventLogTaskBlackList.Checked;
            eventLogMessageWhiteListEnable = chbEventLogMessageWhiteList.Checked;
            eventLogMessageBlackListEnable = chbEventLogMessageBlackList.Checked;

            dailySystemInfoEmailEnable = chbDailySystemInfoEmailEnable.Checked;
            dailySystemInfoEmailTime = txtDailySystemInfoEmailTime.Text;

            checkLocalDiskSpaceEnable = chbCheckLocalDiskSpaceEnable.Checked;
            checkLocalDiskSpacePeriod = (int)numCheckLocalDiskUsagePeriod.Value;
            systemPartitionThreshold = (int)numSystemPartitionUsageThreshold.Value;
            otherPartitionThreshold = (int)numOtherPartitionUsageThreshold.Value;

            diagnoseLocalDiskHealthEnable = chbDiagnoseLocalDiskHealthEnable.Checked;
            diagnoseLocalDiskHealthPeriod = (int)numDiagnoseLocalDiskHealthPeriod.Value;

            restartDiskTimers();
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
            chbEventLogCategory1.Checked = eventLogCategory1;
            chbEventLogCategory2.Checked = eventLogCategory2;
            chbEventLogCategory3.Checked = eventLogCategory3;
            chbEventLogCategory4.Checked = eventLogCategory4;
            chbEventLogLevel1.Checked = eventLogLevel1;
            chbEventLogLevel2.Checked = eventLogLevel2;
            chbEventLogLevel3.Checked = eventLogLevel3;
            chbEventLogLevel4.Checked = eventLogLevel4;

            enableDisableEventControls();

            chbEventLogSourceWhiteList.Checked = eventLogSourceWhiteListEnable;
            chbEventLogSourceBlackList.Checked = eventLogSourceBlackListEnable;
            chbEventLogIdsWhiteList.Checked = eventLogIdsWhiteListEnable;
            chbEventLogIdsBlackList.Checked = eventLogIdsBlackListEnable;
            chbEventLogTaskWhiteList.Checked = eventLogTaskWhiteListEnable;
            chbEventLogTaskBlackList.Checked = eventLogTaskBlackListEnable;
            chbEventLogMessageWhiteList.Checked = eventLogMessageWhiteListEnable;
            chbEventLogMessageBlackList.Checked = eventLogMessageBlackListEnable;

            btnCheckOnlineUpdate.Visible = btnTestEventLog.Visible = showTestButton;

            chbDailySystemInfoEmailEnable.Checked = dailySystemInfoEmailEnable;
            txtDailySystemInfoEmailTime.Text = dailySystemInfoEmailTime;

            chbCheckLocalDiskSpaceEnable.Checked = checkLocalDiskSpaceEnable;
            numCheckLocalDiskUsagePeriod.Value = checkLocalDiskSpacePeriod;
            numSystemPartitionUsageThreshold.Value = systemPartitionThreshold;
            numOtherPartitionUsageThreshold.Value = otherPartitionThreshold;

            chbDiagnoseLocalDiskHealthEnable.Checked = diagnoseLocalDiskHealthEnable;
            numDiagnoseLocalDiskHealthPeriod.Value = diagnoseLocalDiskHealthPeriod;
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
            Settings.Set("eventLogCategory1", eventLogCategory1);
            Settings.Set("eventLogCategory2", eventLogCategory2);
            Settings.Set("eventLogCategory3", eventLogCategory3);
            Settings.Set("eventLogCategory4", eventLogCategory4);
            Settings.Set("eventLogLevel1", eventLogLevel1);
            Settings.Set("eventLogLevel2", eventLogLevel2);
            Settings.Set("eventLogLevel3", eventLogLevel3);
            Settings.Set("eventLogLevel4", eventLogLevel4);


            Settings.Set("email_subject_log", email_subject_log);

            Settings.Set("eventLogSourceWhiteListEnable", eventLogSourceWhiteListEnable);
            Settings.Set("eventLogSourceBlackListEnable", eventLogSourceBlackListEnable);
            Settings.Set("eventLogIdsWhiteListEnable", eventLogIdsWhiteListEnable);
            Settings.Set("eventLogIdsBlackListEnable", eventLogIdsBlackListEnable);
            Settings.Set("eventLogTaskWhiteListEnable", eventLogTaskWhiteListEnable);
            Settings.Set("eventLogTaskBlackListEnable", eventLogTaskBlackListEnable);
            Settings.Set("eventLogMessageWhiteListEnable", eventLogMessageWhiteListEnable);
            Settings.Set("eventLogMessageBlackListEnable", eventLogMessageBlackListEnable);

            Settings.Set("eventLogSourceWhiteList", string.Join(",", eventLogSourceWhiteList));
            Settings.Set("eventLogSourceBlackList", string.Join(",", eventLogSourceBlackList));
            Settings.Set("eventLogIdsWhiteList", string.Join(",", eventLogIdsWhiteList));
            Settings.Set("eventLogIdsBlackList", string.Join(",", eventLogIdsBlackList));
            Settings.Set("eventLogTaskWhiteList", string.Join(",", eventLogTaskWhiteList));
            Settings.Set("eventLogTaskBlackList", string.Join(",", eventLogTaskBlackList));
            Settings.Set("eventLogMessageWhiteList", string.Join(",", eventLogMessageWhiteList));
            Settings.Set("eventLogMessageBlackList", string.Join(",", eventLogMessageBlackList));

            Settings.Set("showTestButton", showTestButton);

            Settings.Set("dailySystemInfoEmailEnable", dailySystemInfoEmailEnable);
            Settings.Set("dailySystemInfoEmailTime", dailySystemInfoEmailTime);

            Settings.Set("onlineUpdateUrl", onlineUpdateUrl);
            Settings.Set("onlineUpdateDir", onlineUpdateDir);
            Settings.Set("onlineUpdatePeriod", onlineUpdatePeriod);

            Settings.Set("checkLocalDiskSpaceEnable", checkLocalDiskSpaceEnable);
            Settings.Set("checkLocalDiskSpacePeriod", checkLocalDiskSpacePeriod);
            Settings.Set("systemPartitionThreshold", systemPartitionThreshold);
            Settings.Set("otherPartitionThreshold", otherPartitionThreshold);

            Settings.Set("diagnoseLocalDiskHealthEnable", diagnoseLocalDiskHealthEnable);
            Settings.Set("diagnoseLocalDiskHealthPeriod", diagnoseLocalDiskHealthPeriod);

            writeLastLogEntryTimeToSetting();
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
            splitMultivalueSettingStringToList(email_tos, separatorComma, email_to);

            forwardEventLogs = Settings.Get("forwardEventLogs", false);
            eventLogCategory1 = Settings.Get("eventLogCategory1", false);
            eventLogCategory2 = Settings.Get("eventLogCategory2", false);
            eventLogCategory3 = Settings.Get("eventLogCategory3", false);
            eventLogCategory4 = Settings.Get("eventLogCategory4", false);
            eventLogLevel1 = Settings.Get("eventLogLevel1", false);
            eventLogLevel2 = Settings.Get("eventLogLevel2", false);
            eventLogLevel3 = Settings.Get("eventLogLevel3", false);
            eventLogLevel4 = Settings.Get("eventLogLevel4", false);

            email_subject_log = Settings.Get("email_subject_log", "");

            eventLogSourceWhiteListEnable = Settings.Get("eventLogSourceWhiteListEnable", false);
            eventLogSourceBlackListEnable = Settings.Get("eventLogSourceBlackListEnable", false);
            eventLogIdsWhiteListEnable = Settings.Get("eventLogIdsWhiteListEnable", false);
            eventLogIdsBlackListEnable = Settings.Get("eventLogIdsBlackListEnable", false);
            eventLogTaskWhiteListEnable = Settings.Get("eventLogTaskWhiteListEnable", false);
            eventLogTaskBlackListEnable = Settings.Get("eventLogTaskBlackListEnable", false);
            eventLogMessageWhiteListEnable = Settings.Get("eventLogMessageWhiteListEnable", false);
            eventLogMessageBlackListEnable = Settings.Get("eventLogMessageBlackListEnable", false);

            splitMultivalueSettingStringToList(Settings.Get("eventLogSourceWhiteList", ""), separatorComma, eventLogSourceWhiteList);
            splitMultivalueSettingStringToList(Settings.Get("eventLogSourceBlackList", ""), separatorComma, eventLogSourceBlackList);
            splitMultivalueSettingStringToList(Settings.Get("eventLogIdsWhiteList", ""), separatorComma, eventLogIdsWhiteList);
            splitMultivalueSettingStringToList(Settings.Get("eventLogIdsBlackList", ""), separatorComma, eventLogIdsBlackList);
            splitMultivalueSettingStringToList(Settings.Get("eventLogTaskWhiteList", ""), separatorComma, eventLogTaskWhiteList);
            splitMultivalueSettingStringToList(Settings.Get("eventLogTaskBlackList", ""), separatorComma, eventLogTaskBlackList);
            splitMultivalueSettingStringToList(Settings.Get("eventLogMessageWhiteList", ""), separatorComma, eventLogMessageWhiteList);
            splitMultivalueSettingStringToList(Settings.Get("eventLogMessageBlackList", ""), separatorComma, eventLogMessageBlackList);

            showTestButton = Settings.Get("showTestButton", false);

            dailySystemInfoEmailTime = Settings.Get("dailySystemInfoEmailTime", "");
            dailySystemInfoEmailEnable = Settings.Get("dailySystemInfoEmailEnable", false);

            onlineUpdateUrl = Settings.Get("onlineUpdateUrl", "http://172.21.160.62/systemResourceAlerter/update.txt");
            onlineUpdateDir = Settings.Get("onlineUpdateDir", "updater");
            onlineUpdatePeriod = Settings.Get("onlineUpdatePeriod", 900);

            if (onlineUpdatePeriod > 0)
            {
                timer4.Interval = onlineUpdatePeriod * 1000;
                timer4.Stop();
                timer4.Start();
            }

            string timeTmp = Settings.Get("lastLogEntryTime", string.Empty);
            lastLogEntryTime = parseDateTime(timeTmp, lastLogEntryTime_maxDistance);

            checkLocalDiskSpaceEnable = Settings.Get("checkLocalDiskSpaceEnable", true);
            checkLocalDiskSpacePeriod = Settings.Get("checkLocalDiskSpacePeriod", 60);
            systemPartitionThreshold = Settings.Get("systemPartitionThreshold", 90);
            otherPartitionThreshold = Settings.Get("otherPartitionThreshold", 90);

            diagnoseLocalDiskHealthEnable = Settings.Get("diagnoseLocalDiskHealthEnable", true);
            diagnoseLocalDiskHealthPeriod = Settings.Get("diagnoseLocalDiskHealthPeriod", 60);
        }

        private string[] separatorComma = new string[] { "," };
        private void splitMultivalueSettingStringToList(string source, string[] separator, List<string> target)
        {
            target.Clear();
            string[] array = source.Split(separator, StringSplitOptions.None);
            for (int i = 0; i < array.Length; i++)
            {
                string sub = array[i].Trim();
                if (sub.Length > 0)
                {
                    target.Add(sub);
                }
            }
        }

        private void writeLastLogEntryTimeToSetting()
        {
            Settings.Set("lastLogEntryTime", formatDateTime(lastLogEntryTime));
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
            if (forwardEventLogs && !backgroundWorker1.IsBusy)
            {
                backgroundWorker1.RunWorkerAsync();
            }
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

            labelEditText(lblStatusBar, statusBarText);
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
            chbEventLogCategory1.Enabled = chbEventLogCategory2.Enabled = chbEventLogCategory3.Enabled = chbEventLogCategory4.Enabled = chbForwardEventLogs.Checked;
            chbEventLogLevel1.Enabled = chbEventLogLevel2.Enabled = chbEventLogLevel3.Enabled = chbEventLogLevel4.Enabled = chbForwardEventLogs.Checked;
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            sendEmailForwardEventLog();
        }

        private void editBlackWhiteList(ref List<string> list)
        {
            var options = new Form2();
            options.loadList(list);

            if (options.ShowDialog() == DialogResult.OK)
            {
                list = options.saveList();
            }
        }

        private void lnkEventLogSourceWhiteList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            editBlackWhiteList(ref eventLogSourceWhiteList);
        }

        private void lnkEventLogSourceBlackList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            editBlackWhiteList(ref eventLogSourceBlackList);
        }

        private void lnkEventLogIdsWhiteList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            editBlackWhiteList(ref eventLogIdsWhiteList);
        }

        private void lnkEventLogIdsBlackList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            editBlackWhiteList(ref eventLogIdsBlackList);
        }

        private void lnkEventLogTaskWhiteList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            editBlackWhiteList(ref eventLogTaskWhiteList);
        }

        private void lnkEventLogTaskBlackList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            editBlackWhiteList(ref eventLogTaskBlackList);
        }

        private void lnkEventLogMessageWhiteList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            editBlackWhiteList(ref eventLogMessageWhiteList);
        }

        private void lnkEventLogMessageBlackList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            editBlackWhiteList(ref eventLogMessageBlackList);
        }

        private void btnTestEventLog_Click(object sender, EventArgs e)
        {
            applySettings();

            lastLogEntryTime = DateTime.Now.Subtract(new TimeSpan(24, 00, 0));

            List<myEventEntry> myEventEntries = readEventLog();

            string message = string.Format("myEventEntries count = {0}", myEventEntries.Count);
            MessageBox.Show(message);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(message);
            string separator = "=======================================================";

            for (int i = 0; i < myEventEntries.Count; i++)
            {
                myEventEntry me = myEventEntries[i];
                string email_body = writeEventForwardEmailBody(me);

                sb.AppendLine();
                sb.AppendLine(separator);
                sb.AppendLine();
                sb.AppendLine(email_body);
            }

            string testResultFilename = string.Format("eventLogTestResult_{0}.txt", getNowStringForFilename());
            System.IO.File.WriteAllText(testResultFilename, sb.ToString());

            MessageBox.Show(string.Format("myEventEntries written to file {0}", testResultFilename));

            sendDailySystemInfoEmail();
            MessageBox.Show("Sent general system information to email!");
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            schedulerLoop();
        }

        private void btnCheckOnlineUpdate_Click(object sender, EventArgs e)
        {
            MessageBox.Show("File version = " + Application.ProductVersion);
            checkOnlineForUpdate(true);
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            checkOnlineForUpdate();
        }

        private void sendSystemInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applySettings();
            sendDailySystemInfoEmail();
        }

        private void sendResourceStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            applySettings();
            checkLocalDiskSpaceUsage();
            sendEmailAlert(true, true);
        }

        private void updateAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkOnlineForUpdate(false);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Environment.Exit(0);
        }

        private void timer5_Tick(object sender, EventArgs e)
        {
            if (checkLocalDiskSpaceEnable)
            {
                checkLocalDiskSpaceUsage();
            }
        }

        private void timer6_Tick(object sender, EventArgs e)
        {
            if (diagnoseLocalDiskHealthEnable)
            {
                diagnoseLocalDiskHealth();
                sendHddHealthAlert();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            diagnoseLocalDiskHealth();
            sendHddHealthAlert(true, true);
        }

        #endregion
    }

}
