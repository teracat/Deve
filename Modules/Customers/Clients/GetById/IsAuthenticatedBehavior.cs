namespace Deve.Customers.Clients.GetById;

internal sealed class IsAuthenticatedBehavior : MustBeAuthenticatedBehavior<Query, ResultGet<ClientResponse>>
{
    public IsAuthenticatedBehavior(IDataOptions options, IUserIdentityService identityService)
        : base(options, identityService)
    {
    }
}
