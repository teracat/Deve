using Deve.Internal.Model;
using Deve.Model;

namespace Deve.Internal.Data
{
    public interface IDataStats
    {
        Task<ResultGet<ClientStats>> GetClientStats();
    }
}