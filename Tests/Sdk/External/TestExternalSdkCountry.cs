using Microsoft.AspNetCore.Mvc.Testing;
using Deve.External;
using Deve.External.Api;
using Deve.External.Sdk;
using Deve.Tests.Api;

namespace Deve.Tests.Sdk.External
{
    public class TestExternalSdkCountry : TestBaseDataGet<ISdk, Country, Country, CriteriaCountry>, IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly TestApiFactory<Program> _factory;

        public TestExternalSdkCountry(WebApplicationFactory<Program> factory)
        {
            _factory = new TestApiFactory<Program>(factory);
        }

        protected override ISdk CreateData() => TestsExternalSdkHelpers.CreateSdk(_factory.CreateClient());

        protected override IDataGet<Country, Country, CriteriaCountry> GetDataGet(ISdk sdk) => sdk.Countries;
    }
}