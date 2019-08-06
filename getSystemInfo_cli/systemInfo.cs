using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Globalization;
using System.IO;
using WindowsDisplayAPI;
using System.Net.NetworkInformation;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Net;
using System.Diagnostics;

namespace getSystemInfo_cli
{
    class systemInfo
    {
        #region lookupValue

        private static string lookupValue_first(string wantedClass, string wantedValue)
        {
            string result = String.Empty;
            ManagementClass mc = new ManagementClass(wantedClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                result = mo.Properties[wantedValue].Value.ToString();
                break;
            }
            return result;
        }

        private static List<string> lookupValue_all(string wantedClass, string wantedValue)
        {
            List<string> result = new List<string>();
            ManagementClass mc = new ManagementClass(wantedClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                result.Add(mo.Properties[wantedValue].Value.ToString());
            }
            return result;
        }

        private static List<string[]> lookupValue_all(string wantedClass, string[] wantedValues)
        {
            List<string[]> results = new List<string[]>();

            ManagementClass mc = new ManagementClass(wantedClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                string[] oneResult = new string[wantedValues.Length];
                for (int i = 0; i < wantedValues.Length; i++)
                {
                    oneResult[i] = mo.Properties[wantedValues[i]].Value.ToString();
                }
                results.Add(oneResult);
            }
            return results;
        }

