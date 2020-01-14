using getSystemInfo_cli;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace getSystemResources
{
    class Program
    {
        static StringBuilder sb = new StringBuilder();

        static void Main(string[] args)
        {

            //PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            //float t = cpuCounter.NextValue(); // the first reading is always 0

            //for (int i = 0; i < 1; i++)
            //{
            //    System.Threading.Thread.Sleep(1000); // wait a second to get a valid reading
            //    t = cpuCounter.NextValue(); // this time it will be correct
            //    Console.WriteLine(t);
            //}

            sbAppendWriteLine("System Time: " + getNowString());

            #region System
            sbAppendWriteLine("Mainboard Manufacturer: {0}", systemInfo.getMobo_manufacturer());
            sbAppendWriteLine("Mainboard Model: {0}", systemInfo.getMobo_model());
            sbAppendWriteLine("Computer Serial Number: {0}", systemInfo.getMobo_serialNumber());

            sbAppendWriteLine("System Manufacturer: {0}", systemInfo.getSystem_manufacturer());
            sbAppendWriteLine("System Model: {0}", systemInfo.getSystem_model());

            // Console.WriteLine(misc.byteToHumanSize(systemInfo.getSystem_systemDriveFreeSpace()));
            // Console.WriteLine(misc.byteToHumanSize(systemInfo.getSystem_systemDriveTotalSpace()));

            sbAppendWriteLine("System OS: {0}", systemInfo.getSystem_OS());
            sbAppendWriteLine("System Language: {0}", systemInfo.getSystem_language());
            sbAppendWriteLine("Computer Name: {0}", systemInfo.getSystem_name());
            sbAppendWriteLine("Domain Name: {0}", systemInfo.getSystem_domain());

            sbAppendWriteLine("Current Username: {0}", System.Security.Principal.WindowsIdentity.GetCurrent().Name);

            sbAppendWriteLine("System Up Time: {0}", systemInfo.getSystem_uptime_str());
            #endregion

            #region CPU
            var allCPUs = systemInfo.getCPU_all();
            sbAppendWriteLine("CPU Socket Count: {0}", allCPUs.Count);
            for (int i = 0; i < allCPUs.Count; i++)
            {
                sbAppendWriteLine("CPU #{0}", i);
                sbAppendWriteLine("    CPU Name: {0}", allCPUs[i][0]);
                sbAppendWriteLine("    CPU Core Count: {0}", allCPUs[i][1]);
                sbAppendWriteLine("    CPU Thread Count: {0}", allCPUs[i][2]);
                sbAppendWriteLine("    CPU Clock Speed: {0}", allCPUs[i][3]);
            }

            try
            {
                PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                float t = cpuCounter.NextValue(); // the first reading is always 0
                System.Threading.Thread.Sleep(1000); // wait a second to get a valid reading
                t = cpuCounter.NextValue(); // this time it will be correct

                sbAppendWriteLine("CPU Usage: {0:0.00}%", t);
            }
            catch (System.InvalidOperationException)
            {
                sbAppendWriteLine("CPU Usage: N/A");
            }
            #endregion

            #region RAM
            var comInfo = new Microsoft.VisualBasic.Devices.ComputerInfo();
            long total = (long)comInfo.TotalPhysicalMemory;
            long available = (long)comInfo.AvailablePhysicalMemory;
            long used = total - available;
            double usedpercent = 100.0 * used / total;
            double availablepercent = 100.0 * available / total;
            sbAppendWriteLine("RAM Total Capacity: {0}", misc.byteToHumanSize(total));
            sbAppendWriteLine("RAM Used Size: {0} ({1:0.00}%)", misc.byteToHumanSize(used), usedpercent);
            sbAppendWriteLine("RAM Free Size: {0} ({1:0.00}%)", misc.byteToHumanSize(available), availablepercent);

            sbAppendWriteLine("RAM Slot Count: {0}", systemInfo.getRAM_slotCount());
            sbAppendWriteLine("RAM Stick Count: {0}", systemInfo.getRAM_stickCount());

            List<string[]> RAMs = systemInfo.getRAM_stickList();

            for (int i = 0; i < RAMs.Count; i++)
            {
                sbAppendWriteLine("RAM Stick #{0}:", i + 1);
                sbAppendWriteLine("    Manufacturer: {0}", RAMs[i][0]);
                sbAppendWriteLine("    PartNumber: {0}", RAMs[i][1]);
                sbAppendWriteLine("    Capacity: {0}", misc.byteToHumanSize(long.Parse(RAMs[i][2])));
                sbAppendWriteLine("    Bus Speed: {0} MHz", RAMs[i][3]);
                sbAppendWriteLine("    Type: {0}", RAMs[i][4]);
            }
            #endregion

            #region HDD

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            sbAppendWriteLine("Logical Drive Count: {0}", allDrives.Length);

            bool showFixedDriveStats = true;
            long fixedDriveTotalSize = 0;
            long fixedDriveAvailableSize = 0;

            foreach (DriveInfo d in allDrives)
            {
                sbAppendWriteLine("    Drive {0}", d.Name);
                sbAppendWriteLine("        Drive type: {0}", d.DriveType);
                if (d.IsReady == true)
                {
                    sbAppendWriteLine("        Volume label: {0}", d.VolumeLabel);
                    sbAppendWriteLine("        File system: {0}", d.DriveFormat);
                    sbAppendWriteLine("        Available space to current user: {0}", misc.byteToHumanSize(d.AvailableFreeSpace));
                    sbAppendWriteLine("        Total available space: {0}", misc.byteToHumanSize(d.TotalFreeSpace));
                    sbAppendWriteLine("        Total size of drive: {0} ", misc.byteToHumanSize(d.TotalSize));

                    if (showFixedDriveStats)
                    {
                        long dUsedSize = d.TotalSize - d.TotalFreeSpace;
                        double dUsedPercent = 100.0 * dUsedSize / d.TotalSize;
                        // sbAppendWriteLine("    Fixed drives total size: {0}", misc.byteToHumanSize(fixedDriveTotalSize));
                        sbAppendWriteLine("        Your fixed stats: {0}, {1} used ({2:0.00}%)", misc.byteToHumanSize(d.TotalSize), misc.byteToHumanSize(dUsedSize), dUsedPercent);
                    }

                    if (d.DriveType == DriveType.Fixed)
                    {
                        fixedDriveTotalSize += d.TotalSize;
                        fixedDriveAvailableSize += d.TotalFreeSpace;
                    }

                }
            }

            if (showFixedDriveStats)
            {
                long fixedDriveUsedSize = fixedDriveTotalSize - fixedDriveAvailableSize;
                double fixedDriveUsedPercent = 100.0 * fixedDriveUsedSize / fixedDriveTotalSize;
                sbAppendWriteLine("    Your fixed stats: {0}, {1} used ({2:0.00}%)", misc.byteToHumanSize(fixedDriveTotalSize), misc.byteToHumanSize(fixedDriveUsedSize), fixedDriveUsedPercent);
            }

            var HDDs = systemInfo.getHDD_list();
            sbAppendWriteLine("Physical HDD Count: {0}", HDDs.Count);

            for (int i = 0; i < HDDs.Count; i++)
            {
                sbAppendWriteLine("HDD #{0}:", i + 1);

                var HDD = HDDs[i];

                sbAppendWriteLine("    Address: " + HDD.addr);
                sbAppendWriteLine("    Model: " + HDD.model);
                sbAppendWriteLine("    Capacity: " + misc.byteToHumanSize(HDD.size));
                sbAppendWriteLine("    S.M.A.R.T Info:");
                sbAppendWriteLine(addPaddingToLines(HDD.smart, "        "));
            }
            #endregion

            #region Network
            var networkInterface = systemInfo.getNetwork_interfaces();
            NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            sbAppendWriteLine("Network Interface Count: {0}", networkInterface.Count);

            for (int i = 0; i < networkInterface.Count; i++)
            {
                var network = networkInterface[i];
                sbAppendWriteLine("Network Interface #{0}:", i + 1);
                sbAppendWriteLine("    Name: {0}", network.name);
                sbAppendWriteLine("    Description: {0}", network.description);
                sbAppendWriteLine("    MAC Address: {0}", network.MAC);
                sbAppendWriteLine("    Status: {0}", network.isUp ? "Up" : "Down");
                sbAppendWriteLine("    Speed: {0}", misc.bpsToHumanSize(network.speed));
                sbAppendWriteLine("    DHCP Enabled: {0}", network.isDhcpEnabled ? "Yes" : "No");
                sbAppendWriteLine("    IP Addresses ({0}):", network.ipAddresses.Count);
                foreach (var ip in network.ipAddresses)
                {
                    sbAppendWriteLine("        {0} {1}", ip.ipAddr, ip.netMask);
                }
                sbAppendWriteLine("    Gateways ({0}):", network.gateways.Count);
                foreach (var gate in network.gateways)
                {
                    sbAppendWriteLine("        {0}", gate);
                }
                sbAppendWriteLine("    DNS Servers ({0}):", network.dnsServers.Count);
                foreach (var dns in network.dnsServers)
                {
                    sbAppendWriteLine("        {0}", dns);
                }

                // get the current throughput of this interface
                foreach (var thisInterface in allNetworkInterfaces)
                {
                    if (thisInterface.Name == network.name)
                    {
                        if (thisInterface.OperationalStatus == OperationalStatus.Up)
                        {
                            var firstTime = DateTime.Now;
                            var interfaceStatistic = thisInterface.GetIPv4Statistics();

                            // get the latest sent/received byte count
                            long lngBytesSent = interfaceStatistic.BytesSent;
                            long lngBytesReceived = interfaceStatistic.BytesReceived;

                            // wait 1s, then get the stats again, and calculate the difference
                            // but don't trust it to be 1000 ms, as Sleep is not accurate
                            System.Threading.Thread.Sleep(1000);
                            var secondTime = DateTime.Now;
                            int sleepTime = (int)(secondTime - firstTime).TotalMilliseconds;

                            interfaceStatistic = thisInterface.GetIPv4Statistics();
                            long bytesSentDelta = interfaceStatistic.BytesSent - lngBytesSent;
                            long bytesReceivedDelta = interfaceStatistic.BytesReceived - lngBytesReceived;

                            long bitSentSpeed = (bytesSentDelta) * 8 * (sleepTime / 1000);
                            long bitReceivedSpeed = (bytesReceivedDelta) * 8 * (sleepTime / 1000);

                            double sendSpeedPerc = 100.0 * bitSentSpeed / thisInterface.Speed;
                            double recvSpeedPerc = 100.0 * bitReceivedSpeed / thisInterface.Speed;

                            sbAppendWriteLine("    Stats:");
                            // sbAppendLine(string.Format("        TX Bytes: {0}", bytesSentDelta));
                            // sbAppendLine(string.Format("        RX Bytes: {0}", bitReceivedSpeed));
                            sbAppendWriteLine("        TX Speed: {0} ({1:0.00}%)", misc.bpsToHumanSize(bitSentSpeed), sendSpeedPerc);
                            sbAppendWriteLine("        RX Speed: {0} ({1:0.00}%)", misc.bpsToHumanSize(bitReceivedSpeed), recvSpeedPerc);
                        }

                        break;
                    }
                }
            }
            #endregion

            File.WriteAllText(Path.Combine(Application.StartupPath.ToString(), string.Format("system_info_{0}.txt", getNowStringForFilename())), sb.ToString());

            Console.WriteLine("Done.");
            // Console.ReadLine();
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

        public static void sbAppendWriteLine(string text)
        {
            Console.WriteLine(text);
            sb.AppendLine(text);
        }

        public static void sbAppendWriteLine(string text, params object[] arg)
        {
            Console.WriteLine(text, arg);
            sb.AppendLine(string.Format(text, arg));
        }
    }
}
