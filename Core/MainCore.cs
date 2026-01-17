using Deve.Data;
using Deve.Customers;
using Deve.Identity;
using Deve.Auth;

namespace Deve.Core;

internal sealed class MainCore(
    IAuthData auth,
    ICustomersData customers,
    IIdentityData identity) : IData
{
    public IAuthData Auth => auth;

    public ICustomersData Customers => customers;

    public IIdentityData Identity => identity;

    public void Dispose()
    {
        // Nothing to dispose
    }
}
