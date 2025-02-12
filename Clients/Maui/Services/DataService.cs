using Deve.Sdk;
using Deve.Sdk.LoggingHandlers;
using Deve.Internal.Sdk;
using Deve.Internal.Data;
using Deve.Clients.Maui.Interfaces;

namespace Deve.Clients.Maui.Services
{
    internal class DataService : IDataService
    {
        #region Fields
        private IData? _data;
        #endregion

        #region IDataService
        public IData Data => _data ??= SdkFactory.Get(EnvironmentType.Staging, null, new LoggingHandlerLog());
        #endregion

        #region IDisposable
        public void Dispose()
        {
            _data?.Dispose();
        }
        #endregion
    }
}