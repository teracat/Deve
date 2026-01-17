using Deve.Identity.Users;

namespace Deve.Identity;

public interface IIdentityData : IModule
{
    IUserData Users { get; }
}
