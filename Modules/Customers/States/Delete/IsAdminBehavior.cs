namespace Deve.Customers.States.Delete;

internal sealed class IsAdminBehavior : MustBeAdminBehavior<Command, Result>
{
    public IsAdminBehavior(IDataOptions options, IUserIdentityService identityService)
        : base(options, identityService)
    {
    }
}
