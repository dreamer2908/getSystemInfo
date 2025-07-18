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
        public bool? IsOK { get; set; }
        public string Health { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string MediaType { get; set; }
        public bool IsSSD { get; set; }
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
        public ManagementScope scope3;
        public static Boolean scope3_enabled;
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
                hdd.MediaType = drive["MediaType"].ToString().Trim();
                hdd.IsSSD = false;
                _logger.AppendLine($"disk drive {hdd.Index} {hdd.Id} {hdd.Model}");
                dicDrives.Add(hdd.Index, hdd); // modified from iDriveIndex to Index
                iDriveIndex++;
                //Console.WriteLine(drive["Index"].ToString());
                //Console.WriteLine(drive["Model"].ToString());
                //Console.WriteLine(drive["MediaType"].ToString());
                //Console.WriteLine(drive["InterfaceType"].ToString());
            }

            // extract MediaType info (SSD/HDD)
            // only when scope3 is enabled
            if (scope3_enabled)
            {
                var dicMedia = new Dictionary<int, string>();
                var mediaSearcher = new ManagementObjectSearcher("SELECT * FROM MSFT_PhysicalDisk");
                mediaSearcher.Scope = scope3;

                // extract model and interface information
                int iMax = iDriveIndex;
                foreach (ManagementObject drive in mediaSearcher.Get())
                {
                    string sDriveID = drive["DeviceId"].ToString().Trim();
                    int iDriveID = Convert.ToInt16(sDriveID);
                    //Console.WriteLine(drive["DeviceId"].ToString());
                    //Console.WriteLine(drive["FriendlyName"].ToString());
                    //Console.WriteLine(drive["MediaType"].ToString());
                    //Console.WriteLine(drive["BusType"].ToString());
                    int iMediaType = Convert.ToInt16(drive["MediaType"].ToString().Trim());
                    bool isSSD = false;
                    int iBusType = Convert.ToInt16(drive["BusType"].ToString().Trim());

                    string sMediaType = string.Empty;
                    switch (iMediaType)
                    {
                        case 1:
                            sMediaType = "Unspecified"; break;
                        case 3:
                            sMediaType = "HDD"; break;
                        case 4:
                            sMediaType = "SSD"; isSSD = true; break;
                        case 5:
                            sMediaType = "SCM"; isSSD = true; break;
                        default:
                            sMediaType = "Unspecified"; break;
                    }

                    string sBusType = string.Empty;
                    switch (iBusType)
                    {
                        case 1:
                            sBusType = "SCSI"; break;
                        case 2:
                            sBusType = "ATAPI"; break;
                        case 3:
                            sBusType = "ATA"; break;
                        case 4:
                            sBusType = "1394"; break;
                        case 5:
                            sBusType = "SSA"; break;
                        case 6:
                            sBusType = "Fibre Channel"; break;
                        case 7:
                            sBusType = "USB"; break;
                        case 8:
                            sBusType = "RAID"; break;
                        case 9:
                            sBusType = "iSCSI"; break;
                        case 10:
                            sBusType = "SAS"; break;
                        case 11:
                            sBusType = "SATA"; break;
                        case 12:
                            sBusType = "SD"; break;
                        case 13:
                            sBusType = "MMC"; break;
                        case 14:
                            sBusType = "MAX"; break;
                        case 15:
                            sBusType = "File Backed Virtual"; break;
                        case 16:
                            sBusType = "Storage Spaces"; break;
                        case 17:
                            sBusType = "NVMe"; break;
                        case 18:
                            sBusType = "Microsoft Reserved"; break;
                        default:
                            sBusType = "Unknown"; break;
                    }

                    for (int i = 0; i < iMax; i++)
                    {
                        if (dicDrives[i].Index == iDriveID)
                        {
                            dicDrives[i].Type = sBusType;
                            dicDrives[i].MediaType = sMediaType;
                            dicDrives[i].IsSSD = isSSD;
                            break;
                        }
                    }
                }
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
            try
            {
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
            catch (System.Management.ManagementException)
            {
                _logger.AppendLine("SMART not supported.");
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
            bool overallWarning = false;
            bool overallUnknown = true;
            int attCount = 0;
            foreach (var attr in drive.Attributes)
            {
                if (attr.Value.HasData)
                {
                    attCount++;
                    int id = attr.Key;
                    if (drive.IsSSD)
                    {
                        // TODO: find critical attributes for SSD
                    }
                    else
                    {
                        overallUnknown = false;
                        if ((id == 0x05 || id == 0xC4 || id == 0xC5 || id == 0xC6 || id == 0xC7
                             || id == 0x0A || id == 0xB8 || id == 0xBB || id == 0xBC || id == 0xC9)
                            && attr.Value.Data != 0)
                        {
                            overallWarning = true;
                            attr.Value.IsOK = false;
                            attr.Value.Health = "warning";
                        }

                    }
                }
            }

            /*
             * If one or more "current value" is smaller than "threshold value" (except the "threshold value" is 0), that will reported as "drive failure"
             */
            bool overallFailed = false;
            bool overallFailedBefore = false;
            int attrFailedBefore = 0;
            foreach (var attr in drive.Attributes)
            {
                if (attr.Value.HasData)
                {
                    bool failedBefore = (attr.Value.Worst < attr.Value.Threshold || attr.Value.Worst == 0) && !(drive.IsSSD && attr.Value.Worst == 0); // added correction for some SSDs where worst values = 0
                    bool failedNow = attr.Value.Current < attr.Value.Threshold || attr.Value.Current == 0;

                    // some SSDs report faulty current value < worst value. Ignore in this case
                    if (attr.Value.Current < attr.Value.Worst)
                    {
                        failedBefore = failedNow = false;
                    }

                    if (failedNow)
                    {
                        overallFailed = true;
                        attr.Value.IsOK = false;
                        attr.Value.Health = "failed";
                    }
                    else if (failedBefore)
                    {
                        overallFailedBefore = true;
                        attr.Value.IsOK = false;
                        attr.Value.Health = "failed in the past";
                        attrFailedBefore += 1;
                    }
                }
            }

            // if the drive is flash-based, assume GOOD if SMART works and there's no warning or failure
            Console.WriteLine(attCount);
            if (overallUnknown && !overallFailed && !overallFailedBefore && !overallWarning && (attCount > 0))
            {
                drive.IsOK = true;
                drive.Health = "GOOD";
            }
            // if SMART doesn't work (blank attribute list) or there're some warning or past failure, keep it UNKNOWN
            else if (overallUnknown && !overallFailed)
            {
                drive.IsOK = null;
                drive.Health = "UNKNOWN";
            }
            else if (overallFailed)
            {
                drive.IsOK = false;
                drive.Health = "FAILED";
            }
            else if (overallWarning)
            {
                drive.IsOK = false;
                drive.Health = "WARNING";
            }
            else
            {
                drive.IsOK = true;
                drive.Health = "GOOD";
            }
            if (overallFailedBefore)
            {
                drive.Health = string.Format("{0}, {1} attr failed in the past", drive.Health, attrFailedBefore);
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
            try
            {
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
            catch (System.Management.ManagementException)
            {
                _logger.AppendLine("SMART not supported.");
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
                output.AppendLine(" DRIVE (" + drive.Health + "): " + drive.Serial + " - " + drive.Model + " - " + drive.Type + " - " + drive.MediaType);
                output.AppendLine("-----------------------------------------------------");
                output.AppendLine("");

                output.AppendLine("ID# ATTRIBUTE_NAME                           CURRENT  WORST  THRESHOLD  DATA        STATUS");
                foreach (var attr in drive.Attributes)
                {
                    if (attr.Value.HasData)
                        output.AppendLine(string.Format("{4,3} {0,-40} {1,-8} {2,-6} {3,-10} {5,-11} {6}", attr.Value.Attribute, attr.Value.Current, attr.Value.Worst, attr.Value.Threshold, attr.Key.ToString(), attr.Value.Data, ((attr.Value.IsOK) ? "ok" : attr.Value.Health)));
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
