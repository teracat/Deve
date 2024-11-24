using Microsoft.AspNetCore.Mvc.Testing;
using Deve.Internal.Api;

namespace Deve.Tests.Api.Internal
{
    /// <summary>
    /// Internal Api Auth endpoints tests.
    /// </summary>
    public class TestInternalApiAuth : TestApiBaseAuth<Program>
    {
        public TestInternalApiAuth(WebApplicationFactory<Program> factory)
            : base(factory)
        {
        }
    }
}