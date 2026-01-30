namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.GetList;

internal sealed class IsAdminBehavior : MustBeAdminBehavior<Query, ResultGetList<ITEM_NAME_SINGULARResponse>>
{
    public IsAdminBehavior(IDataOptions options, IUserIdentityService identityService)
        : base(options, identityService)
    {
    }
}
