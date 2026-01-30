using Deve.Data;
// <hooks:core-main-using>
using Deve.Customers;
using Deve.Identity;
using Deve.Auth;

namespace Deve.Core;

internal sealed class MainCore(
    // <hooks:core-main-contructor>
    IAuthData dataAuth,
    ICustomersData dataCustomers,
    IIdentityData dataIdentity) : IData
{
    // <hooks:core-main-properties>

    public IAuthData Auth => dataAuth;

    public ICustomersData Customers => dataCustomers;

    public IIdentityData Identity => dataIdentity;

    public void Dispose()
    {
        // Nothing to dispose
    }
}
