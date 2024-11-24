using Microsoft.AspNetCore.Mvc.Testing;
using Deve.External.Api;

namespace Deve.Tests.Api.External
{
    /// <summary>
    /// External Api Auth endpoints tests.
    /// </summary>
    public class TestExternalApiAuth : TestApiBaseAuth<Program>
    {
        public TestExternalApiAuth(WebApplicationFactory<Program> factory)
            : base(factory)
        {
        }
    }
}