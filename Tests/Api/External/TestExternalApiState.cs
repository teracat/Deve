using Microsoft.AspNetCore.Mvc.Testing;
using Deve.External.Api;

namespace Deve.Tests.Api.External
{
    public class TestExternalApiState : TestExternalApiBaseGet
    {
        protected override string Path => ApiConstants.PathState;

        public TestExternalApiState(WebApplicationFactory<Program> factory)
            : base(factory)
        {
        }
    }
}