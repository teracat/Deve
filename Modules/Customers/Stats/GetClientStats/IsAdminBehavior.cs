namespace Deve.Customers.Stats.GetClientStats;

internal sealed class IsAdminBehavior : MustBeAdminBehavior<Query, ResultGet<ClientStatsResponse>>
{
    public IsAdminBehavior(IDataOptions options, IUserIdentityService identityService)
        : base(options, identityService)
    {
    }
}
