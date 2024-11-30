using Deve.Core;
using Deve.External;

namespace Deve.Tests.Core
{
    public class TestCoreClientBasic : TestBaseDataGet<ICore, ClientBasic, Client, CriteriaClientBasic>, IClassFixture<FixtureDataCore>
    {
        public TestCoreClientBasic(FixtureDataCore fixtureDataCore)
            : base(fixtureDataCore)
        {
        }

        protected override IDataGet<ClientBasic, Client, CriteriaClientBasic> GetDataGet(ICore data) => data.ClientsBasic;
    }
}