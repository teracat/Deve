using Deve.Internal.Data;

namespace Deve.Clients.Maui.Interfaces
{
    public interface IDataService : IDisposable
    {
        IData Data { get; }
    }
}