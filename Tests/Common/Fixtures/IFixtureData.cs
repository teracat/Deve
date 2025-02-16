using Deve.Auth.TokenManagers;
using Deve.Data;

namespace Deve.Tests
{
    public interface IFixtureData<out TDataType> where TDataType : IDataCommon
    {
        ITokenManager TokenManager { get; }
        TDataType DataNoAuth { get; }
        TDataType DataValidAuth { get; }
    }
}