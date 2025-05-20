using System.Collections.Concurrent;

namespace Deve.Cache
{
    /// <summary>
    /// A simple in-memory cache implementation that supports expiration and optional automatic cleanup of expired entries.
    /// IMPORTANT: This implementation stores data in memory only. If the application is hosted in IIS or any environment 
    /// that recycles the application pool, the cache data will be lost when the application is restarted.
    /// </summary>
    public class SimpleInMemoryCache : ICache
    {
        #region Fields
        /// <summary>
        /// The dictionary that stores the cached values along with their expiration dates.
        /// </summary>
        private readonly ConcurrentDictionary<string, (object Value, DateTimeOffset? Expiration)> _cache = new();

        /// <summary>
        /// The timer for automatic cleanup of expired entries. If set to <c>null</c>, automatic cleanup is disabled.
        /// </summary>
        private readonly Timer? _cleanupTimer;
        #endregion

        #region Properties
        /// <inheritdoc />
        public TimeSpan? DefaultExpiry { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleInMemoryCache"/> class.
        /// </summary>
        /// <param name="defaultExpiry">The default expiration time for cached entries. If null, the object will never expire.</param>"
        /// <param name="cleanupInterval">The interval to execute the cleanup process for expired entries. If <see cref="TimeSpan.Zero"/> is used, the automatic cleanup process is disabled.</param>
        public SimpleInMemoryCache(TimeSpan? defaultExpiry, TimeSpan cleanupInterval)
        {
            DefaultExpiry = defaultExpiry;
            if (cleanupInterval > TimeSpan.Zero)
            {
                _cleanupTimer = new Timer(_ => CleanExpiredEntries(), null, TimeSpan.Zero, cleanupInterval);
            }
            else
            {
                _cleanupTimer = null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleInMemoryCache"/> class with a default expiration time of 5 minutes and automatic cleanup every .
        /// </summary>
        public SimpleInMemoryCache()
        {
            DefaultExpiry = TimeSpan.FromMinutes(5);
            _cleanupTimer = new Timer(_ => CleanExpiredEntries(), null, TimeSpan.Zero, TimeSpan.FromMinutes(15));
        }
        #endregion

        #region ICache
        /// <inheritdoc />
        public bool TryGet<T>(string key, out T value)
        {
            if (!_cache.TryGetValue(key, out var entry))
            {
                value = default!;
                return false;
            }

            if (entry.Expiration != null && entry.Expiration <= DateTimeOffset.UtcNow)
            {
                Remove(key);
                value = default!;
                return false;
            }

            if (entry.Value is not T)
            {
                value = default!;
                return false;
            }

            value = (T)entry.Value;
            return true;
        }

        /// <inheritdoc />am>
        public void Set<T>(string key, T value) => Set(key, value, DefaultExpiry);

        /// <inheritdoc />
        public void Set<T>(string key, T value, TimeSpan? expiry)
        {
            if (!expiry.HasValue)
            {
                _cache[key] = (value!, null);
                return;
            }

            _cache[key] = (value!, DateTimeOffset.UtcNow.Add(expiry.Value));
        }

        /// <inheritdoc />
        public void Remove(string key) => _cache.TryRemove(key, out _);
        #endregion

        #region IDisposable
        public void Dispose()
        {
            _cleanupTimer?.Dispose();
            _cache.Clear();
        }
        #endregion

        #region Private Methods
        /// <inheritdoc />
        private void CleanExpiredEntries()
        {
            // The ToList() method is used to create a copy of the keys to avoid potential exceptions 
            // in multithreaded scenarios where the collection may be modified during iteration.
            foreach (var key in _cache.Keys.ToList())
            {
                if (_cache.TryGetValue(key, out var entry) && entry.Expiration != null && entry.Expiration <= DateTimeOffset.UtcNow)
                {
                    Remove(key);
                }
            }
        }
        #endregion
    }
}
