using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;

namespace ReferralApp.Models
{
    public class Logger
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Info(string message)
        {
            log.Info(message);

        }

        public static void Error(string message)
        {
            log.Error(message);
        }

        public static void Error(Exception ex)
        {
            log.Error(ex.Message, ex);
        }


        public static void Error(string message, Exception ex)
        {
            log.Error(message, ex);
        }

        public static void Debug(string message)
        {
            log.Debug(message);
        }
    }
}