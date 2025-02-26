using Deve.Core;
using Deve.Criteria;
using Deve.Model;
using Deve.External.Data;
using Deve.External.Model;
using Deve.Tests.Core.Fixtures;

namespace Deve.Tests.Core
{
    public class TestCoreClientBasic : TestBaseDataGet<ICore, ClientBasic, Client, CriteriaClientBasic>, IClassFixture<FixtureCore>
    {
        public TestCoreClientBasic(FixtureCore fixtureDataCore)
            : base(fixtureDataCore)
        {
        }

        protected override IDataGet<ClientBasic, Client, CriteriaClientBasic> GetDataGet(ICore data) => data.ClientsBasic;
    }
}