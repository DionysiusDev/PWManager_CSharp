using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            // add reference to pw manager model - accesses the dll / pw manager initializer class - calls create database method
            PWManager_Model.DLL.PWManagerInitializer.CreateDatabase();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            log4net.Config.XmlConfigurator.Configure(); // initialise the logger

            // Runs the login screen
            Application.Run(new Login());
        }
    }
}
