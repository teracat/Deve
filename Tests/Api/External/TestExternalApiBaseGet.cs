using Microsoft.AspNetCore.Mvc.Testing;
using Deve.External.Api;

namespace Deve.Tests.Api.External
{
    public abstract class TestExternalApiBaseGet : TestApiBaseGet<Program>
    {
        public TestExternalApiBaseGet(WebApplicationFactory<Program> factory)
            : base(factory)
        {
        }
    }
}