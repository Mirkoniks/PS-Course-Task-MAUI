using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Text;

namespace WelcomeExtended.Loggers
{
    public class FileLogger : ILogger
    {
        private readonly string _name;

        public FileLogger(string name)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            // Scope is not implemented for simplicity
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // You can implement more complex logic here to enable/disable logging based on logLevel
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));
            if (formatter == null) throw new ArgumentNullException(nameof(formatter));

            try
            {
                string file = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName, "ErrorsLog.txt");

                using (StreamWriter writer = File.AppendText(file))
                {
                    writer.WriteLine("~ LOGGER ~");
                    writer.WriteLine(DateTime.UtcNow.ToString("dd-MM-yyyy HH:mm:ss"));

                    var messageToBeLogged = new StringBuilder();
                    messageToBeLogged.Append($"[{logLevel}]");
                    messageToBeLogged.AppendFormat(" [{0}]", _name);

                    writer.WriteLine(messageToBeLogged);
                    writer.WriteLine($" {formatter(state, exception)}");

                    if (exception != null)
                    {
                        writer.WriteLine($"Exception: {exception}");
                    }

                    writer.WriteLine("~ LOGGER ~");
                    writer.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                // Handle logging exception (e.g., file IO issues)
                Console.Error.WriteLine($"Logging failed: {ex}");
            }
        }
    }
}
