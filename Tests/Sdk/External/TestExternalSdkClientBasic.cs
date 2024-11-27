using Microsoft.AspNetCore.Mvc.Testing;
using Deve.External;
using Deve.External.Api;
using Deve.External.Sdk;
using Deve.Tests.Api;

namespace Deve.Tests.Sdk.External
{
    public class TestExternalSdkClientBasic : TestBaseDataGet<ISdk, ClientBasic, Client, CriteriaClientBasic>, IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly TestApiFactory<Program> _factory;

        public TestExternalSdkClientBasic(WebApplicationFactory<Program> factory)
        {
            _factory = new TestApiFactory<Program>(factory);
        }

        protected override ISdk CreateData() => TestsExternalSdkHelpers.CreateSdk(_factory.CreateClient());

        protected override IDataGet<ClientBasic, Client, CriteriaClientBasic> GetDataGet(ISdk sdk) => sdk.Clients;
    }
}