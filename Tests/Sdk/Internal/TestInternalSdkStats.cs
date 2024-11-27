using Microsoft.AspNetCore.Mvc.Testing;
using Deve.Internal;
using Deve.Internal.Api;
using Deve.Tests.Api;

namespace Deve.Tests.Sdk.Internal
{
    public class TestInternalSdkStats : TestStats, IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly TestApiFactory<Program> _factory;

        public TestInternalSdkStats(WebApplicationFactory<Program> factory)
        {
            _factory = new TestApiFactory<Program>(factory);
        }

        protected override IData CreateData() => TestsInternalSdkHelpers.CreateSdk(_factory.CreateClient());
    }
}