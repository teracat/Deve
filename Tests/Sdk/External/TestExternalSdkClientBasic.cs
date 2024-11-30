using Deve.External;
using Deve.External.Sdk;

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