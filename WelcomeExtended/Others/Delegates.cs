using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WelcomeExtended.Helpers;
using WelcomeExtended.Loggers;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WelcomeExtended.Others
{
    public static class Delegates
    {
        public static readonly ILogger logger = LoggerHelper.GetLogger("Console");

        public static readonly ILogger fileLogger = LoggerHelper.GetFileLogger("File");

        public static readonly ILogger loginLogger = LoggerHelper.GetFileLogger("Login");

        public static readonly ILogger databaseLogger = LoggerHelper.GetDatabaseLogger("Database");


        public static void Log(string error) 
        {
            logger.LogError(error);
            databaseLogger.LogError(error);
        }

        public static void Log2(string error)
        {
            Console.WriteLine("~ DELEGATES ~");
            Console.WriteLine($"{error}");
            Console.WriteLine("~ DELEGATES ~");
        }

        public static void LogToFile(string error) 
        {
            fileLogger.LogError(error);
            databaseLogger.LogError(error);
        }

        public static void LogLoginSuccess(string username) 
        {
            string message = $"User: {username} has succssesfully loged in!";
            loginLogger.LogInformation(message);
            databaseLogger.LogError(message);
        }

        public static void LogLoginError(string username)
        {
            string message = $"{username} was not found!";
            LogLoginError(message, false);
        }

        private static void LogLoginError(string error, bool isError)
        {
            if (isError) 
            {
                loginLogger.LogError(error);
                databaseLogger.LogError(error);
            }
            else
            {
                loginLogger.LogInformation(error);
                databaseLogger.LogInformation(error);
            }
        }

        public delegate void ActionOnError(string errorMessage);
    }
}
