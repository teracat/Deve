namespace Deve.Customers.Clients.UpdateStatus;

internal sealed class IsAdminBehavior : MustBeAdminBehavior<UpdateClientStatusCommand, Result>
{
    public IsAdminBehavior(IDataOptions options, IUserIdentityService identityService)
        : base(options, identityService)
    {
    }
}
