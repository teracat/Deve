namespace Deve.MODULE_NAME.FEATURE_PLURAL.Update;

internal sealed class IsAdminBehavior : MustBeAdminBehavior<Command, Result>
{
    public IsAdminBehavior(IDataOptions options, IUserIdentityService identityService)
        : base(options, identityService)
    {
    }
}
