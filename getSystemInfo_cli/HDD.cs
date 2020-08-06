/* 
  Some parts are from Llewellyn Kruger https://web.archive.org/web/20200118172745/http://www.know24.net/blog/C+WMI+HDD+SMART+Information.aspx
  Some are from derek wilson http://derekwilson.net/blog/2017/08/26/smart
  Some are from Quy Nguyen (this repo)
*/

/*
Copyright (c) 2013, Llewellyn Kruger
All rights reserved.
Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS “AS IS” AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE. 
*/

using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace getSystemInfo_cli
{
    public class HDD
    {
        public string Id { get; set; }
        public int Index { get; set; }
        public bool IsOK { get; set; }
        public string Health { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Serial { get; set; }
        public string smart { get; set; }
        public Dictionary<int, Smart> Attributes = new Dictionary<int, Smart>() {
                {0x00, new Smart("Invalid")},
                {0x01, new Smart("Raw read error rate")},
                {0x02, new Smart("Throughput performance")},
                {0x03, new Smart("Spinup time")},
                {0x04, new Smart("Start/Stop count")},
                {0x05, new Smart("Reallocated sector count")},
                {0x06, new Smart("Read channel margin")},
                {0x07, new Smart("Seek error rate")},
                {0x08, new Smart("Seek timer performance")},
                {0x09, new Smart("Power-on hours count")},
                {0x0A, new Smart("Spinup retry count")},
                {0x0B, new Smart("Calibration retry count")},
                {0x0C, new Smart("Power cycle count")},
                {0x0D, new Smart("Soft read error rate")},
                {0x16, new Smart("Current Helium Level")}, // added by QN
                {0xAA, new Smart("Available Reserved Space")}, // added by QN
                {0xAB, new Smart("SSD Program Fail Count")}, // added by QN
                {0xAC, new Smart("SSD Erase Fail Count")}, // added by QN
                {0xAD, new Smart("SSD Wear Leveling Count")}, // added by QN
                {0xAE, new Smart("Unexpected Power Loss Count")}, // added by QN
                {0xAF, new Smart("Power Loss Protection Failure")}, // added by QN
                {0xB0, new Smart("Erase Fail Count")}, // added by QN
                {0xB1, new Smart("Wear Range Delta")}, // added by QN
                {0xB3, new Smart("Used Reserved Block Count Total")}, // added by QN
                {0xB4, new Smart("Unused Reserved Block Count Total")}, // added by QN
                {0xB5, new Smart("Program Fail Count Total")}, // added by QN
                {0xB6, new Smart("Erase Fail Count")}, // added by QN
                {0xB7, new Smart("Runtime Bad Block")}, // added by QN
                {0xB8, new Smart("End-to-End error")},
                {0xB9, new Smart("Head Stability")}, // added by QN
                {0xBA, new Smart("Induced Op-Vibration Detection")}, // added by QN
                {0xBB, new Smart("Reported Uncorrectable Errors")}, // added by QN
                {0xBC, new Smart("Command Timeout")}, // added by QN
                {0xBD, new Smart("High Fly Writes")}, // added by QN
                {0xBE, new Smart("Airflow Temperature")},
                {0xBF, new Smart("G-sense error rate")},
                {0xC0, new Smart("Power-off retract count")},
                {0xC1, new Smart("Load/Unload cycle count")},
                {0xC2, new Smart("HDD temperature")},
                {0xC3, new Smart("Hardware ECC recovered")},
                {0xC4, new Smart("Reallocation event count")},
                {0xC5, new Smart("Current pending sector count")},
                {0xC6, new Smart("Offline scan uncorrectable count")},
                {0xC7, new Smart("UDMA CRC error rate")},
                {0xC8, new Smart("Write error rate")},
                {0xC9, new Smart("Soft read error rate")},
                {0xCA, new Smart("Data Address Mark errors")},
                {0xCB, new Smart("Run out cancel")},
                {0xCC, new Smart("Soft ECC correction")},
                {0xCD, new Smart("Thermal asperity rate (TAR)")},
                {0xCE, new Smart("Flying height")},
                {0xCF, new Smart("Spin high current")},
                {0xD0, new Smart("Spin buzz")},
                {0xD1, new Smart("Offline seek performance")},
                {0xD2, new Smart("Vibration During Write")}, // added by QN
                {0xD3, new Smart("Vibration During Write")}, // added by QN
                {0xD4, new Smart("Shock During Write")}, // added by QN
                {0xDC, new Smart("Disk shift")},
                {0xDD, new Smart("G-sense error rate")},
                {0xDE, new Smart("Loaded hours")},
                {0xDF, new Smart("Load/unload retry count")},
                {0xE0, new Smart("Load friction")},
                {0xE1, new Smart("Load/Unload cycle count")},
                {0xE2, new Smart("Load-in time")},
                {0xE3, new Smart("Torque amplification count")},
                {0xE4, new Smart("Power-off retract count")},
                {0xE6, new Smart("GMR head amplitude")},
                {0xE7, new Smart("Life Left")}, // changed by QN
                {0xE8, new Smart("Endurance Remaining")}, // added by QN
                {0xE9, new Smart("Media Wearout Indicator")}, // added by QN
                // 0xEA and 0xEB can't be decoded for now
                {0xEA, new Smart("Average erase count")}, // added by QN
                {0xEB, new Smart("Good Block Count")}, // added by QN
                {0xF0, new Smart("Head flying hours")},
                {0xF1, new Smart("Total LBAs Written")}, // added by QN
                {0xF2, new Smart("Total LBAs Read")}, // added by QN
                // 0xF3 and 0xF4 can't be combined into F0 and F1 yet
                {0xF3, new Smart("Total LBAs Written Expanded")}, // added by QN
                {0xF4, new Smart("Total LBAs Read Expanded")}, // added by QN
                {0xF9, new Smart("NAND Writes (1GiB)")}, // added by QN
                {0xFA, new Smart("Read error retry rate")},
                {0xFB, new Smart("Minimum Spares Remaining")}, // added by QN
                {0xFC, new Smart("Newly Added Bad Flash Block")}, // added by QN
                {0xFE, new Smart("Free Fall Protection")}, // added by QN
                /* slot in any new codes you find in here */
            };
    }

    public class Smart
    {
        public bool HasData
        {
            get
            {
                if (Current == 0 && Worst == 0 && Threshold == 0 && Data == 0)
                    return false;
                return true;
            }
        }
        public string Attribute { get; set; }
        public int Current { get; set; }
        public int Worst { get; set; }
        public int Threshold { get; set; }
        public int Data { get; set; }
        public bool IsOK { get; set; }
        public string Health { get; set; }

        public Smart()
        {

        }

        public Smart(string attributeName)
        {
            this.Attribute = attributeName;
        }
    }


    public class getHddInfo
    {
        public ManagementScope scope;
        public ManagementScope scope2;
        private StringBuilder _logger = new StringBuilder();

        /* 
         * this part is by derek wilson, with some modification by Quy Nguyen to make it work here
        */

        public Dictionary<int, HDD> GetAllDisks()
        {
            // retrieve list of drives on computer (this will return both HDD's and CDROM's and Virtual CDROM's)                    
            var dicDrives = new Dictionary<int, HDD>();
            var wdSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            wdSearcher.Scope = scope;

            // extract model and interface information
            int iDriveIndex = 0;
            foreach (ManagementObject drive in wdSearcher.Get())
            {
                var hdd = new HDD();
                hdd.Id = drive["DeviceId"].ToString().Trim();
                hdd.Index = Convert.ToInt32(drive["Index"].ToString().Trim());
                hdd.Model = drive["Model"].ToString().Trim();
                hdd.Type = drive["InterfaceType"].ToString().Trim();
                _logger.AppendLine($"disk drive {hdd.Index} {hdd.Id} {hdd.Model}");
                dicDrives.Add(hdd.Index, hdd); // modified from iDriveIndex to Index
                iDriveIndex++;
            }
            return dicDrives;
        }

        public void GetSmartInformation(HDD drive)
        {
            // this is the actual physical drive number
            int index = drive.Index;
            _logger.AppendLine($"Drive Number {index} Drive {drive.Id}, {drive.Model}");

            // get wmi access to hdd 
            var searcher = new ManagementObjectSearcher("Select * from Win32_DiskDrive");
            //searcher.Scope = new ManagementScope(@"\root\wmi");
            searcher.Scope = scope2;

            // retrieve attribute flags, value worst and vendor data information
            searcher.Query = new ObjectQuery("Select * from MSStorageDriver_FailurePredictData");
            int iDriveIndex = 0;
            foreach (ManagementObject data in searcher.Get())
            {
                if (index == iDriveIndex)
                {
                    Byte[] bytes = (Byte[])data.Properties["VendorSpecific"].Value;
                    for (int i = 0; i < 30; ++i)
                    {
                        int id = 0;
                        try
                        {
                            id = bytes[i * 12 + 2];

                            int flags = bytes[i * 12 + 4]; // least significant status byte, +3 most significant byte, but not used so ignored.
                                                           //bool advisory = (flags & 0x1) == 0x0;
                            bool failureImminent = (flags & 0x1) == 0x1;
                            //bool onlineDataCollection = (flags & 0x2) == 0x2;

                            int value = bytes[i * 12 + 5];
                            int worst = bytes[i * 12 + 6];
                            int vendordata = BitConverter.ToInt32(bytes, i * 12 + 7);
                            if (id == 0) continue;

                            var attr = drive.Attributes[id];
                            attr.Current = value;
                            attr.Worst = worst;
                            attr.Data = vendordata;
                            attr.IsOK = !failureImminent;
                        }
                        catch
                        {
                            // given key does not exist in attribute collection (attribute not in the dictionary of attributes)
                            _logger.AppendLine($"SMART Key Not found {id}");
                        }
                    }
                }
                iDriveIndex++;
            }
        }

        /*
         * This part by Quy Nguyen
         * Check some specific attributes to see if the drive is still good
         */

        public void AnalyseSmartData(HDD drive)
        {
            /*
             * warning health if at least one of these attributes have value over 0
             * Critical attribute according to wikipedia
             * https://en.wikipedia.org/wiki/S.M.A.R.T.#Known_ATA_S.M.A.R.T._attributes
             * 0x05 "Reallocated sector count"
             * 0xC4 "Reallocation count"
             * 0xC5 "Current pending sector count"
             * 0xC6 "Offline scan uncorrectable count"
             * 0xC7 "UDMA CRC error rate"
             * 0x0A Spin Retry Count
             * 0xB8 End-to-End error
             * 0xBB Reported Uncorrectable Errors
             * 0xBC Command Timeout
             * 0xC9 Soft Read Error Rate
             */
            bool warning = false;
            foreach (var attr in drive.Attributes)
            {
                if (attr.Value.HasData)
                {
                    int id = attr.Key;
                    if ((id == 0x05 || id == 0xC4 || id == 0xC5 || id == 0xC6 || id == 0xC7
                         || id == 0x0A || id == 0xB8 || id == 0xBB || id == 0xBC || id == 0xC9)
                        && attr.Value.Data != 0)
                    {
                        warning = true;
                        attr.Value.IsOK = false;
                        attr.Value.Health = "warning";
                    }
                }
            }

            /*
             * failed health if any attribute is equal to or lower than its threshold
             */
            bool failed = false;
            foreach (var attr in drive.Attributes)
            {
                if (attr.Value.HasData)
                {
                    if (attr.Value.Worst <= attr.Value.Threshold)
                    {
                        failed = true;
                        attr.Value.IsOK = false;
                        attr.Value.Health = "failed";
                    }
                }
            }

            if (failed)
            {
                drive.IsOK = false;
                drive.Health = "FAILED";
            }
            else if (warning)
            {
                drive.IsOK = false;
                drive.Health = "WARNING";
            }
            else
            {
                drive.IsOK = true;
                drive.Health = "GOOD";
            }
        }

        /*
         * This part is a mix between Llewellyn Kruger's and derek wilson's code. Modification by Quy Nguyen
         */

        public void GetSmartThreshold(HDD drive)
        {
            // this is the actual physical drive number
            int index = drive.Index;
            _logger.AppendLine($"Drive Number {index} Drive {drive.Id}, {drive.Model}");

            // get wmi access to hdd 
            var searcher = new ManagementObjectSearcher("Select * from Win32_DiskDrive");
            //searcher.Scope = new ManagementScope(@"\root\wmi");
            searcher.Scope = scope2;

            // retreive threshold values foreach attribute
            searcher.Query = new ObjectQuery("Select * from MSStorageDriver_FailurePredictThresholds");
            int iDriveIndex = 0;
            foreach (ManagementObject data in searcher.Get())
            {
                if (index == iDriveIndex)
                {
                    Byte[] bytes = (Byte[])data.Properties["VendorSpecific"].Value;
                    for (int i = 0; i < 30; ++i)
                    {
                        try
                        {
                            int id = bytes[i * 12 + 2];
                            int thresh = bytes[i * 12 + 3];
                            if (id == 0) continue;

                            var attr = drive.Attributes[id];
                            attr.Threshold = thresh;
                        }
                        catch
                        {
                            // given key does not exist in attribute collection (attribute not in the dictionary of attributes)
                        }
                    }
                }
                iDriveIndex++;
            }
        }

        /*
         * This part is modified from Llewellyn Kruger's code. Modification by Quy Nguyen
         */

        public void compileSmartInfo(HDD drive)
        {
            var output = new StringBuilder();

            {
                output.AppendLine("-----------------------------------------------------");
                output.AppendLine(" DRIVE (" + drive.Health + "): " + drive.Serial + " - " + drive.Model + " - " + drive.Type);
                output.AppendLine("-----------------------------------------------------");
                output.AppendLine("");

                output.AppendLine("ID# ATTRIBUTE_NAME                           CURRENT  WORST  THRESHOLD  DATA       STATUS");
                foreach (var attr in drive.Attributes)
                {
                    if (attr.Value.HasData)
                        output.AppendLine(string.Format("{4,3} {0,-40} {1,-8} {2,-6} {3,-10} {5,-10} {6}", attr.Value.Attribute, attr.Value.Current, attr.Value.Worst, attr.Value.Threshold, attr.Key.ToString(), attr.Value.Data, ((attr.Value.IsOK) ? "ok" : attr.Value.Health)));
                }
            }
            drive.smart = output.ToString();
        }

        /*
         * This part is by Quy Nguyen
         */

        public Dictionary<int, HDD> getAllDiskData()
        {
            var HDDs = GetAllDisks();
            foreach (var HDD in HDDs)
            {
                GetSmartInformation(HDD.Value);
                GetSmartThreshold(HDD.Value);
                AnalyseSmartData(HDD.Value);
                compileSmartInfo(HDD.Value);
            }
            return HDDs;
        }
    }
}
