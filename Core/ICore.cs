using Deve.Auth;
using Deve.Core.Shield;
using Deve.DataSource;
using Deve.Internal.Data;

namespace Deve.Core
{
    public interface ICore : IData
    {
        IDataSource DataSource { get; }
        IAuth Auth { get; }
        IShield Shield { get; }
    }
}
