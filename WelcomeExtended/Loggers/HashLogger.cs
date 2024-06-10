using System.Collections.Concurrent;
using System.Text;
using Microsoft.Extensions.Logging;

namespace WelcomeExtended.Loggers
{
    public class HashLogger : ILogger
    {
        private readonly ConcurrentDictionary<int, string> _logMessages;

        private readonly string _name;

        public HashLogger()
        {
            _logMessages = new ConcurrentDictionary<int, string>();
        }

        public HashLogger(string name)
        {
            _name = name;
            _logMessages = new ConcurrentDictionary<int, string>();
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            var message = formatter(state, exception);

            switch (logLevel) 
            {
                case LogLevel.Critical:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case LogLevel.Error:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case LogLevel.Warning:
                    Console.ForegroundColor= ConsoleColor.Yellow;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Console.WriteLine("~ LOGGER ~");
            var messageToBeLogger = new StringBuilder();
            messageToBeLogger.Append($"[{logLevel}]");
            messageToBeLogger.AppendFormat(" [{0}]", _name);
            Console.WriteLine(messageToBeLogger);
            Console.WriteLine($" {formatter(state, exception)}");
            Console.WriteLine("~ LOGGER ~");
            Console.ResetColor();
            _logMessages[eventId.Id] = message;
        }

        public void PrintAllMessages() 
        {
            Console.WriteLine("Printing all messages !");
            Console.WriteLine();

            foreach (var message in _logMessages)
            {
                PrintMessage(message.Key, message.Value);
            }
        }

        public void PrintMessageById(int eventId) 
        {
            var message = _logMessages.Where(lm => lm.Key == eventId).FirstOrDefault();

            if (message.Value != null) 
            {
                PrintMessage(message.Key, message.Value);
            }
        }

        public bool DeleteMessageById(int eventId) 
        {
            var message = _logMessages.Where(lm => lm.Key == eventId).FirstOrDefault();
            return _logMessages.TryRemove(message);
        }

        private void PrintMessage(int eventId, string message) 
        {
            Console.WriteLine($"Event id: {eventId} - Message: {message}");
        }
    }
}
