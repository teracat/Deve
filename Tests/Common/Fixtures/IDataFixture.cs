using Deve.Auth.TokenManagers;
using Deve.Data;

namespace Deve.Tests;

public interface IDataFixture<out TDataType> where TDataType : IData
{
    ITokenManager TokenManager { get; }
    TDataType DataNoAuth { get; }
    TDataType DataAuthUser { get; }
    TDataType DataAuthAdmin { get; }
}
