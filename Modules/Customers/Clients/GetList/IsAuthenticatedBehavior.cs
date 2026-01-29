namespace Deve.Customers.Clients.GetList;

internal sealed class IsAuthenticatedBehavior : MustBeAuthenticatedBehavior<Query, ResultGetList<ClientListResponse>>
{
    public IsAuthenticatedBehavior(IDataOptions options, IUserIdentityService identityService)
        : base(options, identityService)
    {
    }
}
