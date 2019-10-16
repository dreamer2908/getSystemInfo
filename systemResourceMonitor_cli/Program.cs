using getSystemInfo_cli;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace systemResourceMonitor_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();

            //PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            //float t = cpuCounter.NextValue(); // the first reading is always 0

            //for (int i = 0; i < 1; i++)
            //{
            //    System.Threading.Thread.Sleep(1000); // wait a second to get a valid reading
            //    t = cpuCounter.NextValue(); // this time it will be correct
            //    Console.WriteLine(t);
            //}

            sb.AppendLine("Current Time: " + getNowString());
            
            sb.AppendLine(string.Format("CPU Name: {0}", systemInfo.getCPU_name()));
            sb.AppendLine(string.Format("CPU Core Count: {0}", systemInfo.getCPU_coreCount()));
            sb.AppendLine(string.Format("CPU Thread Count: {0}", systemInfo.getCPU_threadCount()));
            sb.AppendLine(string.Format("CPU Clock Speed: {0}", systemInfo.getCPU_clockSpeed()));

            PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            float t = cpuCounter.NextValue(); // the first reading is always 0
            System.Threading.Thread.Sleep(1000); // wait a second to get a valid reading
            t = cpuCounter.NextValue(); // this time it will be correct

            sb.AppendLine(string.Format("CPU Usage: {0:0.00}%", t));


            var comInfo = new Microsoft.VisualBasic.Devices.ComputerInfo();
            long total = (long)comInfo.TotalPhysicalMemory;
            long available = (long)comInfo.AvailablePhysicalMemory;
            long used = total - available;
            double usedpercent = 100.0 * used / total;
            double availablepercent = 100.0 * available / total;
            sb.AppendLine(string.Format("RAM Total Capacity: {0}", misc.byteToHumanSize(total)));
            sb.AppendLine(string.Format("RAM Used Size: {0} ({1:0.00}%)", misc.byteToHumanSize(used), usedpercent));
            sb.AppendLine(string.Format("RAM Free Size: {0} ({1:0.00}%)", misc.byteToHumanSize(available), availablepercent));

            sb.AppendLine(string.Format("RAM Slot Count: {0}", systemInfo.getRAM_slotCount()));
            sb.AppendLine(string.Format("RAM Stick Count: {0}", systemInfo.getRAM_stickCount()));

            List<string[]> RAMs = systemInfo.getRAM_stickList();

            for (int i = 0; i < RAMs.Count; i++)
            {
                sb.AppendLine(string.Format("RAM stick #{0}:", i + 1));
                sb.AppendLine(string.Format("    Manufacturer: {0}", RAMs[i][0]));
                sb.AppendLine(string.Format("    PartNumber: {0}", RAMs[i][1]));
                sb.AppendLine(string.Format("    Capacity: {0}", misc.byteToHumanSize(long.Parse(RAMs[i][2]))));
                sb.AppendLine(string.Format("    Bus Speed: {0} MHz", RAMs[i][3]));
            }

            sb.AppendLine(string.Format("Mainboard Manufacturer: {0}", systemInfo.getMobo_manufacturer()));
            sb.AppendLine(string.Format("Mainboard Model: {0}", systemInfo.getMobo_model()));

            sb.AppendLine(string.Format("System Manufacturer: {0}", systemInfo.getSystem_manufacturer()));
            sb.AppendLine(string.Format("System Model: {0}", systemInfo.getSystem_model()));

            // Console.WriteLine(misc.byteToHumanSize(systemInfo.getSystem_systemDriveFreeSpace()));
            // Console.WriteLine(misc.byteToHumanSize(systemInfo.getSystem_systemDriveTotalSpace()));

            sb.AppendLine(string.Format("System OS: {0}", systemInfo.getSystem_OS()));
            sb.AppendLine(string.Format("System Language: {0}", systemInfo.getSystem_language()));
            sb.AppendLine(string.Format("Computer Name: {0}", systemInfo.getSystem_name()));
            sb.AppendLine(string.Format("Domain Name: {0}", systemInfo.getSystem_domain()));
            sb.AppendLine(string.Format("Current Username: {0}", systemInfo.getSystem_currentUsername()));

            // get logical drives

            DriveInfo[] allDrives = DriveInfo.GetDrives();

            sb.AppendLine(string.Format("Logical Drive Count: {0}", allDrives.Length));

            foreach (DriveInfo d in allDrives)
            {
                sb.AppendLine(string.Format("    Drive {0}", d.Name));
                sb.AppendLine(string.Format("        Drive type: {0}", d.DriveType));
                if (d.IsReady == true)
                {
                    sb.AppendLine(string.Format("        Volume label: {0}", d.VolumeLabel));
                    sb.AppendLine(string.Format("        File system: {0}", d.DriveFormat));
                    sb.AppendLine(string.Format("        Available space to current user: {0}", misc.byteToHumanSize(d.AvailableFreeSpace)));
                    sb.AppendLine(string.Format("        Total available space: {0}", misc.byteToHumanSize(d.TotalFreeSpace)));
                    sb.AppendLine(string.Format("        Total size of drive: {0} ", misc.byteToHumanSize(d.TotalSize)));
                }
            }

            var HDDs = systemInfo.getHDD_list();
            sb.AppendLine(string.Format("Physical HDD Count: {0}", HDDs.Count));

            for (int i = 0; i < HDDs.Count; i++)
            {
                sb.AppendLine(string.Format("HDD #{0}:", i + 1));

                var HDD = HDDs[i];

                sb.AppendLine("    Address: " + HDD.addr);
                sb.AppendLine("    Model: " + HDD.model);
                sb.AppendLine("    Capacity: " + misc.byteToHumanSize(HDD.size));
                sb.AppendLine("    S.M.A.R.T Info:\n" + padSpaceLines(HDD.smart, "        ") + "\n");
            }

            File.WriteAllText(string.Format("system_info_{0}.txt", getNowStringForFilename()), sb.ToString());

            Console.WriteLine("Done.");
            // Console.ReadLine();
        }

        static string padSpaceLines(string text, string pad)
        {
            var sb = new StringBuilder();

            var input = text.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

            foreach (var line in input)
            {
                sb.AppendLine(pad + line);
            }

            return sb.ToString();
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
    }
}
