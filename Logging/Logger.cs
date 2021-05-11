using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public class Logger
    {
        public static void LogDebug(string debugMessage)
        {
            log4net.ILog _Logger = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            _Logger.Debug(debugMessage);
        }

        public static void LogInfo(string infoMessage)
        {
            log4net.ILog _Logger = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            _Logger.Info(infoMessage);
        }

        public static void LogError(string errorMessage)
        {
            log4net.ILog _Logger = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            _Logger.Error(errorMessage);
        }
    }
}
