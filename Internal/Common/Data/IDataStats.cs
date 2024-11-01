namespace Deve.Internal
{
    public interface IDataStats
    {
        Task<ResultGet<ClientStats>> GetClientStats();
    }
}