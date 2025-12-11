using Deve.Dto;
using Deve.External.Data;
using Deve.External.Dto;
using Deve.External.Sdk;
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