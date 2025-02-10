using Deve.Criteria;
using Deve.Model;
using Deve.External.Data;
using Deve.External.Sdk;
using Deve.External.Model;
using Deve.Tests.Sdk.External.Fixtures;

namespace Deve.Tests.Sdk.External
{
    public class TestExternalSdkClientBasic : TestBaseDataGet<ISdk, ClientBasic, Client, CriteriaClientBasic>, IClassFixture<FixtureSdkExternal>
    {
        public TestExternalSdkClientBasic(FixtureSdkExternal fixture)
            : base(fixture)
        {
        }

        protected override IDataGet<ClientBasic, Client, CriteriaClientBasic> GetDataGet(ISdk sdk) => sdk.Clients;
    }
}