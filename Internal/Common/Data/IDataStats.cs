using Deve.Internal.Dto;
using Deve.Dto;

namespace Deve.Internal.Data
{
    public interface IDataStats
    {
        Task<ResultGet<ClientStats>> GetClientStats();
    }
}