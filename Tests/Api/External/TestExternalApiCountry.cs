using Microsoft.AspNetCore.Mvc.Testing;
using Deve.External.Api;

namespace Deve.Tests.Api.External
{
    public class TestExternalApiCountry : TestExternalApiBaseGet
    {
        protected override string Path => ApiConstants.PathCountry;

        public TestExternalApiCountry(WebApplicationFactory<Program> factory)
            : base(factory)
        {
        }
    }
}