using Deve.Clients.Wpf.ViewModels;

namespace Deve.Tests.Wpf.Fixtures
{
    public class FixtureWpfWithMainViewModel : FixtureWpf
    {
        #region Properties
        public MainViewModel? MainViewModel { get; private set; }

        public TestSchedulers Schedulers { get; private set; }
        #endregion

        #region IAsyncLifetime
        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();

            Schedulers = new TestSchedulers();
            MainViewModel = new MainViewModel(NavigationService.Object, DataValidAuth, MessageHandler.Object, Schedulers);
            await MainViewModel.Initialization;
        }

        public override async Task DisposeAsync()
        {
            await base.DisposeAsync();
        }
        #endregion
    }
}