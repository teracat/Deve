namespace Deve.MODULE_NAME.FEATURE_PLURAL.GetById;

internal sealed class IsAdminBehavior : MustBeAdminBehavior<Query, ResultGet<FEATURE_SINGULARResponse>>
{
    public IsAdminBehavior(IDataOptions options, IUserIdentityService identityService)
        : base(options, identityService)
    {
    }
}
