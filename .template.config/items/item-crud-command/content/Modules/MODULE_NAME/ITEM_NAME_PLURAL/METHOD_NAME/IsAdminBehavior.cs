namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAME;

internal sealed class IsAdminBehavior : MustBeAdminBehavior<Command, Result>
{
    public IsAdminBehavior(IDataOptions options, IUserIdentityService identityService)
        : base(options, identityService)
    {
    }
}
