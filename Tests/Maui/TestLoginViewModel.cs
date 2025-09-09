using System.Reactive.Threading.Tasks;
using Moq;
using Deve.Clients.Maui.ViewModels;
using Deve.Clients.Maui.Interfaces;
using Deve.Tests.Maui.Fixtures;

namespace Deve.Tests.Maui
{
    public class TestLoginViewModel : IClassFixture<FixtureMaui>
    {
        private readonly FixtureMaui _fixture;

        public TestLoginViewModel(FixtureMaui fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Login_EmptyUsernamePassword_HasError()
        {
            var schedulerProvider = new TestSchedulers();
            var loginViewModel = new LoginViewModel(_fixture.NavigationService.Object, _fixture.DataNoAuth, schedulerProvider)
            {
                Username = string.Empty,
                Password = string.Empty
            };

            await loginViewModel.LoginCommand.Execute().ToTask();

            Assert.True(loginViewModel.HasError);
        }

        [Fact]
        public async Task Login_InvalidUsernamePassword_HasError()
        {
            var schedulerProvider = new TestSchedulers();
            var loginViewModel = new LoginViewModel(_fixture.NavigationService.Object, _fixture.DataNoAuth, schedulerProvider)
            {
                Username = TestsConstants.UserUsernameInactive,
                Password = TestsConstants.UserPasswordInactive
            };

            await loginViewModel.LoginCommand.Execute().ToTask();

            Assert.True(loginViewModel.HasError);
        }

        [Fact]
        public async Task Login_ValidUsernamePassword_HasNoError()
        {
            var schedulerProvider = new TestSchedulers();
            var loginViewModel = new LoginViewModel(_fixture.NavigationService.Object, _fixture.DataNoAuth, schedulerProvider)
            {
                Username = TestsConstants.UserUsernameValid,
                Password = TestsConstants.UserPasswordValid
            };

            await loginViewModel.LoginCommand.Execute().ToTask();

            Assert.False(loginViewModel.HasError);
        }

        [Fact]
        public async Task Login_ValidUsernamePassword_NavigatesToClients()
        {
            // We use a new instance so other tests does not interfere with this one
            var navigationService = new Mock<INavigationService>();
            var schedulerProvider = new TestSchedulers();
            var loginViewModel = new LoginViewModel(navigationService.Object, _fixture.DataNoAuth, schedulerProvider)
            {
                Username = TestsConstants.UserUsernameValid,
                Password = TestsConstants.UserPasswordValid
            };

            await loginViewModel.LoginCommand.Execute().ToTask();

            navigationService.Verify(x => x.NavigateToAsync("//clients"), Times.Once);
        }
    }
}