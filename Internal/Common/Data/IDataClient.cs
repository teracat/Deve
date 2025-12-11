using Deve.Internal.Dto;
using Deve.Dto;

namespace Deve.Internal.Data
{
    public interface IDataClient : IDataAll<Client, Client, CriteriaClient>
    {
        Task<Result> UpdateStatus(long id, ClientStatus newStatus);
    }
}
