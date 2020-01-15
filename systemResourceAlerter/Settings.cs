using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace systemResourceAlerter
{
    // by vitalinvent from https://stackoverflow.com/a/31044553
    // with defVal fix and changed filename

    public static class Settings
    {
        public static string SECTION = typeof(Settings).Namespace;//"SETTINGS";
        public static string settingsPath = Application.StartupPath.ToString() + "\\settings.ini";
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        public static String GetString(String name)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(SECTION, name, "", temp, 255, settingsPath);
            return temp.ToString();
        }
        public static String Get(String name, String defVal)
        {
            return Get(SECTION, name, defVal);
        }
        public static String Get(string _SECTION, String name, String defVal)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(_SECTION, name, "", temp, 255, settingsPath);
            // quy nguyen changed this
            if (i > 0)
            {
                return temp.ToString();
            }
            else
            {
                return defVal;
            }
        }
        public static Boolean Get(String name, Boolean defVal)
        {
            return Get(SECTION, name, defVal);
        }
        public static Boolean Get(string _SECTION, String name, Boolean defVal)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(_SECTION, name, "", temp, 255, settingsPath);
            bool retval = false;
            if (bool.TryParse(temp.ToString(), out retval))
            {
                return retval;
            }
            else
            {
                return defVal; // quy nguyen changed this
            }
        }
        public static int Get(String name, int defVal)
        {
            return Get(SECTION, name, defVal);
        }
        public static int Get(string _SECTION, String name, int defVal)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(SECTION, name, "", temp, 255, settingsPath);
            int retval = 0;
            if (int.TryParse(temp.ToString(), out retval))
            {
                return retval;
            }
            else
            {
                return defVal; // quy nguyen changed this
            }
        }
        public static void Set(String name, String val)
        {
            Set(SECTION, name, val);
        }
        public static void Set(string _SECTION, String name, String val)
        {
            WritePrivateProfileString(_SECTION, name, val, settingsPath);
        }
        public static void Set(String name, Boolean val)
        {
            Set(SECTION, name, val);
        }
        public static void Set(string _SECTION, String name, Boolean val)
        {
            WritePrivateProfileString(_SECTION, name, val.ToString(), settingsPath);
        }
        public static void Set(String name, int val)
        {
            Set(SECTION, name, val);
        }
        public static void Set(string _SECTION, String name, int val)
        {
            WritePrivateProfileString(SECTION, name, val.ToString(), settingsPath);
        }
    }
}
