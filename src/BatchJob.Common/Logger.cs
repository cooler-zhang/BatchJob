using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchJob.Common
{
    public class Logger
    {
        public static void Write(string message, LoggerLevel loggerLevel = LoggerLevel.Error, Exception ex = null)
        {
            switch (loggerLevel)
            {
                case LoggerLevel.Debug:
                    LogManager.GetLogger("Log").Debug(message);
                    break;
                case LoggerLevel.Info:
                    LogManager.GetLogger("Log").Info(message);
                    break;
                case LoggerLevel.Warn:
                    LogManager.GetLogger("Log").Warn(message);
                    break;
                case LoggerLevel.Error:
                    if (ex == null)
                        LogManager.GetLogger("Log").Error(message);
                    else
                        LogManager.GetLogger("Log").Error(message, ex);
                    break;
                case LoggerLevel.Fatal:
                    if (ex == null)
                        LogManager.GetLogger("Log").Fatal(message);
                    else
                        LogManager.GetLogger("Log").Error(message, ex);
                    break;
            }
        }

        public static void Write(Exception ex, LoggerLevel loggerLevel = LoggerLevel.Error)
        {
            Logger.Write(ex.Message, loggerLevel, ex);
        }

        public static void Write(string format, LoggerLevel loggerLevel = LoggerLevel.Error, params object[] arg0)
        {
            Logger.Write(string.Format(format, arg0), loggerLevel);
        }
    }

    public enum LoggerLevel
    {
        [Description("调试")]
        Debug = 0,
        [Description("信息")]
        Info = 1,
        [Description("警告")]
        Warn = 2,
        [Description("错误")]
        Error = 3,
        [Description("严重错误")]
        Fatal = 4
    }
}
