using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace getSystemInfo_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            //Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
            //AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            //Console.WriteLine(systemInfo.getCPU_name());
            //Console.WriteLine(systemInfo.getCPU_coreCount());
            //Console.WriteLine(systemInfo.getCPU_threadCount());
            //Console.WriteLine(systemInfo.getCPU_clockSpeed());
            //Console.WriteLine(systemInfo.getCPU_socket());
            //Console.WriteLine(systemInfo.getRAM_stickCount());
            //Console.WriteLine(systemInfo.getRAM_slotCount());
            //Console.WriteLine(misc.byteToHumanSize(systemInfo.getRAM_totalCapacity()));

            //List<string[]> RAMs = systemInfo.getRAM_stickList();

            //misc.printListOfStringArray(RAMs);

            //Console.WriteLine(systemInfo.getMobo_manufacturer());
            //Console.WriteLine(systemInfo.getMobo_model());

            //Console.WriteLine(systemInfo.getSystem_manufacturer());
            //Console.WriteLine(systemInfo.getSystem_model());

            //Console.WriteLine(misc.byteToHumanSize(systemInfo.getSystem_systemDriveFreeSpace()));
            //Console.WriteLine(misc.byteToHumanSize(systemInfo.getSystem_systemDriveTotalSpace()));

            //Console.WriteLine(systemInfo.getSystem_OS());
            //Console.WriteLine(systemInfo.getSystem_language());
            //Console.WriteLine(systemInfo.getSystem_name());
            //Console.WriteLine(systemInfo.getSystem_domain());
            //Console.WriteLine(systemInfo.getSystem_currentUsername());

            //List<string[]> users = systemInfo.getSystem_listUserAccounts();
            //misc.printListOfStringArray(users);

            // list of test system: 
            // 1 - PC-276x: Win10 x64 EN
            // 2 - NAV 160.84: Win10 x64 EN
            // 3 - 160.28: Win7 x64 EN
            // 4 - 160.91: Win7 x86 EN
            // gives the wanted result in (1) and (2), crashes in (3), and simply hangs on (4)
            // systemInfo.getVideo_monitor1();

            // doesn't work here, only returning //DisplayX etc
            // systemInfo.getVideo_monitor2();

            // doesn't work here, only return Generic PnP Monitor  
            // systemInfo.getVideo_monitor3();

            // works in (1) and (2), crashes in (3), and simply hangs on (4)
            // systemInfo.getVideo_monitor4();

            // David Heffernan stuff
            // works fine in (1) and (2), crashes in (3), and simply hangs on (4)
            // monitorInfo_david.search();

            // List<systemInfo.sruct_networkInterfaceInfo> a = systemInfo.getNetwork_interfaces();

            misc.printStringArray(systemInfo.getVideo_adapter().ToArray());

            // systemInfo.getProgram_list();

            Console.ReadLine();
        }

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            // Log the exception, display it, etc
            Debug.WriteLine(e.Exception.Message);
        }
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Log the exception, display it, etc
            Debug.WriteLine((e.ExceptionObject as Exception).Message);
        }
    }
}
