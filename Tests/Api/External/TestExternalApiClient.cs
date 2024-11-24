using Microsoft.AspNetCore.Mvc.Testing;
using Deve.External.Api;

namespace Deve.Tests.Api.External
{
    public class TestExternalApiClient : TestExternalApiBaseGet
    {
        protected override string Path => ApiConstants.PathClient;

        public TestExternalApiClient(WebApplicationFactory<Program> factory)
            : base(factory)
        {
        }
    }
}