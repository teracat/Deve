using Microsoft.AspNetCore.Mvc.Testing;
using Deve.External.Api;

namespace Deve.Tests.Api.External
{
    public class TestExternalApiCity : TestExternalApiBaseGet
    {
        protected override string Path => ApiConstants.PathCity;

        public TestExternalApiCity(WebApplicationFactory<Program> factory)
            : base(factory)
        {
        }
    }
}