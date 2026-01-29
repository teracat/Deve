using System.Reactive.Concurrency;
using Deve.Clients.Wpf.Interfaces;

namespace Deve.Tests.Wpf.Fixtures;

/// <summary>
/// TestSchedulers is a class that implements the ISchedulerProvider interface for tests.
/// More info:
/// https://www.reactiveui.net/docs/handbook/testing.html
/// https://introtorx.com/chapters/testing-reactive-extensions-for-dotnet#TestingRx
/// </summary>
public class TestSchedulers : ISchedulerProvider
{
    public IScheduler MainThread => Scheduler.Immediate;

    public IScheduler CurrentThread => Scheduler.Immediate;

    public IScheduler TaskPool => Scheduler.Immediate;
}