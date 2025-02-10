using Deve.Model;
using Deve.Internal.Criteria;
using Deve.Internal.Model;

namespace Deve.Internal.Data
{
    public interface IDataClient : IDataAll<Client, Client, CriteriaClient>
    {
        Task<Result> UpdateStatus(long id, ClientStatus newStatus);
    }
}
