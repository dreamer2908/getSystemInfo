using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Diagnostics;
using getSystemInfo_cli;

namespace systemResourceAlerter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            timer1.Interval = 1000;
            timer1.Start();

            timer2.Interval = 60000;
            timer2.Start();
            
            notifyIcon1.Visible = true;
        }

        // todo: GUI, setting

        string email_host = "172.21.160.13";
        int email_port = 25;
        bool email_ssl = false;
        string email_from = "camera-alert@prs-vn.com.hk";
        string email_user = "camera-alert@prs-vn.com.hk";
        string email_password = "123456";
        string email_to = "vn-mis3@pihlgp.com";
        string email_subject = "Lilin NAV System Resource Usage Alert";

        Queue<double> cpuUsageHistory = new Queue<double>();
        Queue<double> ramUsageHistory = new Queue<double>();

        PerformanceCounter cpuCounter;

        int cpuHistoryMax = 60;
        int ramHistoryMax = 60;

        double cpuThreshold = 90.0;
        double ramThreshold = 90.0;

        bool alertInProgress = false;
        DateTime alertBegin = DateTime.MinValue;

        DateTime lastEmailTimestamp = DateTime.MinValue;
        double delayBetweenEmails = 900; // seconds

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

            while (ramUsageHistory.Count > ramHistoryMax) ramUsageHistory.Dequeue();
        }

        private void checkThreshold()
        {
            double avgCPU = queueCalcAverage(cpuUsageHistory);
            double avgRAM = queueCalcAverage(ramUsageHistory);
            var now = DateTime.Now;

            if (avgCPU > cpuThreshold || avgRAM > ramThreshold)
            {
                // set alertBegin if this is the beginning of an alert
                if (!alertInProgress)
                {
                    alertBegin = now;
                }
                alertInProgress = true;
            } 
            else
            {
                alertInProgress = false;
                alertBegin = DateTime.MinValue;
            }
        }
        #endregion

        #region email
        private void sendEmail(string email_to, string email_subject, string email_body)
        {
            sendEmail(email_host, email_port, email_ssl, email_from, email_user, email_password, email_to, email_subject, email_body);
        }

        private void sendEmail(string email_host, int email_port, bool email_ssl, string email_from, string email_user, string email_password, string email_to, string email_subject, string email_body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(email_host);

                mail.From = new MailAddress(email_from);
                mail.To.Add(email_to);
                mail.Subject = email_subject;
                mail.Body = email_body;

                SmtpServer.Port = email_port;
                SmtpServer.Credentials = new System.Net.NetworkCredential(email_user, email_password);
                SmtpServer.EnableSsl = email_ssl;

                SmtpServer.Send(mail);
                MessageBox.Show("mail Send");
                Console.WriteLine("mail Send");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
        }

        private void sendEmailAlert(bool ignoreDelay=false)
        {
            // only send email if more than delayBetweenEmails seconds has passed since last email
            // or when delay is asked to be ignored
            double secondSinceLastEmail = (DateTime.Now - lastEmailTimestamp).TotalSeconds;

            if (alertInProgress && (ignoreDelay || secondSinceLastEmail >= delayBetweenEmails))
            {
                string email_body = "At system time: " + getNowString();
                double avgCPU = queueCalcAverage(cpuUsageHistory);
                double avgRAM = queueCalcAverage(ramUsageHistory);
                var alertDuration = (DateTime.Now - alertBegin);

                email_body += "\n\n" + string.Format("Average CPU usage over {1} seconds period is {0:0.00}%.", avgCPU, cpuUsageHistory.Count);
                email_body += "\n" + string.Format("Average RAM usage over {1} seconds period is {0:0.00}%.", avgRAM, ramUsageHistory.Count);
                email_body += "\n\n" + string.Format("Alert detected at: {0}. \nElapsed Time: {1}\n", formatDateTime(alertBegin), formatTimeSpan(alertDuration));

                lastEmailTimestamp = DateTime.Now;
                sendEmail(email_to, email_subject, email_body);
            }
        }
        #endregion

        #region UI stuff
        private void timer1_Tick(object sender, EventArgs e)
        {
            queueCpuUsage();
            queueRamUsage();
            checkThreshold();
            sendEmailAlert();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            notifyIcon1.Visible = false;
        }
        #endregion

    }

}
