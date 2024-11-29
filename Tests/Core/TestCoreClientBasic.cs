using Deve.Core;
using Deve.External;

namespace Deve.Tests.Core
{
    public class TestCoreClientBasic : TestBaseDataGet<ICore, ClientBasic, Client, CriteriaClientBasic>, IClassFixture<FixtureDataCore>, IClassFixture<FixtureDataCoreLogged>
    {
        public TestCoreClientBasic(FixtureDataCore fixtureDataCore, FixtureDataCoreLogged fixtureDataLogged)
            : base(fixtureDataCore, fixtureDataLogged)
        {
        }

        protected override IDataGet<ClientBasic, Client, CriteriaClientBasic> GetDataGet(ICore data) => data.ClientsBasic;
    }
}