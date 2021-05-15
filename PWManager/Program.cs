using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PWManager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool isProcessRunning = false;
            bool isCheckedRunningProcesses = false;

            log4net.Config.XmlConfigurator.Configure(); // initialise the logger

            try
            {
                // string to store the process name from task manager
                string PWManagerProcess = Process.GetCurrentProcess().ProcessName;

                // checks if the current process is running already
                if (Process.GetProcesses().Count(p => p.ProcessName == PWManagerProcess) > 1)
                {
                    isProcessRunning = true;
                    return;
                }
                else
                {
                    isCheckedRunningProcesses = true;
                }

            } catch (Exception e)
            {
                Logging.Logger.LogError("[PWManager] [Main] Process is Running Error " + e);
            }

            // checks if processes aren't already running
            if (!isProcessRunning && isCheckedRunningProcesses)
            {
                // initializes the database
                PWManager_Model.DLL.PWManagerInitializer.CreateDatabase();

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Runs the login screen
                Application.Run(new Login());
            }
        }
    }
}
