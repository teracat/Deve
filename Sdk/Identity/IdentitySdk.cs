using Deve.Identity;
// <hooks:sdk-module-using>
using Deve.Identity.Users;

namespace Deve.Sdk.Identity;

internal class IdentitySdk(ISdk sdk) : IIdentityData
{
    // <hooks:sdk-module-properties>

    public IUserData Users => field ??= new UserSdk(sdk);
}
