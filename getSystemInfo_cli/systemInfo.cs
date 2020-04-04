using System;
using System.Collections.Generic;
using System.Text;
using System.Management;
using System.Globalization;
using System.IO;
using System.Net.NetworkInformation;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Net;
using System.Diagnostics;

namespace getSystemInfo_cli
{
    class systemInfo
    {
        #region Setup Context Local/Remote
        public static ManagementScope scope;
        public static RegistryKey registryLocalMachine;
        public static RegistryKey registryCurrentUser;
        public static RegistryKey registryUsers;
        public static bool contextIsRemote;

        static systemInfo()
        {
            setupContextLocal();
        }

        public static int setupContextLocal()
        {
            contextIsRemote = false;

            registryLocalMachine = Registry.LocalMachine;
            registryCurrentUser = Registry.CurrentUser;
            registryUsers = Registry.Users;

            scope = new ManagementScope(@"\\.\root\cimv2");
            try {
                scope.Connect();
            }
            catch (Exception ex) when (
                       ex is IOException // hostname not found or unreachable
                    || ex is ArgumentNullException // null hostname
                    || ex is System.Security.SecurityException // user doesn't have permissions for this action
                    || ex is UnauthorizedAccessException // user doesn't have the necessary registry rights
                )
            {
                return -1;
            }
            return 0;
        }

        // return -1 when wmi scope connection fails
        // return -2 when remote registry connection fails
        // return -3 when both fails
        // return 0 when all right
        public static int setupContextRemote(string hostname, bool useCurrentWindowsLogin, string username="", string password="")
        {
            contextIsRemote = true;

            ConnectionOptions options = new ConnectionOptions();
            options.Impersonation = System.Management.ImpersonationLevel.Impersonate;

            if (!useCurrentWindowsLogin)
            {
                options.Username = username;
                options.Password = password;
            }

            scope = new ManagementScope(@"\\" + hostname + @"\root\cimv2");
            scope.Options = options;

            int re = 0;

            try
            {
                scope.Connect();
            }
            catch (Exception ex) when (
                       ex is IOException // hostname not found or unreachable
                    || ex is ArgumentNullException // null hostname
                    || ex is System.Security.SecurityException // user doesn't have permissions for this action
                    || ex is UnauthorizedAccessException // user doesn't have the necessary rights
                )
            {
                re = re - 1;
            }

            try
            {
                // todo: authenticate with username and password
                // only works with current windows login for now
                registryLocalMachine = RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, hostname);
                registryCurrentUser = RegistryKey.OpenRemoteBaseKey(RegistryHive.CurrentUser, hostname);
                registryUsers = RegistryKey.OpenRemoteBaseKey(RegistryHive.Users, hostname);
            }
            catch (Exception ex) when (
                       ex is IOException // hostname not found or unreachable
                    || ex is ArgumentNullException // null hostname
                    || ex is System.Security.SecurityException // user doesn't have permissions for this action
                    || ex is UnauthorizedAccessException // user doesn't have the necessary registry rights
                )
            {
                re = re - 2;
            }

            return re;
        }
        #endregion

        #region lookupValue

