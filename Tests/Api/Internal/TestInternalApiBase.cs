using Microsoft.AspNetCore.Mvc.Testing;
using Deve.Internal.Api;

namespace Deve.Tests.Api.Internal
{
    /// <summary>
    /// Internal Api tests base class.
    /// </summary>
    public class TestInternalApiBase : TestApiBase<Program>
    {
        public TestInternalApiBase(WebApplicationFactory<Program> factory)
            : base(factory)
        {
        }
    }
}