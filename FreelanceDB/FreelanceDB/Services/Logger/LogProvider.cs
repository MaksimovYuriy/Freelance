using StackExchange.Redis;

namespace FreelanceDB.Services.Logger
{
    public class LogProvider : ILoggerProvider
    {
        private readonly string _connectionString;
        private ConnectionMultiplexer _redis;

        public LogProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new LogService(this, categoryName, GetDatabse());
        }

        public void Dispose()
        {
            _redis?.Dispose();
        }

        private IDatabase GetDatabse()
        {
            if (_redis == null || !_redis.IsConnected)
            {
                _redis = ConnectionMultiplexer.Connect(_connectionString);
            }
            return _redis.GetDatabase();
        }
    }
}
