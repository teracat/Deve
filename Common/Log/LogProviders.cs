namespace Deve
{
    public class LogProviders
    {
        #region Fields
        private List<ILogProvider> _list = [];
        #endregion

        #region Properties
        internal List<ILogProvider> List => _list;
        #endregion

        #region Methods
        /// <summary>
        /// A new provider will be used to write the logs.
        /// </summary>
        /// <param name="provider">The new provider to be included. If the class type is already in the list of providers, it won't be added again.</param>
        /// <returns>True if the provider was added.</returns>
        public bool Add(ILogProvider provider)
        {
            if (_list.Any(x => x.GetType() == provider.GetType()))
                return false;

            _list.Add(provider);
            return true;
        }

        /// <summary>
        /// Remove a provider to stop sending messages to it. 
        /// </summary>
        /// <param name="provider">The provider to be removed.</param>
        /// <returns>True if the provider was removed.</returns>
        public bool Remove(ILogProvider provider)
        {
            return _list.Remove(provider);
        }
        #endregion
    }
}
