using Deve.Identity;
using Deve.Identity.Users;

namespace Deve.Core;

internal sealed class IdentityCore(IUserData users) : IIdentityData
{
    public IUserData Users => users;
}
