using System.Reactive.Concurrency;
using ReactiveUI;
using Deve.Clients.Wpf.Interfaces;

namespace Deve.Clients.Wpf.Services
{
    public sealed class SchedulerProvider : ISchedulerProvider
    {
        public IScheduler MainThread => RxApp.MainThreadScheduler;
        public IScheduler CurrentThread => CurrentThreadScheduler.Instance;
        public IScheduler TaskPool => RxApp.TaskpoolScheduler;
    }
}