using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace getSystemInfo_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Hostname to connect: ");
            string hostname = Console.ReadLine();
            hostname = hostname.Trim();

            int connectionResult = 0;
            bool connectionIsLocal = true;

            if (hostname.ToLower() == "localhost" || hostname == "127.0.0.1" || hostname.ToLower() == Environment.MachineName.ToLower())
            {
                connectionIsLocal = true;
                connectionResult = systemInfo.setupContextLocal();
            }
            else
            {
                connectionIsLocal = false;
                Console.Write("Use Windows Authentication? [y/n] ");
                var userInput = Console.ReadKey();
                string selection = userInput.KeyChar.ToString().ToLower();

                Console.WriteLine();
                if (selection != "n")
                {
                    Console.WriteLine("Setting up remote WMi and registry connections with Windows Authentication...");
                    connectionResult = systemInfo.setupContextRemote(hostname, true);
                }
                else
                {
                    Console.Write("Enter username: ");
                    string username = Console.ReadLine();

                    Console.Write("Enter password: ");
                    // hide keypress, from https://stackoverflow.com/a/3404522
                    string password = "";
                    do
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);
                        // Backspace Should Not Work
                        if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                        {
                            password += key.KeyChar;
                            Console.Write("*");
                        }
                        else
                        {
                            if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                            {
                                password = password.Substring(0, (password.Length - 1));
                                Console.Write("\b \b");
                            }
                            else if (key.Key == ConsoleKey.Enter)
                            {
                                break;
                            }
                        }
                    } while (true);
                    Console.WriteLine();

                    connectionResult = systemInfo.setupContextRemote(hostname, false, username, password);
                }
            }

            Console.WriteLine("Context status = {0}", connectionResult);
            if (connectionResult != 0)
            {
                string localOrRemote = connectionIsLocal ? "Local" : "Remote";
                if (connectionResult == -1 || connectionResult == -3)
                {
                    Console.WriteLine("{0} WMI connection failed.", localOrRemote);
                }
                if (connectionResult == -2 || connectionResult == -3)
                {
                    Console.WriteLine("{0} registry connection failed.", localOrRemote);
                }

                Console.Write("Continue? [y/n] ");
                var userInput = Console.ReadKey();
                if (userInput.KeyChar.ToString().ToLower() != "y")
                {
                    System.Environment.Exit(1);
                }
            }

            Console.WriteLine("Getting system information...");
            string outputSysInfoLog = string.Format("system_info_{0}_{1}.txt", hostname, getNowStringForFilename());
            Console.WriteLine();
            getSystemResources(outputSysInfoLog);

            Console.WriteLine("Getting installed program list...");
            string outputProgramCsv = string.Format("programList_remote_{0}_{1}.csv", hostname, getNowStringForFilename());
            misc.writeCsv(systemInfo.getProgram_list(), outputProgramCsv, true);
            Console.WriteLine("Written list to {0}", outputProgramCsv);

            Console.WriteLine("Done.");
            Console.ReadLine();
        }

        static void getSystemResources(string outputFname)
        {
            sbAppendWriteLine("System Time: " + formatDateTime(systemInfo.getSystem_time()));
            sbAppendWriteLine("Application Version: " + Application.ProductVersion);

            #region System
            sbAppendWriteLine("Mainboard Manufacturer: {0}", systemInfo.getMobo_manufacturer());
            sbAppendWriteLine("Mainboard Model: {0}", systemInfo.getMobo_model());
            sbAppendWriteLine("Computer Serial Number: {0}", systemInfo.getMobo_serialNumber());

            sbAppendWriteLine("System Manufacturer: {0}", systemInfo.getSystem_manufacturer());
            sbAppendWriteLine("System Model: {0}", systemInfo.getSystem_model());

            // Console.WriteLine(misc.byteToHumanSize(systemInfo.getSystem_systemDriveFreeSpace()));
            // Console.WriteLine(misc.byteToHumanSize(systemInfo.getSystem_systemDriveTotalSpace()));

            sbAppendWriteLine("System OS: {0}", systemInfo.getSystem_OS());
            sbAppendWriteLine("System OS Root Path: {0}", systemInfo.getSystem_rootPath());
            sbAppendWriteLine("System Language: {0}", systemInfo.getSystem_language());
            sbAppendWriteLine("Computer Name: {0}", systemInfo.getSystem_name());
            sbAppendWriteLine("Domain Name: {0}", systemInfo.getSystem_domain());

            sbAppendWriteLine("Current Username: {0}", systemInfo.getSystem_currentUsername());
            var loggonUsers = systemInfo.getSystem_listLoggonUsers().ToArray();
            sbAppendWriteLine("Logged on Users: {0}", loggonUsers.Length);
            sbAppendWriteLine(loggonUsers);

            sbAppendWriteLine("System Up Time: {0}", systemInfo.getSystem_uptime_str());
            #endregion

            #region CPU
            var allCPUs = systemInfo.getCPU_all();
            sbAppendWriteLine("CPU Socket Count: {0}", allCPUs.Count);
            for (int i = 0; i < allCPUs.Count; i++)
            {
                sbAppendWriteLine("CPU #{0}", i + 1);
                sbAppendWriteLine("    CPU Name: {0}", allCPUs[i][0]);
                sbAppendWriteLine("    CPU Core Count: {0}", allCPUs[i][1]);
                sbAppendWriteLine("    CPU Thread Count: {0}", allCPUs[i][2]);
                sbAppendWriteLine("    CPU Clock Speed: {0}", allCPUs[i][3]);
            }

            var p = systemInfo.getCPU_usageAvg(5, 500);
            sbAppendWriteLine("CPU Usage (via WMI): {0:0.00}%", p);

            #endregion

            #region RAM
            long total = systemInfo.getMemory_visibleSize();
            long available = systemInfo.getMemory_freeSize();
            long used = total - available;
            double usedpercent = 100.0 * used / total;
            double availablepercent = 100.0 * available / total;
            sbAppendWriteLine("RAM Total Capacity: {0}", misc.byteToHumanSize(total));
            sbAppendWriteLine("RAM Used Size: {0} ({1:0.00}%)", misc.byteToHumanSize(used), usedpercent);
            sbAppendWriteLine("RAM Free Size: {0} ({1:0.00}%)", misc.byteToHumanSize(available), availablepercent);

            sbAppendWriteLine("RAM Slot Count: {0}", systemInfo.getMemory_slotCount());
            sbAppendWriteLine("RAM Stick Count: {0}", systemInfo.getMemory_stickCount());

            try
            {
                List<string[]> RAMs = systemInfo.getMemory_stickList();

                for (int i = 0; i < RAMs.Count; i++)
                {
                    sbAppendWriteLine("RAM Stick #{0}:", i + 1);
                    sbAppendWriteLine("    Manufacturer: {0}", RAMs[i][0]);
                    sbAppendWriteLine("    PartNumber: {0}", RAMs[i][1]);
                    sbAppendWriteLine("    Capacity: {0}", misc.byteToHumanSize(long.Parse(RAMs[i][2])));
                    sbAppendWriteLine("    Bus Speed: {0} MHz", RAMs[i][3]);
                    sbAppendWriteLine("    Type: {0}", RAMs[i][4]);
                }
            }
            catch (Exception)
            {
                sbAppendWriteLine("Failed to get RAM Stick.");
            }
            #endregion

            #region HDD
            var allLogicalDrives = systemInfo.getDrive_allLogicalDrives();

            sbAppendWriteLine("Logical Drive Count: {0}", allLogicalDrives.Count);

            bool showFixedDriveStats = true;
            long fixedDriveTotalSize = 0;
            long fixedDriveAvailableSize = 0;

            foreach (var d in allLogicalDrives)
            {
                sbAppendWriteLine("    Drive {0}", d.name);
                sbAppendWriteLine("        Drive type: {0}", d.sType);
                sbAppendWriteLine("        System Drive: {0}", d.isSystemDrive);
                sbAppendWriteLine("        Volume label: {0}", d.volumeName);
                sbAppendWriteLine("        File system: {0}", d.fileSystem);
                sbAppendWriteLine("        Total size of drive: {0} ", misc.byteToHumanSize(d.size));
                sbAppendWriteLine("        Total available space: {0}", misc.byteToHumanSize(d.free));

                if (showFixedDriveStats && d.size > 0)
                {
                    long dUsedSize = d.size - d.free;
                    double dUsedPercent = 100.0 * dUsedSize / d.size;
                    // sbAppendWriteLine("    Fixed drives total size: {0}", misc.byteToHumanSize(fixedDriveTotalSize));
                    sbAppendWriteLine("        Your fixed stats: {0}, {1} used ({2:0.00}%)", misc.byteToHumanSize(d.size), misc.byteToHumanSize(dUsedSize), dUsedPercent);
                }

                if (d.iType == 3)
                {
                    fixedDriveTotalSize += d.size;
                    fixedDriveAvailableSize += d.free;
                }
            }

            if (showFixedDriveStats)
            {
                long fixedDriveUsedSize = fixedDriveTotalSize - fixedDriveAvailableSize;
                double fixedDriveUsedPercent = 100.0 * fixedDriveUsedSize / fixedDriveTotalSize;
                sbAppendWriteLine("    Your fixed stats: {0}, {1} used ({2:0.00}%)", misc.byteToHumanSize(fixedDriveTotalSize), misc.byteToHumanSize(fixedDriveUsedSize), fixedDriveUsedPercent);
            }

            var HDDs = systemInfo.getHDD_list(true);
            sbAppendWriteLine("Physical Drive Count: {0}", HDDs.Count);

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
                sbAppendWriteLine("    Type: {0}", network.type);
                sbAppendWriteLine("    Description: {0}", network.description);
                sbAppendWriteLine("    Device Instance Path: {0}", network.PnpInstanceID);
                sbAppendWriteLine("    GUID: {0}", network.guid);
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
                    if (thisInterface.Id == network.guid)
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

            #region Monitor
            var gpu = systemInfo.getVideo_adapter();
            sbAppendWriteLine("Display Adapter: {0}", gpu.Count);
            sbAppendWriteLine(gpu.ToArray());

            var monitors = systemInfo.getVideo_monitor_wrapper();
            sbAppendWriteLine("Monitor Count: {0}", monitors.Count);
            sbAppendWriteLine(monitors.ToArray());

            #endregion

            sbAppendWriteLine("End Time: " + getNowString());
            string outputFilePath = outputFname;
            try
            {
                File.WriteAllText(outputFilePath, sb.ToString());
                Console.WriteLine("Log File: {0}", outputFilePath);
            }
            catch (IOException e)
            {
                Console.WriteLine("Can't write to log file:");
                Console.WriteLine(addPaddingToLines(e.ToString(), "    "));
            }

        }

        static StringBuilder sb = new StringBuilder();

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

        public static void sbAppendWriteLine(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                string s = arr[i];
                string line = ("    " + (i + 1).ToString() + ": " + s);
                sbAppendWriteLine(line);
            }
        }
    }
}
