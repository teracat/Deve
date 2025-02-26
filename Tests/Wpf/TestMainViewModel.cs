using Moq;
using Deve.Model;
using Deve.Clients.Wpf.ViewModels;
using Deve.Clients.Wpf.Views;
using Deve.Clients.Wpf.Interfaces;
using Deve.Tests.Wpf.Fixtures;

namespace Deve.Tests.Wpf
{
    public class TestMainViewModel : IClassFixture<FixtureWpfWithMainViewModel>
    {
        #region Fields
        private readonly FixtureWpfWithMainViewModel _fixture;
        private readonly MainViewModel? _mainViewModel;
        #endregion

        #region Constructor
        public TestMainViewModel(FixtureWpfWithMainViewModel fixture)
        {
            _fixture = fixture;
            _mainViewModel = _fixture.MainViewModel;
        }
        #endregion

        #region Initialization
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
        #endregion

        #region State
        [Fact]
        public async Task AddState_NavigatesToStateView()
        {
            // We use a new instance of Mock<IMessageHandler> so other tests does not interfere with this one
            var navigationService = new Mock<INavigationService>();
            var mainViewModel = new MainViewModel(navigationService.Object, _fixture.DataValidAuth, _fixture.MessageHandler.Object);
            await mainViewModel.Initialization;

            await mainViewModel.AddStateCommand.ExecuteAsync(null);

            navigationService.Verify(x => x.NavigateModalTo<StateView>(), Times.Once);
        }

        [Fact]
        public async Task EditState_NavigatesToStateView()
        {
            // We use a new instance of Mock<IMessageHandler> so other tests does not interfere with this one
            var navigationService = new Mock<INavigationService>();
            var mainViewModel = new MainViewModel(navigationService.Object, _fixture.DataValidAuth, _fixture.MessageHandler.Object);
            await mainViewModel.Initialization;
            var state = mainViewModel.CtrlDataStates?.Items?.First();
            var stateId = state?.Id ?? 0;

            await mainViewModel.EditStateCommand.ExecuteAsync(state);

            navigationService.Verify(x => x.NavigateModalTo<StateView>(stateId), Times.Once);
        }

        [Fact]
        public async Task DeleteState_ShowQuestion()
        {
            // We use a new instance of Mock<IMessageHandler> so other tests does not interfere with this one
            var messageHandler = new Mock<IMessageHandler>();
            var mainViewModel = new MainViewModel(_fixture.NavigationService.Object, _fixture.DataValidAuth, messageHandler.Object);
            await mainViewModel.Initialization;
            var state = mainViewModel.CtrlDataStates?.Items?.First();

            await mainViewModel.DeleteStateCommand.ExecuteAsync(state);

            messageHandler.Verify(x => x.ShowQuestion(It.IsNotNull<string>(), It.IsNotNull<string>()), Times.Once);
        }
        #endregion

        #region Country
        [Fact]
        public async Task AddCountry_NavigatesToCountryView()
        {
            // We use a new instance of Mock<IMessageHandler> so other tests does not interfere with this one
            var navigationService = new Mock<INavigationService>();
            var mainViewModel = new MainViewModel(navigationService.Object, _fixture.DataValidAuth, _fixture.MessageHandler.Object);
            await mainViewModel.Initialization;

            await mainViewModel.AddCountryCommand.ExecuteAsync(null);

            navigationService.Verify(x => x.NavigateModalTo<CountryView>(), Times.Once);
        }

        [Fact]
        public async Task EditCountry_NavigatesToCountryView()
        {
            // We use a new instance of Mock<IMessageHandler> so other tests does not interfere with this one
            var navigationService = new Mock<INavigationService>();
            var mainViewModel = new MainViewModel(navigationService.Object, _fixture.DataValidAuth, _fixture.MessageHandler.Object);
            await mainViewModel.Initialization;
            var country = mainViewModel.CtrlDataCountries?.Items?.First();

            await mainViewModel.EditCountryCommand.ExecuteAsync(country);

            navigationService.Verify(x => x.NavigateModalTo<CountryView, Country>(It.IsNotNull<Country>()), Times.Once);
        }

        [Fact]
        public async Task DeleteCountry_ShowQuestion()
        {
            // We use a new instance of Mock<IMessageHandler> so other tests does not interfere with this one
            var messageHandler = new Mock<IMessageHandler>();
            var mainViewModel = new MainViewModel(_fixture.NavigationService.Object, _fixture.DataValidAuth, messageHandler.Object);
            await mainViewModel.Initialization;
            var country = mainViewModel.CtrlDataCountries?.Items?.First();

            await mainViewModel.DeleteCountryCommand.ExecuteAsync(country);

            messageHandler.Verify(x => x.ShowQuestion(It.IsNotNull<string>(), It.IsNotNull<string>()), Times.Once);
        }
        #endregion
    }
}