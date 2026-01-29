using Microsoft.Extensions.Caching.Memory;

namespace Deve.Cache;

/// <summary>
/// An implementation of the ICache interface using Microsoft.Extensions.Caching.Memory.MemoryCache.
/// </summary>
public sealed class InMemoryCache : ICache
{
    #region Fields
    /// <summary>
    /// The memory cache instance used for storing cached values.
    /// </summary>
    private readonly MemoryCache _memoryCache;
    #endregion

    #region Properties
    /// <inheritdoc />
    public TimeSpan? DefaultExpiry { get; set; }
    #endregion

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="InMemoryCache"/> class with default options.
    /// </summary>
    public InMemoryCache()
    {
        _memoryCache = new MemoryCache(new MemoryCacheOptions());
        DefaultExpiry = TimeSpan.FromMinutes(5);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InMemoryCache"/> class with the specified cache options.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the memory cache, such as size limits, expiration settings, and compaction behavior.
    /// </param>
    public InMemoryCache(MemoryCacheOptions options)
    {
        _memoryCache = new MemoryCache(options);
        DefaultExpiry = TimeSpan.FromMinutes(5);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InMemoryCache"/> class with the specified cache options.
    /// </summary>
    /// <param name="options">
    /// The configuration options for the memory cache, such as size limits, expiration settings, and compaction behavior.
    /// </param>
    /// <param name="defaultExpiry">Sets the default expiration time for new cached entries. This is used when no expiration date is provided.</param>
    public InMemoryCache(MemoryCacheOptions options, TimeSpan defaultExpiry)
    {
        _memoryCache = new MemoryCache(options);
        DefaultExpiry = defaultExpiry;
    }
    #endregion

    #region ICache
    /// <inheritdoc />
    public bool TryGet<T>(string key, out T value)
    {
        if (_memoryCache.TryGetValue(key, out var cachedValue))
        {
            value = (T)cachedValue!;
            return true;
        }

        value = default!;
        return false;
    }

    /// <inheritdoc />
    public void Set<T>(string key, T value) => Set(key, value, DefaultExpiry);

    /// <inheritdoc />
    public void Set<T>(string key, T value, TimeSpan? expiry)
    {
        if (!expiry.HasValue)
        {
            _ = _memoryCache.Set(key, value);
            return;
        }

        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiry.Value,
        };
        _ = _memoryCache.Set(key, value, options);
    }

    /// <inheritdoc />
    public void Remove(string key) => _memoryCache.Remove(key);
    #endregion

    #region IDisposable
    public void Dispose() => _memoryCache.Dispose();
    #endregion
}
