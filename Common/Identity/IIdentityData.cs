// <hooks:data-using>
using Deve.Identity.Users;

namespace Deve.Identity;

public interface IIdentityData : IModule
{
    // <hooks:data-properties>

    IUserData Users { get; }
}
