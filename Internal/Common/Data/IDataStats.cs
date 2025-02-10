using Deve.Model;
using Deve.Internal.Model;

namespace Deve.Internal.Data
{
    public interface IDataStats
    {
        Task<ResultGet<ClientStats>> GetClientStats();
    }
}