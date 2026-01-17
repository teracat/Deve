namespace Deve.Identity.Users.GetList;

internal sealed class IsAdminBehavior : MustBeAdminBehavior<Query, ResultGetList<UserResponse>>
{
    public IsAdminBehavior(IDataOptions options, IUserIdentityService identityService)
        : base(options, identityService)
    {
    }
}
