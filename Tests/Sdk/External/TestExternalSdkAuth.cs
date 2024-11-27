using Microsoft.AspNetCore.Mvc.Testing;
using Deve.External.Api;
using Deve.External.Sdk;
using Deve.Tests.Api;

namespace Deve.Tests.Sdk.External
{
    public class TestExternalSdkAuth : TestAuthenticate<ISdk>, IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly TestApiFactory<Program> _factory;

        public TestExternalSdkAuth(WebApplicationFactory<Program> factory)
        {
            _factory = new TestApiFactory<Program>(factory);
        }

        protected override ISdk CreateData() => TestsExternalSdkHelpers.CreateSdk(_factory.CreateClient());
    }
}