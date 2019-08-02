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

namespace getSystemInfo_cli
{
    class systemInfo
    {
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
        
        public static string getMobo_manufacturer()
        {
            return lookupValue_first("Win32_BaseBoard", "Manufacturer");
        }

        public static string getMobo_model()
        {
            return lookupValue_first("Win32_BaseBoard", "Product");
        }

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

        // TODO: get VGA, monitor
        public static void getVideo_monitor1()
        {
            foreach (var display in WindowsDisplayAPI.Display.GetDisplays())
            {
                Console.WriteLine(display.DeviceName);
            }
        }
        public static void getVideo_monitor2()
        {
            foreach (var target in WindowsDisplayAPI.DisplayConfig.PathDisplayTarget.GetDisplayTargets())
            {
                Console.WriteLine(target.FriendlyName);
            }
        }

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


        // TODO: get software list
        // MS Office, Visio, AV, others
    }
}
