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

            try
            {
                Console.WriteLine("systemInfo.getVideo_monitor1");
                systemInfo.getVideo_monitor1();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            try
            {
                Console.WriteLine("systemInfo.getVideo_monitor2");
                systemInfo.getVideo_monitor2();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            try
            {
                Console.WriteLine("systemInfo.getVideo_monitor3");
                systemInfo.getVideo_monitor3();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            try
            {
                Console.WriteLine("systemInfo.getVideo_monitor4");
                systemInfo.getVideo_monitor4();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            try
            {
                Console.WriteLine("systemInfo.getVideo_monitor5");
                systemInfo.getVideo_monitor5();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            // List<systemInfo.sruct_networkInterfaceInfo> a = systemInfo.getNetwork_interfaces();

            misc.printStringArray(systemInfo.getVideo_adapter().ToArray());

            // systemInfo.getProgram_list();

            systemInfo.getHDD_list();

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
