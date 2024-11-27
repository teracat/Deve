using Microsoft.AspNetCore.Mvc.Testing;
using Deve.External;
using Deve.External.Api;
using Deve.External.Sdk;
using Deve.Tests.Api;

namespace Deve.Tests.Sdk.External
{
    public class TestExternalSdkCity : TestBaseDataGet<ISdk, City, City, CriteriaCity>, IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly TestApiFactory<Program> _factory;

        public TestExternalSdkCity(WebApplicationFactory<Program> factory)
        {
            _factory = new TestApiFactory<Program>(factory);
        }

        protected override ISdk CreateData() => TestsExternalSdkHelpers.CreateSdk(_factory.CreateClient());

        protected override IDataGet<City, City, CriteriaCity> GetDataGet(ISdk sdk) => sdk.Cities;
    }
}