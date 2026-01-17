using Deve.Tests.Modules.Identity;
using Deve.Tests.Sdk.Fixtures;

namespace Deve.Tests.Sdk.Modules.Identity;

public class UserSdkTest : UserTest, IClassFixture<SdkFixture>
{
    public UserSdkTest(SdkFixture fixture)
        : base(fixture)
    {
    }
}
