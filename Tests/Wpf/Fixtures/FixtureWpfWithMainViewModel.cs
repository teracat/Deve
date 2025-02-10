using Deve.Clients.Wpf.ViewModels;

namespace Deve.Tests.Wpf.Fixtures
{
    public class FixtureWpfWithMainViewModel : FixtureWpf
    {
        public MainViewModel MainViewModel { get; private set; }

        public FixtureWpfWithMainViewModel()
            : base()
        {
            MainViewModel = new MainViewModel(NavigationService.Object, DataServiceValidAuth);
        }
    }
}