using Basket.Host.Configurations;
using Basket.Host.Services.Interfaces;
using StackExchange.Redis;

namespace Basket.Host.Services
{
    public class RedisCacheConnectionService : IRedisCacheConnectionService, IDisposable
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionLazy;
        private bool _disposed;

        public RedisCacheConnectionService(
            IOptions<RedisConfig> config)
        {
            var redisConfigurationOptions = ConfigurationOptions.Parse(config.Value.Host);

            ConnectionMultiplexer ValueFactory() => ConnectionMultiplexer.Connect(redisConfigurationOptions);

            _connectionLazy =
                new Lazy<ConnectionMultiplexer>(ValueFactory);
        }

        public IConnectionMultiplexer Connection => _connectionLazy.Value;

        public void Dispose()
        {
            if (!_disposed)
            {
                Connection.Dispose();
                _disposed = true;
            }
        }
    }
}