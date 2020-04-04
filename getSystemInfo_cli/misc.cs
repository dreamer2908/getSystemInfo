using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace getSystemInfo_cli
{
    class misc
    {

        private static string generateFloatStringFormatDescriptor(int decimalPlaces, bool decimalIsOptional)
        {
            string result = "0";

            if (decimalPlaces > 0)
            {
                result += ".";
                for (int i = 0; i < decimalPlaces; i++)
                {
                    result += (decimalIsOptional ? "#" : "0");
                }
            }

            return result;
        }

        public static string byteToHumanSize(long bytes, int decimalPlaces)
        {
            string[] units = { "B", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB", "ZiB", "YiB" };

            if (bytes < 1000)
            {
                return String.Format("{0} {1}", bytes, units[0]);
            }

            double value = bytes;
            int unit = 0;

            while (value >= 1000)
            {
                unit++;
                value = value / 1024;
            }

            string formatOptions = "{0:" + generateFloatStringFormatDescriptor(decimalPlaces, true) + "} {1}";
            return String.Format(formatOptions, value, units[unit]);
        }

        public static string byteToHumanSize(long bytes)
        {
            return byteToHumanSize(bytes, 3);
        }

        public static string bpsToHumanSize(long speed, int decimalPlaces)
        {
            string[] units = { "Bps", "Kbps", "Mbps", "Gbps", "Tbps", "Pbps", "Ebps", "Zbps", "Ybps" };

            if (speed < 1000)
            {
                return String.Format("{0} {1}", speed, units[0]);
            }

            double value = speed;
            int unit = 0;

            while (value >= 1000)
            {
                unit++;
                value = value / 1000;
            }

            string formatOptions = "{0:" + generateFloatStringFormatDescriptor(decimalPlaces, true) + "} {1}";
            return String.Format(formatOptions, value, units[unit]);
        }

        public static string bpsToHumanSize(long bytes)
        {
            return bpsToHumanSize(bytes, 3);
        }

        public static void printListOfStringArray(List<string[]> l)
        {
            for (int i = 0; i < l.Count; i++)
            {
                string[] arr = l[i];
                Console.WriteLine(i.ToString() + ":");
                foreach (string s in arr)
                {
                    Console.WriteLine("    " + s);
                }
            }
        }

        public static void printStringArray(string[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                string s = arr[i];
                Console.WriteLine(i.ToString() + ": " + s);
            }
        }

        public static List<string> getDistinct_stringList(List<string> list)
        {
            List<string> result = new List<string>();
            foreach(string s in list)
            {
                if (!result.Contains(s))
                {
                    result.Add(s);
                }
            }
            return result;
        }


        public static void writeCsv(List<string[]> log, string filename, bool overwrite)
        {
            string csvContents = convertToCsvContents(log);

            if (overwrite)
            {
                File.WriteAllText(filename, csvContents);
            }
            else
            {
                File.AppendAllText(filename, csvContents);
            }
        }

        public static string convertToCsvContents(List<string[]> log)
        {
            StringBuilder sb = new StringBuilder();
            int logCount = log.Count;
            for (int i = 0; i < logCount; i++)
            {
                var logLine = log[i];
                for (int j = 0; j < logLine.Length; j++)
                {
                    logLine[j] = quoteStringForCsv(logLine[j]);
                }
                sb.AppendLine(string.Join(",", logLine));
            }

            string csvContents = sb.ToString();
            return csvContents;
        }

        // see https://en.m.wikipedia.org/wiki/Comma-separated_values for what to quote
        private static string quoteStringForCsv(string input)
        {
            string result = input;

            if (result.Contains(",") || result.Contains("\"") || result.Contains("\n") || result.Contains("\r"))
            {
                result = result.Replace("\"", "\"\"");
                result = "\"" + result + "\"";
            }

            return result;
        }
    }
}
