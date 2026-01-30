using Deve.Tests.Modules.Auth;
using Deve.Tests.Sdk.Fixtures;

namespace Deve.Tests.Sdk.Modules.Auth;

public class AuthSdkTest : AuthTest, IClassFixture<SdkFixture>
{
    public AuthSdkTest(SdkFixture fixture)
        : base(fixture)
    {
    }
}
