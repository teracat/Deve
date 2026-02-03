namespace Deve.MODULE_NAME.FEATURE_PLURAL.METHOD_NAME;

internal sealed class IsAdminBehavior : MustBeAdminBehavior<Query, ResultGetList<FEATURE_SINGULARMETHOD_NAMEResponse>>
{
    public IsAdminBehavior(IDataOptions options, IUserIdentityService identityService)
        : base(options, identityService)
    {
    }
}
