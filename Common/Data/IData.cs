using Deve.Auth;
using Deve.Customers;
using Deve.Identity;

namespace Deve.Data;

public interface IData : IDisposable
{
    IAuthData Auth { get; }

    ICustomersData Customers { get; }

    IIdentityData Identity { get; }
}
