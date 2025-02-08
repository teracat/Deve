using Deve.Internal;
using Deve.ClientApp.Maui.Interfaces;

namespace Deve.Tests.Maui.Fixtures
{
    internal class FixtureDataService : IDataService
    {
        public IData Data { get; private set; }

        public FixtureDataService(IData data)
        {
            Data = data;
        }
    }
}