using Deve.Internal;
using Deve.Sdk;
using Deve.Internal.Sdk;
using Deve.ClientApp.Maui.Interfaces;

namespace Deve.ClientApp.Maui.Services
{
    internal class DataService : IDataService
    {
        private IData? _data;

        public IData Data => _data ??= SdkFactory.Get(EnvironmentType.Staging, null, new LoggingHandlerLog());
    }
}
