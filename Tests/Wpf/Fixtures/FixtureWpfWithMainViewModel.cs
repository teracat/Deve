using Deve.Clients.Wpf.ViewModels;

namespace Deve.Tests.Wpf.Fixtures;

public class FixtureWpfWithMainViewModel : FixtureWpf
{
    #region Properties
    internal MainViewModel? MainViewModel { get; private set; }
    #endregion

    #region IAsyncLifetime
    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();

        MainViewModel = new MainViewModel(NavigationService.Object, DataAuthAdmin, MessageHandler.Object);
        await MainViewModel.Initialization;
    }

    public override async Task DisposeAsync() => await base.DisposeAsync();
    #endregion
}
