using Microsoft.AspNetCore.Mvc.Testing;
using Deve.Internal.Api;
using Deve.Internal.Sdk;
using Deve.Tests.Api;

namespace Deve.Tests.Sdk.Internal
{
    public class TestInternalSdkClient : TestClient, IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly TestApiFactory<Program> _factory;

        public TestInternalSdkClient(WebApplicationFactory<Program> factory)
        {
            _factory = new TestApiFactory<Program>(factory);
        }

        protected override ISdk CreateData() => TestsInternalSdkHelpers.CreateSdk(_factory.CreateClient());
    }
}