using Deve.Core;
using Deve.Data;
using Deve.Auth.TokenManagers;
using Deve.Internal.Data;
using Deve.Clients.Wpf.Interfaces;

namespace Deve.Clients.Wpf.Services
{
    internal class DataService : IDataService
    {
        #region Fields
        private IData? _data;

        /// <summary>
        /// TokenManager for the CoreFactory (it uses CryptAes with auto generated Key and IV).
        /// Due to the auto-generation of the Key and IV, tokens are only valid during a single program execution.
        /// </summary>
        private readonly TokenManagerCrypt _tokenManager = new();
        #endregion

        #region IDataService
        public IData Data => _data ??= CoreFactory.Get(true, _tokenManager, new DataOptions()
        {
            LangCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
        });
        #endregion

        #region IDisposable
        public void Dispose()
        {
            _data?.Dispose();
            _tokenManager.Dispose();
        }
        #endregion
    }
}