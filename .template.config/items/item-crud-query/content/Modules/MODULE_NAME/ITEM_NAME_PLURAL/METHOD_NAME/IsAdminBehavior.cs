namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.METHOD_NAME;

internal sealed class IsAdminBehavior : MustBeAdminBehavior<Query, ResultGet<ITEM_NAME_SINGULARMETHOD_NAMEResponse>>
{
    public IsAdminBehavior(IDataOptions options, IUserIdentityService identityService)
        : base(options, identityService)
    {
    }
}
