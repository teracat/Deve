namespace Deve.MODULE_NAME.ITEM_NAME_PLURAL.Add;

internal sealed class IsAdminBehavior : MustBeAdminBehavior<Command, ResultGet<ResponseId>>
{
    public IsAdminBehavior(IDataOptions options, IUserIdentityService identityService)
        : base(options, identityService)
    {
    }
}
