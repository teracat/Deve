using Deve.ClientApp.Wpf.Interfaces;
using Deve.Core;
using Deve.Internal;

namespace Deve.ClientApp.Wpf.Services
{
    internal class DataService : IDataService
    {
        private IData? _data;

        public IData Data => _data ??= CoreFactory.Get(true, null, new DataOptions()
        {
            LangCode = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
        });
    }
}