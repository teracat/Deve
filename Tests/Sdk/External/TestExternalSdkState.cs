using Microsoft.AspNetCore.Mvc.Testing;
using Deve.External;
using Deve.External.Api;
using Deve.External.Sdk;
using Deve.Tests.Api;

namespace Deve.Tests.Sdk.External
{
    public class TestExternalSdkState : TestBaseDataGet<ISdk, State, State, CriteriaState>, IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly TestApiFactory<Program> _factory;

        public TestExternalSdkState(WebApplicationFactory<Program> factory)
        {
            _factory = new TestApiFactory<Program>(factory);
        }

        protected override ISdk CreateData() => TestsExternalSdkHelpers.CreateSdk(_factory.CreateClient());

        protected override IDataGet<State, State, CriteriaState> GetDataGet(ISdk sdk) => sdk.States;
    }
}