        private static string lookupValue_first(string wantedClass, string wantedValue)
        {
            string result = String.Empty;
            ManagementClass mc = new ManagementClass(wantedClass);
            mc.Scope = scope;
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                try
                {
                    result = mo.Properties[wantedValue].Value.ToString();
                    break;
                }
                catch (Exception)
                { }
            }
            return result;
        }

        private static List<string> lookupValue_all(string wantedClass, string wantedValue)
        {
            List<string> result = new List<string>();
            ManagementClass mc = new ManagementClass(wantedClass);
            mc.Scope = scope;
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                try
                {
                    result.Add(mo.Properties[wantedValue].Value.ToString());
                }
                catch (Exception)
                { }
            }
            return result;
        }

        private static List<string[]> lookupValue_all(string wantedClass, string[] wantedValues)
        {
            List<string[]> results = new List<string[]>();

            ManagementClass mc = new ManagementClass(wantedClass);
            mc.Scope = scope;
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                string[] oneResult = new string[wantedValues.Length];
                for (int i = 0; i < wantedValues.Length; i++)
                {
                    try
                    {
                        oneResult[i] = mo.Properties[wantedValues[i]].Value.ToString();
                    }
                    catch (Exception)
                    {
                        oneResult[i] = string.Empty;
                    }
                }
                results.Add(oneResult);
            }
            return results;
        }

        private static int lookupValue_count(string wantedClass, string wantedValue)
        {
            int count = 0;
            ManagementClass mc = new ManagementClass(wantedClass);
            mc.Scope = scope;
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo.Properties[wantedValue] != null && mo.Properties[wantedValue].Value != null)
                {
                    count += 1;
                }
            }
            return count;
        }

        public static ManagementObjectCollection runWmiQuery(string queryString)
        {
            ManagementObjectSearcher query = new ManagementObjectSearcher(queryString);
            query.Scope = scope;
            ManagementObjectCollection moc = query.Get();
            return moc;
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

        public static List<string[]> getCPU_all()
        {
            var CPUs = lookupValue_all("win32_processor", new string[] { "Name", "NumberOfCores", "NumberOfLogicalProcessors", "MaxClockSpeed", "SocketDesignation" });

            return CPUs;
        }

        public static int getCPU_usage()
        {
            // see https://stackoverflow.com/questions/48432100/
            var all = lookupValue_all("Win32_PerfFormattedData_Counters_ProcessorInformation", new string[] { "Name", "PercentProcessorTime" });

            foreach (var col in all)
            {
                if (col[0] == "_Total")
                {
                    return Convert.ToInt32(col[1]);
                }
            }

            return -1; // get again if it arrives here
        }

        // get <n> times, with <delay> miliseconds delay between each
        // return the average
        public static double getCPU_usageAvg(int n, int delay)
        {
            int[] usage = new int[n];

            for (int i = 0; i < n; i++)
            {
                usage[i] = getCPU_usage();
                System.Threading.Thread.Sleep(delay);
            }

            double sum = 0;
            foreach (int u in usage)
            {
                sum += u;
            }

            return sum / n;
        }

        #endregion

        #region get Memory

        // visible memory size = how much memory the system can use (the same as in Task Manager), usually a bit less than total physical capacity
        public static long getMemory_visibleSize()
        {
            long visibleSizeInKiB = stringToLong(lookupValue_first("Win32_OperatingSystem", "TotalVisibleMemorySize"));
            // TotalVisibleMemorySize is in KiB
            // return in bytes
            return visibleSizeInKiB * 1024;
        }

        // free memory size, the same as in Task Manager
        public static long getMemory_freeSize()
        {
            long freeSizeInKiB = stringToLong(lookupValue_first("Win32_OperatingSystem", "FreePhysicalMemory"));
            // FreePhysicalMemory is in KiB
            // return in bytes
            return freeSizeInKiB * 1024;
        }

        // count number of installed ram sticks
        public static string getMemory_stickCount()
        {
            return lookupValue_count("Win32_PhysicalMemory", "Capacity").ToString();
        }

        // count number of physical ram slot
        public static string getMemory_slotCount()
        {
            return lookupValue_first("Win32_PhysicalMemoryArray", "MemoryDevices");
        }

        // get total physical memory
        public static long getMemory_totalCapacity()
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
        public static List<string[]> getMemory_stickList()
        {
            var RAMs = lookupValue_all("Win32_PhysicalMemory", new string[] { "Manufacturer", "PartNumber", "Capacity", "Speed", "MemoryType" });

            for (int i = 0; i < RAMs.Count; i++)
            {
                RAMs[i][4] = MemoryTypeToText[RAMs[i][4]];
            }

            return RAMs;
        }

        // might be inaccurate
        // sources:
        // https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-physicalmemory
        // https://stackoverflow.com/questions/14227171/how-to-get-memory-information-ram-type-e-g-ddr-ddr2-ddr3-with-wmi-c
        // https://gist.github.com/ayancey/b3399177f9072db88d8a58559f1e3503
        public static readonly Dictionary<string, string> MemoryTypeToText = new Dictionary<string, string>()
        {
            { "0", "Unknown" },
            { "1", "Other" },
            { "2", "DRAM" },
            { "3", "Synchronous DRAM" },
            { "4", "Cache DRAM" },
            { "5", "EDO" },
            { "6", "EDRAM" },
            { "7", "VRAM" },
            { "8", "SRAM" },
            { "9", "RAM" },
            { "10", "ROM" },
            { "11", "Flash" },
            { "12", "EEPROM" },
            { "13", "FEPROM" },
            { "14", "EPROM" },
            { "15", "CDRAM" },
            { "16", "3DRAM" },
            { "17", "SDRAM" },
            { "18", "SGRAM" },
            { "19", "RDRAM" },
            { "20", "DDR" },
            { "21", "DDR2" },
            { "22", "DDR2 FB-DIMM" },
            { "23", "" },
            { "24", "DDR3" },
            { "25", "FBD2" },
            { "26", "DDR4" },
            { "27", "LPDDR" },
            { "28", "LPDDR2" },
            { "29", "LPDDR3" },
            { "30", "LPDDR4" },
        };

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

        public static string getMobo_serialNumber()
        {
            return lookupValue_first("Win32_BIOS", "SerialNumber");
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
                    smart = contextIsRemote ? "Remote S.M.A.R.T not yet implemented." : getHddSmartInfo(hdd[0])
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
            string error = string.Empty;

            int exitCode = executeTask("smartctl.exe", "-a " + devAddr, true, out result, out error);
            // Console.WriteLine(exitCode);
            // Console.WriteLine(result);

            return result ?? error;
        }

        public static int executeTask(string executable, string argument, bool hideConsole, out string output, out string error)
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

            try
            {
                proc.Start();

                // start capturing console output
                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();

                proc.WaitForExit();

                output = sb.ToString().Trim();
                error = null;

                return proc.ExitCode;
            }
            catch (System.ComponentModel.Win32Exception e) // mostly for file not found
            {
                output = null;
                error = e.ToString();
                return -1;
            }
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
        // 1 - PC-276x: Win10 x64 EN (direct, display on) (in cpl: "VK191")
        // 2 - NAV 160.84: Win10 x64 EN (VNC, display on) (in cpl: "Dell S2340L" and "Generic PnP Monitor"
        // 3a - 160.28: Win7 x64 EN (RDP session) (in cpl: "Generic PnP Monitor")
        // 3b - 160.28: Win7 x64 EN (VNC, display on) (in cpl: "Dell E176FP")
        // 4a - 160.91: Win7 x86 EN (VNC, display off) (in cpl: "Generic Non-PnP Monitor")
        // 4b - 160.91: Win7 x86 EN (VNC, display on) (in cpl: "Generic Non-PnP Monitor")
        // 5 - 160.27: Win7 x64 TW (VNC, KVM) (in cpl: "KVM Monitor")
        // 6 - 160.87: Win7 x86 EN (VNC, no display) (in cpl: N/A)

        // result matrix

        // <cp> means the same as in control panel
        // <generic1> means "Generic PnP Monitor"
        // <generic2> means "Generic Non-PnP Monitor"

        // Methods \ System     (1)         (2)         (3a)        (3b)        (4a)        (4b)        (5)         (6)
        // getVideo_monitor1    <cp>        <cp>        <except>    <cp>        <empty>     <empty>     <cp>        <empty>
        // getVideo_monitor2    <generic>   <generic>   <empty>     <generic>   <generic2>  <generic2>  <generic>   <empty>
        // getVideo_monitor3    <generic>   <generic>   <empty>     <generic>   <generic2>  <generic2>  <generic>   <empty>
        // getVideo_monitor4    <cp>        <cp>        <except>    <cp>        <empty>     <empty>     <cp>        <empty>
        // getVideo_monitor5    <cp>        <cp>        <except>    <cp>        <empty>     <empty>     <cp>        <empty>

        // three exceptions has the same error: System.ComponentModel.Win32Exception (0x80004005): The paramenter is incorrect

        // TODO: find some way to get their frendly names via registry or wmi

        // by G.Y, https://stackoverflow.com/a/28257839
        public static void getVideo_monitor1()
        {
            foreach (string s in screenInterrogatory.GetAllMonitorsFriendlyNames())
            {
                Console.WriteLine(s);
            }
        }

        //// by JamesStuddart, https://stackoverflow.com/q/4958683
        //// getVideo_monitor2 doesn't work anywhere, only returning \\.\DISPLAY1 and \\.\DISPLAY1\Monitor0
        //public static void getVideo_monitor2()
        //{
        //    monitorInfo.search();
        //}

        //// using WindowsDisplayAPI package, by Soroush Falahati https://stackoverflow.com/a/44046839
        //// supporting .NET 2.0 (?) and 4.5
        //// getVideo_monitor3 only returns Generic PnP Monitor and Generic Non-PnP Monitor
        //public static void getVideo_monitor3()
        //{
        //    foreach (var display in WindowsDisplayAPI.Display.GetDisplays())
        //    {
        //        Console.WriteLine(display.DeviceName);
        //    }
        //}
        //// using the new API, requires at least Windows Vista
        //public static void getVideo_monitor4()
        //{
        //    foreach (var target in WindowsDisplayAPI.DisplayConfig.PathDisplayTarget.GetDisplayTargets())
        //    {
        //        Console.WriteLine(target.FriendlyName);
        //    }
        //}

        //// by David Heffernan https://stackoverflow.com/a/26406082
        //public static void getVideo_monitor5()
        //{
        //    monitorInfo_david.search();
        //}


        public static List<string> getVideo_monitor_wrapper()
        {
            List<string> result = new List<string>();

            // first try GetAllMonitorsFriendlyNames to get monitors' real name
            try
            {
                foreach (string s in screenInterrogatory.GetAllMonitorsFriendlyNames())
                {
                    result.Add(s);
                    // Console.WriteLine(s);
                }
            }
            catch (System.ComponentModel.Win32Exception)
            {
                // GetAllMonitorsFriendlyNames got this exception when in RDP session
            }

            // if it can't get anything, query for generic names
            if (result.Count == 0)
            {
                foreach (string s in getVideo_monitor0())
                {
                    result.Add(s);
                    // Console.WriteLine(s);
                }
            }

            return result;
        }

        // query Win32_DesktopMonitor to get monitors
        // ignore ones without PNPDeviceID, as they're not real monitor and thus unwanted
        // results are usually only Generic *** Monitor
        public static List<string> getVideo_monitor0()
        {
            List<string> result = new List<string>();
            var genericMons = lookupValue_all("Win32_DesktopMonitor", new string[] { "Name", "PNPDeviceID" });
            foreach (var genericMon in genericMons)
            {
                // misc.printStringArray(genericMon);
                if (genericMon[1].Length > 0)
                {
                    result.Add(genericMon[0]);
                }
            }
            return result;
        }

        #endregion

        #region getNetwork
        // TODO: update getNetwork_interfaces to support remote context
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
            public string PnpInstanceID;
            public string MAC;
            public bool isUp;
            public bool isDhcpEnabled;
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
        public static List<sruct_networkInterfaceInfo> getNetwork_interfaces(bool dontFilterInstanceID = false)
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

                    // filter real network cards. the device instance path should have "PCI" or "USB" at the beginning or sometimes in the middle (rare)
                    // virtual interface' device instance path start with "ROOT"
                    // some virtual devices should be accepted:
                    // - Broadcom Team (BASP Virtual Adapter): ROOT\BRCM_BLFM\<number>
                    // - Microsoft Network Adapter Multiplexor Driver: COMPOSITEBUS\MS_IMPLAT_MP\{uuid}
                    if (fPnpInstanceID.Length > 0 && (dontFilterInstanceID || fPnpInstanceID.StartsWith("PCI") || fPnpInstanceID.StartsWith("USB") || fPnpInstanceID.Contains("PCI") || fPnpInstanceID.StartsWith("COMPOSITEBUS") || fPnpInstanceID.StartsWith("ROOT\\BRCM_BLFM\\")))
                    {
                        bool isDhcpEnabled;
                        try
                        {
                            isDhcpEnabled = adapter.GetIPProperties().GetIPv4Properties().IsDhcpEnabled;
                        }
                        catch (NetworkInformationException)
                        {
                            isDhcpEnabled = false;
                        }
                        sruct_networkInterfaceInfo thisOne = new sruct_networkInterfaceInfo
                        {
                            name = adapter.Name,
                            description = adapter.Description,
                            PnpInstanceID = fPnpInstanceID,
                            MAC = adapter.GetPhysicalAddress().ToString(),
                            isUp = (adapter.OperationalStatus == OperationalStatus.Up),
                            isDhcpEnabled = isDhcpEnabled,
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

                            // in .NET 2.0 & 3.5, IPv4Mask is null if the network interface is down
                            // this was fixed in .NET 4.0 and later; IPv4Mask will have the correct value
                            // the following null check can prevent crashing in unfortunate occasions,
                            // but please use .NET 4.0 or later
                            thisIP.netMask = (unicast.IPv4Mask != null) ? unicast.IPv4Mask.ToString(): String.Empty;
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

        // get %SystemRoot%
        public static string getSystem_rootPath()
        {
            return lookupValue_first("Win32_OperatingSystem", "WindowsDirectory");
        }

        public static string getSystem_language()
        {
            if (!contextIsRemote)
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
            else
            {
                // get it from HKEY_USERS\.DEFAULT\Control Panel\International, value LocaleName
                var sk = registryUsers.OpenSubKey(@".DEFAULT\Control Panel\International");
                return (sk != null) ? varToString(sk.GetValue("LocaleName")) : string.Empty;
            }
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

        public static List<string> getSystem_listLoggonUsers()
        {
            List<string> result = new List<string>();

            var processList = runWmiQuery("Select * from Win32_Process Where Name = \"Explorer.exe\" ");

            foreach (ManagementObject obj in processList)
            {
                string owner = string.Empty;
                string[] argList = new string[] { string.Empty, string.Empty };
                int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", argList));
                if (returnVal == 0)
                {
                    // DOMAIN\user
                    owner = argList[1] + "\\" + argList[0];
                    // Console.WriteLine(owner);
                    result.Add(owner);
                }
            }

            // remove duplicates and return
            return misc.getDistinct_stringList(result);
        }

        public static TimeSpan getSystem_uptime()
        {
            string lastBootUptime = lookupValue_first("Win32_OperatingSystem", "LastBootUpTime");
            DateTime lastBootUp = ManagementDateTimeConverter.ToDateTime(lastBootUptime);
            TimeSpan uptime = DateTime.Now.ToUniversalTime() - lastBootUp.ToUniversalTime();
            return uptime;
        }
        public static string getSystem_uptime_str()
        {
            var uptime = getSystem_uptime();
            return string.Format("{0:dd\\:hh\\:mm\\:ss}", uptime);
        }

        public static DateTime getSystem_time()
        {
            List<string[]> result1 = lookupValue_all("Win32_LocalTime", new string[] { "Year", "Month", "Day", "Hour", "Minute", "Second", "Milliseconds" });

            int year = stringToInt(result1[0][0]);
            int month = stringToInt(result1[0][1]);
            int day = stringToInt(result1[0][2]);
            int hour = stringToInt(result1[0][3]);
            int minute = stringToInt(result1[0][4]);
            int second = stringToInt(result1[0][5]);
            int milisecond = stringToInt(result1[0][6]);

            DateTime sysTime = new DateTime(year, month, day, hour, minute, second, milisecond);
            return sysTime;
        }
        private static int stringToInt(string s)
        {
            if (int.TryParse(s, out int re))
            {
                return re;
            }
            return 0;
        }
        #endregion

        #region get Logical Drives
        public struct driveInfo
        {
            public string name { get; set; }
            public string description { get; set; }
            public int iType { get; set; }
            public string sType { get; set; }
            public string volumeName { get; set; }
            public string fileSystem { get; set; }
            public long size { get; set; }
            public long free { get; set; }
            public string networkPath { get; set; }
            public bool isSystemDrive { get; set; }
        }

        public static readonly Dictionary<int, string> driveTypeToText = new Dictionary<int, string>()
        {
            { 0, "Unknown" },
            { 1, "No Root Directory" },
            { 2, "Removable Disk" },
            { 3, "Local Disk" },
            { 4, "Network Drive" },
            { 5, "Compact Disc" },
            { 6, "RAM Disk" },
        };

        private static long stringToLong(string s)
        {
            if (long.TryParse(s, out long re))
            {
                return re;
            }
            return 0;
        }

        // sometimes (the first time?) it fails to get size and free size (only returns 0), re-run to get the correct value
        public static List<driveInfo> getDrive_allLogicalDrives()
        {
            List<string[]> allDrives = lookupValue_all("Win32_LogicalDisk", new string[] { "Name", "Description", "DriveType", "VolumeName", "FileSystem", "Size", "FreeSpace", "ProviderName" });

            List<driveInfo> re = new List<driveInfo>();
            string systemLetter = getDrive_systemDrive().Replace("\\", string.Empty);
            foreach (var drive in allDrives)
            {
                int driveType = stringToInt(drive[2]);

                driveInfo d = new driveInfo
                {
                    name = drive[0],
                    description = drive[1],
                    iType = driveType,
                    sType = driveTypeToText[driveType],
                    volumeName = drive[3],
                    fileSystem = drive[4],
                    size = stringToLong(drive[5]),
                    free = stringToLong(drive[6]),
                    networkPath = drive[7],
                    isSystemDrive = (drive[0] == systemLetter),
                };

                re.Add(d);
            }

            return re;
        }

        private static long getDriveFreeSpace(string driveName)
        {
            // search for the wanted drive in the list provided by getDrive_allLogicalDrives
            var all = getDrive_allLogicalDrives();
            foreach (var d in all)
            {
                if (d.name == driveName.Replace("\\", string.Empty))
                {
                    return d.free;
                }
            }
            return -1;
        }

        private static long getDriveCapacity(string driveName)
        {
            // search for the wanted drive in the list provided by getDrive_allLogicalDrives
            var all = getDrive_allLogicalDrives();
            foreach (var d in all)
            {
                if (d.name == driveName.Replace("\\", string.Empty))
                {
                    return d.size;
                }
            }
            return -1;
        }

        // get system drive total size and free space
        public static string getDrive_systemDrive()
        {
            string sysDrive = Path.GetPathRoot(getSystem_rootPath());
            return sysDrive;
        }

        public static long getDrive_systemDriveFreeSpace()
        {
            return getDriveFreeSpace(getDrive_systemDrive());
        }

        public static long getDrive_systemDriveCapacity()
        {
            return getDriveCapacity(getDrive_systemDrive());
        }
        #endregion

        #region getProgram

        // get software list
        private static string varToString(object v) => v != null ? v.ToString().Trim() : string.Empty;
        private static long varToLong(object v)
        {
            if (v == null) return 0;

            bool valid = long.TryParse(v.ToString(), out long re);
            return valid ? re : 0;
        }
        public static List<string[]> getProgram_list()
        {
            List<string[]> result = new List<string[]>();

            List<RegistryKey> rks = new List<RegistryKey>();

            string uninstallKeyUser = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            string uninstallKey32 = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            string uninstallKey64 = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";

            rks.Add(registryCurrentUser.OpenSubKey(uninstallKeyUser));
            rks.Add(registryLocalMachine.OpenSubKey(uninstallKey32));
            rks.Add(registryLocalMachine.OpenSubKey(uninstallKey64));

            foreach (RegistryKey rk in rks)
            {
                if (rk == null)
                {
                    continue;
                }

                foreach (string skName in rk.GetSubKeyNames())
                {
                    using (RegistryKey sk = rk.OpenSubKey(skName))
                    {
                        // note that these value might be not available, i.e null is returned
                        var displayName = varToString(sk.GetValue("DisplayName"));
                        var publisher = varToString(sk.GetValue("Publisher"));
                        var installDate = varToString(sk.GetValue("InstallDate"));
                        var size = varToLong(sk.GetValue("EstimatedSize"));
                        var version = varToString(sk.GetValue("DisplayVersion"));
                        var uninstallString = varToString(sk.GetValue("UninstallString"));
                        var releaseType = varToString(sk.GetValue("ReleaseType"));
                        var systemComponent = varToString(sk.GetValue("SystemComponent"));
                        var systemComponentLong = varToLong(sk.GetValue("SystemComponent"));
                        var parentName = varToString(sk.GetValue("ParentDisplayName"));

                        // ignore nameless programs and system components to match Control Panel's behaviour
                        // see https://stackoverflow.com/questions/15524161/
                        if (string.IsNullOrEmpty(displayName)
                            || string.IsNullOrEmpty(uninstallString)
                            || !string.IsNullOrEmpty(releaseType)
                            || !string.IsNullOrEmpty(parentName)
                            || (!string.IsNullOrEmpty(systemComponent) && systemComponent != 0.ToString())
                            )
                            continue;

                        string[] thisOne = new string[7];

                        thisOne[0] = skName;
                        thisOne[1] = displayName;
                        thisOne[2] = publisher;
                        thisOne[3] = installDate;
                        thisOne[4] = misc.byteToHumanSize(size);
                        thisOne[5] = version;
                        thisOne[6] = uninstallString;

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

                rk.Close();
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
