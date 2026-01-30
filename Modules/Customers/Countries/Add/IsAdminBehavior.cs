namespace Deve.Customers.Countries.Add;

internal sealed class IsAdminBehavior : MustBeAdminBehavior<Command, ResultGet<ResponseId>>
{
    public IsAdminBehavior(IDataOptions options, IUserIdentityService identityService)
        : base(options, identityService)
    {
    }
}
