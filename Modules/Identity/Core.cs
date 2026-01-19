namespace Deve.Identity;

internal sealed class Core(IUserData users) : IIdentityData
{
    public IUserData Users => users;
}
