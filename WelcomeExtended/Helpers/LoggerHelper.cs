using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeExtended.Loggers;

namespace WelcomeExtended.Helpers
{
    public static class LoggerHelper
    {
        public static ILogger GetLogger(string categoryName) 
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new LoggerProvider());

            return loggerFactory.CreateLogger(categoryName);
        }

        public static ILogger GetFileLogger(string categoryName)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new FileLoggerProvider());

            return loggerFactory.CreateLogger(categoryName);
        }

        public static ILogger GetLoginLogger(string categoryName)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new LoginLoggerProvider());

            return loggerFactory.CreateLogger(categoryName);
        }

        public static ILogger GetDatabaseLogger(string categoryName)
        {
            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new DatabaseLoggerProvider());

            return loggerFactory.CreateLogger(categoryName);
        }
    }
}
