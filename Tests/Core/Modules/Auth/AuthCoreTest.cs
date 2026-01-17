using Deve.Tests.Core.Fixtures;
using Deve.Tests.Modules.Auth;

namespace Deve.Tests.Core.Modules.Auth;

/// <summary>
/// Authenticate Tests for Core.
/// </summary>
public class AuthCoreTest : AuthTest, IClassFixture<CoreFixture>
{
    public AuthCoreTest(CoreFixture fixtureDataCore)
        : base(fixtureDataCore)
    {
    }
}
