using Deve.Core;
using Deve.Data;
using Deve.Internal.Data;
using Deve.Clients.Wpf.Interfaces;

namespace Deve.Clients.Wpf.Services
{
    internal class DataService : IDataService
    {
        private IData? _data;

        #region IDataService
        public IData Data => _data ??= CoreFactory.Get(true, null, new DataOptions()
        {
            LangCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
        });
        #endregion

        #region IDisposable
        public void Dispose()
        {
            _data?.Dispose();
        }
        #endregion
    }
}