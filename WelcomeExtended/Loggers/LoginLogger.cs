using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WelcomeExtended.Loggers
{
    public class LoginLogger : ILogger
    {
        private readonly string _name;

        public LoginLogger(string name)
        {
            _name = name;
        }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var message = formatter(state, exception);

            string startupPath = Path.Combine(Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "LoginLog.txt");

            // Read the file as one string. 
            string text = System.IO.File.ReadAllText(startupPath);

            string file = FindSolutionFolderPath();

            using (StreamWriter writer = File.AppendText(file))
            {
                
                writer.WriteLine("~ LOGGER ~");
                writer.WriteLine(DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss"));
                var messageToBeLogger = new StringBuilder();
                messageToBeLogger.Append($"[{logLevel}]");
                messageToBeLogger.AppendFormat(" [{0}]", _name);
                writer.WriteLine(messageToBeLogger);
                writer.WriteLine($" {formatter(state, exception)}");
                writer.WriteLine("~ LOGGER ~");
                writer.WriteLine("");
            }
        }

        public string FindSolutionFolderPath()
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            while (!string.IsNullOrEmpty(currentDirectory) && !Directory.GetFiles(currentDirectory, "*.sln").Any())
            {
                currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            }

            return currentDirectory ?? string.Empty;
        }
    }
}
