using Deve.Auth;
using Deve.DataSource;
using Deve.Internal;

namespace Deve.Core
{
    public interface ICore : IData
    {
        IDataSource DataSource { get; }
        UserIdentity? UserIdentity { get; set; }
        User? User { get; set; }
        IShield Shield { get; }
    }
}
