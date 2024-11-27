using Microsoft.AspNetCore.Mvc.Testing;
using Deve.Internal;
using Deve.Internal.Api;
using Deve.Internal.Sdk;
using Deve.Tests.Api;

namespace Deve.Tests.Sdk.Internal
{
    public class TestInternalSdkState : TestState<ISdk>, IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly TestApiFactory<Program> _factory;

        public TestInternalSdkState(WebApplicationFactory<Program> factory)
        {
            _factory = new TestApiFactory<Program>(factory);
        }

        protected override ISdk CreateData() => TestsInternalSdkHelpers.CreateSdk(_factory.CreateClient());

        protected override IDataAll<State, State, CriteriaState> GetDataAll(ISdk sdk) => sdk.States;
    }
}