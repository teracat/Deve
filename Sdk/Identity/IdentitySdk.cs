using Deve.Identity;
using Deve.Identity.Users;

namespace Deve.Sdk.Identity;

internal class IdentitySdk(ISdk sdk) : IIdentityData
{
    public IUserData Users => field ??= new UserSdk(sdk);
}
