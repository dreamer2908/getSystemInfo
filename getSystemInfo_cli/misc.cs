using System;
using System.Collections.Generic;
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
    }
}
