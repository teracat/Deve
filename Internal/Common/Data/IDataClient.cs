namespace Deve.Internal
{
    public interface IDataClient : IDataAll<Client, Client, CriteriaClient>
    {
        Task<Result> UpdateStatus(long id, ClientStatus newStatus);
    }
}
