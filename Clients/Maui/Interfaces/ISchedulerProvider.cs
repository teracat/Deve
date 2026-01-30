using System.Reactive.Concurrency;

namespace Deve.Clients.Maui.Interfaces;

public interface ISchedulerProvider
{
    IScheduler MainThread { get; }
    IScheduler CurrentThread { get; }
    IScheduler TaskPool { get; }
}