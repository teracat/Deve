namespace Deve.Logging;

/// <summary>
/// Manages and retains the collection of logging providers used for handling log messages.
/// </summary>
public sealed class LogProviders
{
    #region Properties
    internal List<ILogProvider> List { get; } = [];
    #endregion

    #region Methods
    /// <summary>
    /// A new provider will be used to write the logs.
    /// </summary>
    /// <param name="provider">The new provider to be included. If the class type is already in the list of providers, it won't be added again.</param>
    /// <returns>True if the provider was added.</returns>
    public bool Add(ILogProvider provider)
    {
        lock (List)
        {
            if (List.Any(x => x.GetType() == provider.GetType()))
            {
                return false;
            }

            List.Add(provider);
            return true;
        }
    }

    /// <summary>
    /// Remove a provider to stop sending messages to it. 
    /// </summary>
    /// <param name="provider">The provider to be removed.</param>
    /// <returns>True if the provider was removed.</returns>
    public bool Remove(ILogProvider provider)
    {
        lock (List)
        {
            return List.Remove(provider);
        }
    }
    #endregion
}
