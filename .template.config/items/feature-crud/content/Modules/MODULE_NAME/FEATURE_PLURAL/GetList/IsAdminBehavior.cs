namespace Deve.MODULE_NAME.FEATURE_PLURAL.GetList;

internal sealed class IsAdminBehavior : MustBeAdminBehavior<Query, ResultGetList<FEATURE_SINGULARResponse>>
{
    public IsAdminBehavior(IDataOptions options, IUserIdentityService identityService)
        : base(options, identityService)
    {
    }
}
