using Deve.Internal.Data;
using Deve.Clients.Wpf.Interfaces;

namespace Deve.Tests.Wpf.Fixtures
{
    internal class FixtureDataService : IDataService
    {
        public IData Data { get; private set; }

        public FixtureDataService(IData data)
        {
            Data = data;
        }

        public void Dispose()
        {
            Data.Dispose();
        }
    }
}