using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WelcomeExtended.Loggers
{
    public class DatabseLogger : ILogger
    {
        private readonly string _name;

        public DatabseLogger(string name)
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

            using (var context = new DataLayer.DataBase.DatabaseContext())
            {
                LogMessage msg = new LogMessage()
                {
                    Message = message,
                    CreatedOn = DateTime.UtcNow,
                };

                context.LogMessages.Add(msg);
                context.SaveChanges();
            }
        }
    }
}
