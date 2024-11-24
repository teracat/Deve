using Microsoft.AspNetCore.Mvc.Testing;
using Deve.External.Api;

namespace Deve.Tests.Api.External
{
    /// <summary>
    /// External Api tests base class.
    /// </summary>
    public class TestExternalApiBase : TestApiBase<Program>
    {
        public TestExternalApiBase(WebApplicationFactory<Program> factory)
            : base(factory)
        {
        }
    }
}