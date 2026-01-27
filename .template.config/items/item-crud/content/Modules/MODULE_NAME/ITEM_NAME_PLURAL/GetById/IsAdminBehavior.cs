namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.GetById;

internal sealed class IsAdminBehavior : MustBeAdminBehavior<Query, ResultGet<ITEM_NAME_SINGULARResponse>>
{
    public IsAdminBehavior(IDataOptions options, IUserIdentityService identityService)
        : base(options, identityService)
    {
    }
}
