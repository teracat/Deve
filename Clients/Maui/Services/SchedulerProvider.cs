using System.Reactive.Concurrency;
using ReactiveUI;
using Deve.Clients.Maui.Interfaces;

namespace Deve.Clients.Maui.Services;

public sealed class SchedulerProvider : ISchedulerProvider
{
    public IScheduler MainThread => RxApp.MainThreadScheduler;
    public IScheduler CurrentThread => CurrentThreadScheduler.Instance;
    public IScheduler TaskPool => RxApp.TaskpoolScheduler;
}