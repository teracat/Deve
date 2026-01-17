namespace Deve.Cache;


/// <summary>
/// Represents a caching mechanism for storing and retrieving objects.
/// </summary>
public interface ICache : IDisposable
{
    /// <summary>
    /// Default expiration time for new cached entries. This is used when no expiration date is provided.
    /// Set to null to disable default expiration.
    /// </summary>
    TimeSpan? DefaultExpiry { get; }

    /// <summary>
    /// Attempts to retrieve an object from the cache using the specified key.
    /// </summary>
    /// <typeparam name="T">The type of the object to retrieve.</typeparam>
    /// <param name="key">The unique key associated with the cached object.</param>
    /// <param name="value">
    /// When this method returns <c>true</c>, contains the object retrieved from the cache; 
    /// otherwise, contains the default value for the type of the object.
    /// </param>
    /// <returns>
    /// <c>true</c> if the object was successfully retrieved from the cache; otherwise, <c>false</c>.
    /// </returns>
    bool TryGet<T>(string key, out T value);

    /// <summary>
    /// Stores an object in the cache with the specified key.
    /// </summary>
    /// <typeparam name="T">The type of the object to store.</typeparam>
    /// <param name="key">The unique key to associate with the cached object.</param>
    /// <param name="value">The object to store in the cache.</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "The name won't confuse users")]
    void Set<T>(string key, T value);

    /// <summary>
    /// Stores an object in the cache with the specified key and expiration time.
    /// </summary>
    /// <typeparam name="T">The type of the object to store.</typeparam>
    /// <param name="key">The unique key to associate with the cached object.</param>
    /// <param name="value">The object to store in the cache.</param>
    /// <param name="expiry">The expiration time for the cached object. If null, the object will never expire.</param>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1716:Identifiers should not match keywords", Justification = "The name won't confuse users")]
    void Set<T>(string key, T value, TimeSpan? expiry);

    /// <summary>
    /// Removes an object from the cache by its key.
    /// </summary>
    /// <param name="key">The unique key of the object to remove.</param>
    void Remove(string key);
}
