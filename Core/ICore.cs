using Deve.Auth;
using Deve.DataSource;
using Deve.Core.Shield;
using Deve.Internal.Data;
using Deve.Internal.Model;

namespace Deve.Core
{
    public interface ICore : IData
    {
        IDataSource DataSource { get; }
        IAuth Auth { get; }
        IShield Shield { get; }
    }
}
