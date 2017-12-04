using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;

namespace DribblyAPI.Helpers
{
    public static class DribblyLogger
    {
        public static List<LogItem> ItemsToWrite = new List<LogItem>();

        private static string _userName = "";

        private static Logger _logger;

        public static void startTransaction(String UserName, Logger logger)
        {
            _userName = UserName;
            _logger = logger;
            ItemsToWrite.Clear();
        }

        public static void addItem(String message, LogLevel logLevel, bool onlyWriteOnError = false)
        {
            ItemsToWrite.Add(new LogItem(message, logLevel, onlyWriteOnError));
        }

        public static void writeItems(LogLevel logLevel, bool clearItemsAfter = true)
        {
            if(_logger != null)
            {
                if (logLevel == LogLevel.Error || logLevel == LogLevel.Fatal)
                {
                    foreach (LogItem item in ItemsToWrite)
                    {
                        _logger.Log(item.logLevel, item.message);
                    }
                }
                else
                {
                    foreach (LogItem item in ItemsToWrite.Where(i => !i.onlyWriteOnError))
                    {
                        _logger.Log(item.logLevel, item.message);
                    }
                }
            }else
            {
                //TODO: write log
            }
                  
        }
    }

    public class LogItem
    {
        public LogLevel logLevel { get; set; }

        public String message { get; set; }

        public bool onlyWriteOnError { get; set; }

        public LogItem(String message, LogLevel logLevel, bool onlyWriteOnError = false)
        {
            this.logLevel = logLevel;
            this.message = message;
            this.onlyWriteOnError = onlyWriteOnError;
        }
    }
}