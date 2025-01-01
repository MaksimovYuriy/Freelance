using FreelanceDB.Database.Context;
using StackExchange.Redis;
using FreelanceDB.Exceptions;

namespace FreelanceDB.Services
{
    public class LogService : ILogger
    {
        private readonly string _categoryName;
        private readonly IDatabase _database;

        public LogService(LogProvider provider, string categoryName, IDatabase database)
        {
            _categoryName = categoryName;
            _database = database;
        }

        public IDisposable? BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true; // Логируем все уровни
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var message = formatter(state, exception ?? new CustomNullException("Null-exception"));

            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            // Запись лога в Redis
            var key = $"{_categoryName}:{DateTime.Now.ToString("yyyy-MM-dd")}";
            var value = $"[{logLevel}] {message}";

            _database.ListRightPush(key, value);
        }
    }
}
