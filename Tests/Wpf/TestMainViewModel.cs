using Deve.Clients.Wpf.ViewModels;
using Deve.Tests.Wpf.Fixtures;

namespace Deve.Tests.Wpf
{
    public class TestMainViewModel : IClassFixture<FixtureWpfWithMainViewModel>
    {
        private readonly FixtureWpfWithMainViewModel _fixture;
        private readonly MainViewModel? _mainViewModel;

        public TestMainViewModel(FixtureWpfWithMainViewModel fixture)
        {
            _fixture = fixture;
            _mainViewModel = _fixture.MainViewModel;
        }

        [Fact]
        public void Initialization_Clients_NotNullOrEmpty()
        {
            Assert.NotNull(_mainViewModel?.CtrlDataClients?.Items);
            Assert.NotEmpty(_mainViewModel.CtrlDataClients.Items);
        }

        [Fact]
        public void Initialization_Cities_NotNullOrEmpty()
        {
            Assert.NotNull(_mainViewModel?.CtrlDataCities?.Items);
            Assert.NotEmpty(_mainViewModel.CtrlDataCities.Items);
        }

        [Fact]
        public void Initialization_States_NotNullOrEmpty()
        {
            Assert.NotNull(_mainViewModel?.CtrlDataStates?.Items);
            Assert.NotEmpty(_mainViewModel.CtrlDataStates.Items);
        }

        [Fact]
        public void Initialization_Countries_NotNullOrEmpty()
        {
            Assert.NotNull(_mainViewModel?.CtrlDataCountries?.Items);
            Assert.NotEmpty(_mainViewModel.CtrlDataCountries.Items);
        }
    }
}