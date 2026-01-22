// <hooks:core-using>

namespace Deve.Identity;

internal sealed class Core(
    // <hooks:core-contructor>
    IUserData dataUser) : IIdentityData
{
    // <hooks:core-properties>

    public IUserData Users => dataUser;
}
