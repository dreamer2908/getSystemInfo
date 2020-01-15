using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using systemResourceAlerter;

namespace mergeIniFile
{
    class Program
    {
        static void Main(string[] args)
        {
            // append smallFile to bigFile
            if (args.Length >= 2)
            {
                string smallFile = args[0];
                string bigFile = args[1];
                
                if (File.Exists(smallFile))
                {
                    // bigFile doesn't need to be exist
                    systemResourceAlerter.Settings.settingsPath = bigFile;

                    string[] smallLines = File.ReadAllLines(smallFile);

                    // simple merger, assume the ini files have a single session
                    for (int i = 0; i < smallLines.Length; i++)
                    {
                        string line = smallLines[i].Trim();
                        if (line.StartsWith("[") && line.EndsWith("]"))
                        {
                            systemResourceAlerter.Settings.SECTION = line.Substring(1, line.Length - 2);
                        }
                        else
                        {
                            string key = string.Empty;
                            string value = string.Empty;
                            if (splitKeyValue(line, ref key, ref value))
                            {
                                systemResourceAlerter.Settings.Set(key, value);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("File not found.");
                }
            }
            else
            {
                Console.WriteLine("Not enough paramenters.");
                Console.WriteLine("Usage: mergeIniFile.exe \"additional_settings.ini\" \"main_settings.ini\"");
            }
        }

        private static bool splitKeyValue(string line, ref string key, ref string value)
        {
            string[] parts = line.Split(new string[] { "=" }, 2, StringSplitOptions.None);
            if (parts.Length == 2)
            {
                key = parts[0];
                value = parts[1];
                return true;
            }
            return false;
        }
    }
}
