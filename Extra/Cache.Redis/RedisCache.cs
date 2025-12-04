using System.Text.Json;
using StackExchange.Redis;
using Deve.Logging;

namespace Deve.Cache
{
    /// <summary>
    /// Implementation of the ICache interface using Redis as the cache system.
    /// </summary>
    public class RedisCache : ICache
    {
        #region Fields
        private readonly ConnectionMultiplexer? _connectionMultiplexer;
        private readonly IDatabase _database;
        #endregion

        #region Properties
        /// <inheritdoc />
        public TimeSpan? DefaultExpiry { get; set; } = TimeSpan.FromMinutes(5);
        public ConnectionMultiplexer? ConnectionMultiplexer => _connectionMultiplexer;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCache"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string to the Redis server.</param>
        public RedisCache(string connectionString)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString);
            _database = _connectionMultiplexer.GetDatabase();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCache"/> class with logging capabilities.
        /// </summary>
        /// <param name="connectionString">The connection string to the Redis server.</param>
        /// <param name="log">The <see cref="TextWriter"/> to log to.</param>
        public RedisCache(string connectionString, TextWriter log)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString, log);
            _database = _connectionMultiplexer.GetDatabase();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCache"/> class with a configuration action.
        /// </summary>
        /// <param name="configuration">The string configuration to use for this multiplexer.</param>
        /// <param name="configure">Action to further modify the parsed configuration options.</param>
        public RedisCache(string configuration, Action<ConfigurationOptions> configure)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(configuration, configure);
            _database = _connectionMultiplexer.GetDatabase();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCache"/> class with a configuration action and logging capabilities.
        /// </summary>
        /// <param name="configuration">The string configuration to use for this multiplexer.</param>
        /// <param name="configure">Action to further modify the parsed configuration options.</param>
        /// <param name="log">The TextWriter to log to.</param>
        public RedisCache(string configuration, Action<ConfigurationOptions> configure, TextWriter log)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(configuration, configure, log);
            _database = _connectionMultiplexer.GetDatabase();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RedisCache"/> class with an existing <see cref="IDatabase"/> instance.
        /// </summary>
        /// <param name="database">The existing <see cref="IDatabase"/> instance.</param>
        public RedisCache(IDatabase database)
        {
            _connectionMultiplexer = null;
            _database = database;
        }
        #endregion

        #region ICache
        /// <inheritdoc />
        public bool TryGet<T>(string key, out T value)
        {
            try
            {
                var redisValue = _database.StringGet(key);
                if (!redisValue.HasValue || redisValue.IsNull)
                {
                    value = default!;
                    return false;
                }

                // Fix ambiguity by explicitly converting to string
                var deserializedValue = JsonSerializer.Deserialize<T>(redisValue.ToString());
                if (deserializedValue is null)
                {
                    value = default!;
                    return false;
                }

                value = deserializedValue;
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                value = default!;
                return false;
            }
        }

        /// <inheritdoc />
        public void Set<T>(string key, T value) => Set(key, value, DefaultExpiry);

        /// <inheritdoc />
        public void Set<T>(string key, T value, TimeSpan? expiry)
        {
            try
            {
                var serializedValue = JsonSerializer.Serialize(value);
                if (expiry is null)
                {
                    _database.StringSet(key, serializedValue);
                    return;
                }

                _database.StringSet(key, serializedValue, expiry, false);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        /// <inheritdoc />
        public void Remove(string key) => _database.KeyDelete(key);
        #endregion

        #region IDisposable
        public void Dispose()
        {
            _connectionMultiplexer?.Dispose();
        }
        #endregion
    }
}
