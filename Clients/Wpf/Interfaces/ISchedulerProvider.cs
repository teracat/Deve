using System.Reactive.Concurrency;

namespace Deve.Clients.Wpf.Interfaces
{
    public interface ISchedulerProvider
    {
        IScheduler MainThread { get; }
        IScheduler CurrentThread { get; }
        IScheduler TaskPool { get; }
    }
}