        private static int lookupValue_count(string wantedClass, string wantedValue)
        {
            int count = 0;
            ManagementClass mc = new ManagementClass(wantedClass);
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo.Properties[wantedValue].Value != null)
                {
                    count += 1;
                }
            }
            return count;
        }

        #endregion

        #region getCPU
        public static string getCPU_name()
        {
            return lookupValue_first("win32_processor", "Name");
        }

        public static string getCPU_coreCount()
        {
            return lookupValue_first("win32_processor", "NumberOfCores");
        }

        public static string getCPU_threadCount()
        {
            return lookupValue_first("win32_processor", "NumberOfLogicalProcessors");
        }

        public static string getCPU_clockSpeed()
        {
            return lookupValue_first("win32_processor", "MaxClockSpeed");
        }

        public static string getCPU_socket()
        {
            return lookupValue_first("win32_processor", "SocketDesignation");
        }

        #endregion

        #region getRAM

        // count number of installed ram sticks
        public static string getRAM_stickCount()
        {
            return lookupValue_count("Win32_PhysicalMemory", "Capacity").ToString();
        }

        // count number of physical ram slot
        public static string getRAM_slotCount()
        {
            return lookupValue_first("Win32_PhysicalMemoryArray", "MemoryDevices");
        }

        // get total physical memory
        public static long getRAM_totalCapacity()
        {
            List<string> capacityList = lookupValue_all("Win32_PhysicalMemory", "Capacity");
            long total = 0;
            foreach (string capacity in capacityList)
            {
                long tmp = long.Parse(capacity);
                total += tmp;
            }
            return total;

            // can also use this, but the return value is slightly different
            // return long.Parse(lookupValue_first("Win32_ComputerSystem", "TotalPhysicalMemory"));
        }

        // list all install ram sticks, return a list of string array of <manufacturer> <model> <capacity> <speed>
        public static List<string[]> getRAM_stickList()
        {
            return lookupValue_all("Win32_PhysicalMemory", new string[] { "Manufacturer", "PartNumber", "Capacity", "Speed" });
        }

        #endregion

        #region getMobo

        public static string getMobo_manufacturer()
        {
            return lookupValue_first("Win32_BaseBoard", "Manufacturer");
        }

        public static string getMobo_model()
        {
            return lookupValue_first("Win32_BaseBoard", "Product");
        }

        #endregion

        #region getHDD

        // get physical HDD list: model name, capacity, smart info
        // todo: function to run self tests
        public struct struct_hddInfo
        {
            public string addr;
            public string model;
            public long size;
            public string smart;
        }

        public static List<struct_hddInfo> getHDD_list()
        {
            List<struct_hddInfo> result = new List<struct_hddInfo>();
            List<string[]> hdds = lookupValue_all("Win32_DiskDrive", new string[] { "Name", "Model", "Size" });

            foreach (string[] hdd in hdds)
            {
                struct_hddInfo thisOne = new struct_hddInfo
                {
                    addr = hdd[0],
                    model = hdd[1],
                    size = varToLong(hdd[2]),
                    smart = getHddSmartInfo(hdd[0])
                };
                //Console.WriteLine(thisOne.name);
                //Console.WriteLine(thisOne.model);
                //Console.WriteLine(thisOne.size);
                //Console.WriteLine(thisOne.smart);
                result.Add(thisOne);
            }

            return result;
        }

        private static string getHddSmartInfo(string addr)
        {
            // use smartmontools to get HDD S.M.A.R.T info
            // `smartctl.exe -a /dev/pd[0-255]` for \\.\PhysicalDrive[0-255] ("name" in hdd info struct)
            // `smartctl.exe --scan` to scan for HDDs. the output includes type and location
            string devAddr = addr.ToLower().Replace("\\\\.\\physicaldrive", "/dev/pd");
            // Console.WriteLine(devAddr);
            string result = string.Empty;

            int exitCode = executeTask("smartctl.exe", "-a " + devAddr, true, out result);
            // Console.WriteLine(exitCode);
            // Console.WriteLine(result);

            return result;
        }

        private static int executeTask(string executable, string argument, bool hideConsole, out string output)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = executable;
            proc.StartInfo.Arguments = argument;

            if (hideConsole)
            {
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;
            }

            // set to capture console output
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;

            // hookup the eventhandlers to capture the data that is received
            var sb = new StringBuilder();
            proc.OutputDataReceived += (sender, args) => sb.AppendLine(args.Data);
            proc.ErrorDataReceived += (sender, args) => sb.AppendLine(args.Data);

            proc.Start();

            // start capturing console output
            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();

            proc.WaitForExit();

            output = sb.ToString().Trim();

            return proc.ExitCode;
        }

        #endregion

        #region getVideo

        // this returns both available and unavailable GPUs
        public static List<string> getVideo_adapter()
        {
            return lookupValue_all("Win32_VideoController", "Name");
        }
        // getting monitor real name (like in control panel) is problematic
        // list of test system:
        // 1 - PC-276x: Win10 x64 EN
        // 2 - NAV 160.84: Win10 x64 EN
        // 3 - 160.28: Win7 x64 EN
        // 4 - 160.91: Win7 x86 EN
        // getVideo_monitor1 gives the wanted result in (1) and (2), crashes in (3), and hangs on (4)
        public static void getVideo_monitor1()
        {
            foreach (string s in screenInterrogatory.GetAllMonitorsFriendlyNames())
            {
                Console.WriteLine(s);
            }
        }
        // getVideo_monitor2 doesn't work in (1), only returning //DisplayX etc
        public static void getVideo_monitor2()
        {
            systemInfo.getVideo_monitor4();
        }
        // getVideo_monitor3 doesn't work in (1), only return Generic PnP Monitor
        public static void getVideo_monitor3()
        {
            foreach (var display in WindowsDisplayAPI.Display.GetDisplays())
            {
                Console.WriteLine(display.DeviceName);
            }
        }
        // getVideo_monitor4 works in (1) and (2), crashes in (3), and simply hangs on (4)
        public static void getVideo_monitor4()
        {
            foreach (var target in WindowsDisplayAPI.DisplayConfig.PathDisplayTarget.GetDisplayTargets())
            {
                Console.WriteLine(target.FriendlyName);
            }
        }

        #endregion

        #region getNetwork

        // get network card, ip address
        // return Name ("Ethernet 2"), Description ("Realtek PCIe GbE Family Controller"), Physical Address, IPv4 Addresses with Subnet Mask, Default Gateways, DNS Servers
        public struct struct_ipAddr
        {
            public string ipAddr;
            public string netMask;
        }
        public struct sruct_networkInterfaceInfo
        {
            public string name;
            public string description;
            public string MAC;
            public bool isUp;
            public List<struct_ipAddr> ipAddresses;
            public List<string> gateways;
            public List<string> dnsServers;
            public long speed;
        }
        private static bool isValidIpAdressV4(string ip)
        {
            // check valid form n.n.n.n
            var match = Regex.Match(ip, "^(\\d+)\\.(\\d+)\\.(\\d+)\\.(\\d+)$");
            if (match.Success && match.Value.Length == ip.Length)
            {
                // check 4 numbers: 0 <= n <= 255
                for (int i = 1; i <= 4; i++)
                {
                    string v = match.Groups[i].Value;
                    int n = int.Parse(v);
                    // Console.WriteLine(n);
                    if (n > 255)
                    {
                        return false;
                    }
                }
                return true;
            }

            return false;
        }
        public static List<sruct_networkInterfaceInfo> getNetwork_interfaces()
        {
            List<sruct_networkInterfaceInfo> result = new List<sruct_networkInterfaceInfo>();

            NetworkInterface[] fNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in fNetworkInterfaces)
            {
                string fRegistryKey = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\" + adapter.Id + "\\Connection";
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(fRegistryKey, false);
                if (rk != null)
                {
                    string fPnpInstanceID = rk.GetValue("PnpInstanceID", "").ToString();
                    int fMediaSubType = Convert.ToInt32(rk.GetValue("MediaSubType", 0));
                    if (fPnpInstanceID.Length > 3 && (fPnpInstanceID.Substring(0, 3) == "PCI" || fPnpInstanceID.Substring(0, 3) == "USB"))
                    {
                        sruct_networkInterfaceInfo thisOne = new sruct_networkInterfaceInfo
                        {
                            name = adapter.Name,
                            description = adapter.Description,
                            MAC = adapter.GetPhysicalAddress().ToString(),
                            isUp = (adapter.OperationalStatus == OperationalStatus.Up),
                            ipAddresses = new List<struct_ipAddr>(),
                            gateways = new List<string>(),
                            dnsServers = new List<string>()
                        };

                        // get address list
                        IPInterfaceProperties properties = adapter.GetIPProperties();
                        foreach (UnicastIPAddressInformation unicast in properties.UnicastAddresses)
                        {
                            struct_ipAddr thisIP;
                            thisIP.ipAddr = unicast.Address.ToString();
                            thisIP.netMask = unicast.IPv4Mask.ToString();
                            if (isValidIpAdressV4(thisIP.ipAddr))
                            {
                                thisOne.ipAddresses.Add(thisIP);
                            }
                        }

                        // get gateway list
                        foreach (GatewayIPAddressInformation gipi in adapter.GetIPProperties().GatewayAddresses)
                        {
                            string gateway = gipi.Address.ToString();
                            if (isValidIpAdressV4(gateway))
                            {
                                thisOne.gateways.Add(gateway);
                            }
                        }

                        // get dns list
                        foreach(IPAddress dnsAddr in properties.DnsAddresses)
                        {
                            string dns = dnsAddr.ToString();
                            if (isValidIpAdressV4(dns))
                            {
                                thisOne.dnsServers.Add(dns);
                            }
                        }

                        thisOne.speed = adapter.Speed;

                        result.Add(thisOne);

                        //Console.WriteLine(thisOne.name);
                        //Console.WriteLine(thisOne.description);
                        //Console.WriteLine(thisOne.MAC);
                        //Console.WriteLine(thisOne.isUp);
                        //foreach (struct_ipAddr ip in thisOne.ipAddresses)
                        //{
                        //    Console.WriteLine(ip.ipAddr);
                        //    Console.WriteLine(ip.netMask);
                        //}
                        //foreach (string ip in thisOne.gateways)
                        //{
                        //    Console.WriteLine(ip);
                        //}
                        //foreach (string ip in thisOne.dnsServers)
                        //{
                        //    Console.WriteLine(ip);
                        //}
                        //Console.WriteLine(thisOne.speed);
                    }
                }
            }

            return result;
        }

        #endregion

        #region getSystem

        // the same as System Manufacturer in dxdiag
        public static string getSystem_manufacturer()
        {
            return lookupValue_first("Win32_ComputerSystem", "Manufacturer");
        }

        // the same as System Model in dxdiag
        public static string getSystem_model()
        {
            return lookupValue_first("Win32_ComputerSystem", "Model");
        }

        // mimic Operating System in dxdiag
        public static string getSystem_OS()
        {
            string result = lookupValue_first("Win32_OperatingSystem", "Caption").Replace("Microsoft ", "");
            result += " " + lookupValue_first("Win32_OperatingSystem", "OSArchitecture");
            // string osVersion = String.Format("{0}.{0}", Environment.OSVersion.Version.Major, Environment.OSVersion.Version.Minor);
            string osVersion = lookupValue_first("Win32_OperatingSystem", "Version");
            result += " (" + osVersion + ")";
            return result;
        }

        public static string getSystem_language()
        {
            CultureInfo ci = CultureInfo.CurrentUICulture;

            //Console.WriteLine("Default Language Info:");
            //Console.WriteLine("* Name: {0}", ci.Name);
            //Console.WriteLine("* Display Name: {0}", ci.DisplayName);
            //Console.WriteLine("* English Name: {0}", ci.EnglishName);
            //Console.WriteLine("* 2-letter ISO Name: {0}", ci.TwoLetterISOLanguageName);
            //Console.WriteLine("* 3-letter ISO Name: {0}", ci.ThreeLetterISOLanguageName);
            //Console.WriteLine("* 3-letter Win32 API Name: {0}", ci.ThreeLetterWindowsLanguageName);

            return ci.Name;

        }

        public static string getSystem_name()
        {
            return lookupValue_first("Win32_ComputerSystem", "Name");
        }

        public static string getSystem_domain()
        {
            return lookupValue_first("Win32_ComputerSystem", "Domain");
        }

        public static string getSystem_currentUsername()
        {
            return lookupValue_first("Win32_ComputerSystem", "UserName");
        }

        // not working, no account found
        // TODO: fix this
        public static List<string[]> getSystem_listUserAccounts()
        {
            List<string[]> result1 = lookupValue_all("Win32_Account", new string[] { "Name", "Domain", "SID", "LocalAccount", "AccountType", "Disabled" });
            List<string[]> result2 = new List<string[]>();

            Console.WriteLine(result1.Count);

            // filter to get only account type 512
            foreach (string[] acc in result1)
            {
                if (acc[4] == "512")
                {
                    result2.Add(acc);
                }
            }

            return result2;
        }

        // note that it won't work with network share drive
        // https://stackoverflow.com/questions/1393711/get-free-disk-space
        private static long getTotalFreeSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                {
                    return drive.TotalFreeSpace;
                }
            }
            return -1;
        }
        private static long getTotalSpace(string driveName)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady && drive.Name == driveName)
                {
                    return drive.TotalSize;
                }
            }
            return -1;
        }

        // get system drive total size and free space
        public static string getSystem_systemDrive()
        {
            string sysDrive = Path.GetPathRoot(Environment.SystemDirectory);
            //Console.WriteLine(sysDrive);
            return sysDrive;
        }
        public static long getSystem_systemDriveFreeSpace()
        {
            return getTotalFreeSpace(getSystem_systemDrive());
        }
        public static long getSystem_systemDriveTotalSpace()
        {
            return getTotalSpace(getSystem_systemDrive());
        }

        #endregion

        #region getProgram

        // get software list
        private static string varToString(object v) => v != null ? v.ToString() : string.Empty;
        private static long varToLong(object v)
        {
            return v != null ? long.Parse(v.ToString()) : 0;
        }
        public static List<string[]> getProgram_list()
        {
            List<string[]> result = new List<string[]>();

            string uninstallKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey rk = Registry.LocalMachine.OpenSubKey(uninstallKey))
            {
                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        // note that these value might be not available, i.e null is returned
                        var displayName = sk.GetValue("DisplayName");
                        var publisher = sk.GetValue("Publisher");
                        var installDate = sk.GetValue("InstallDate");
                        var size = sk.GetValue("EstimatedSize");
                        var version = sk.GetValue("DisplayVersion");

                        // ignore nameless programs
                        if (displayName == null)
                            continue;

                        string[] thisOne = new string[6];

                        thisOne[0] = skName;
                        thisOne[1] = varToString(displayName);
                        thisOne[2] = varToString(publisher);
                        thisOne[3] = varToString(installDate);
                        thisOne[4] = misc.byteToHumanSize(varToLong(size));
                        thisOne[5] = varToString(version);

                        result.Add(thisOne);

                        //if (! displayName.ToString().StartsWith("7-"))
                        //    continue;

                        //Console.WriteLine(skName);
                        //Console.WriteLine(displayName);
                        //Console.WriteLine(publisher);
                        //Console.WriteLine(installDate);
                        //if (size != null)
                        //{
                        //    long sizeb = long.Parse(size.ToString()) * 1024;
                        //    Console.WriteLine(misc.byteToHumanSize(sizeb, 2));
                        //} else
                        //{
                        //    Console.WriteLine("Size unavailable");
                        //}
                        //Console.WriteLine(version);

                        //misc.printStringArray(thisOne);
                        //Console.WriteLine("==============================");
                    }
                }
            }

            return result;
        }

        public static void getProgram_analyze()
        {
            // check for anti-virus apps: Avast, MSE, Windows Defender (win10), McAfee, etc.
            // check for office suite: MS Word/Excel/Powerpoint/Visio/Outlook/others, Lotus Note, OpenOffice, LibreOffice, WPS Office
            // check for pdf apps: Adobe Reader (DC), Foxit PDF, VeryPDF, PDFill, SumatraPDF, Adolix
            // check for archivers: 7-zip, WinRAR
            // check for IME: QQ, Sougou, Unikey, GooglePinyin
            // check for IM apps: Skype, WeChat, Line, Zalo, Microsoft Teams, webex
            // check for browsers: Google Chrome, Firefox, Coccoc, IE, Edge
            // check for ERP apps: LeanERP, Sangely, legacy ERP, Workflow ERP GP
            // check for vendor apps: NGC, eFORMz for UA, U60 Adidas, TradeCard, PackOne
            // check for camera apps: Witness Pro, FreeView, Camera Stream Controller, DAO_install, IP Wizard II, Lilin*, NVR-Client, Onvif, OSDTool, Vivotek Installation Wizard 2
            // check for remote apps: UltraVNC, TeamViewer, AnyDesk, UltraViewer, Radmin
            // check for some known free apps: CCleaner, System Ninjar, FormatFactory, CutePDF, FSCapture, GIMP, HandBrake, K-Lite, KMSpico, Notepad++, Oracle VM, RapidCRC, Unlocker, VLC, PenyuUSB, IObit Uninstaller, Advanced SystemCare, Any Video*, Free Video*, Dropbox, IrfanView, QuickTime, Windows Movie Maker, winmail, Lingoes, StarDict, Youdao, Honeyview
            // check for some known commercial apps: Adobe PTS, Paragon, Easeus, Aomei, MiniTool, ZKTeco, AutoCAD, CorelDraw, Proshow
            // check for specialized apps: HTKK, QTTNCN, KetoanCDcoso, ECUS5VNACCS, ET-bag, SinaJet Plot, GuardScan Monitor, GuardScan Monitor, PSV-EAP, TS24, Ucancam, Ncstudio, Wintopo Pro, Scan2CAD, PGM, Tajima, Wilcom 9
            // check for some known dev apps: Visual Studio, SQL Server (see https://stackoverflow.com/questions/2381055/check-if-sql-server-any-version-is-installed), Sublime Text, Git, XAMPP

            // some known apps but uncommon, so put to others: Process Hacker, Xerox, Everything
            // list to ignore (driver, frameworks, updates, redist): Adobe Flash Player, Java*, Cisco*, Apple*, Samsung*, Lenovo*, Huawei*, Sony*, Panasonic*, Toshiba*, Active Directory*, CodeMeter*, *Printer*, *Driver*, Update*, Gnu*, Microsoft .NET*, Microsoft Help*, Microsoft ODBC*, Microsoft SQL*, Microsoft System*, Microsoft Visual*, Microsoft*SDK*, MSDN*, 
        }

        #endregion
    }
}
