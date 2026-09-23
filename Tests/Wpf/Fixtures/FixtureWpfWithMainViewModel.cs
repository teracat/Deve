using Deve.Clients.Wpf.ViewModels;

namespace Deve.Tests.Wpf.Fixtures;

public class FixtureWpfWithMainViewModel : FixtureWpf
{
    #region Properties
    internal MainViewModel? MainViewModel { get; private set; }

    internal TestSchedulers Schedulers { get; private set; } = new TestSchedulers();
    #endregion

    #region IAsyncLifetime
    public override async ValueTask InitializeAsync()
    {
        await base.InitializeAsync();

        MainViewModel = new MainViewModel(NavigationService.Object, DataAuthAdmin, MessageHandler.Object, Schedulers);
        await MainViewModel.Initialization;
    }

    public override async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        await base.DisposeAsync();
    }
    #endregion
}