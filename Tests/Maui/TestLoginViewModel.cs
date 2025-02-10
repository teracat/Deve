using Deve.Clients.Maui.ViewModels;
using Deve.Tests.Maui.Fixtures;
using Deve.Tests.Maui.Mocks;
using Moq;

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
            var loginViewModel = new LoginViewModel(_fixture.NavigationService.Object, _fixture.DataServiceNoAuth)
            {
                Username = string.Empty,
                Password = string.Empty
            };

            await loginViewModel.LoginCommand.ExecuteAsync(null);

            Assert.True(loginViewModel.HasError);
        }

        [Fact]
        public async Task Login_InvalidUsernamePassword_HasError()
        {
            var loginViewModel = new LoginViewModel(_fixture.NavigationService.Object, _fixture.DataServiceNoAuth)
            {
                Username = TestsConstants.UserUsernameInactive,
                Password = TestsConstants.UserPasswordInactive
            };

            await loginViewModel.LoginCommand.ExecuteAsync(null);

            Assert.True(loginViewModel.HasError);
        }

        [Fact]
        public async Task Login_ValidUsernamePassword_HasNoError()
        {
            var loginViewModel = new LoginViewModel(_fixture.NavigationService.Object, _fixture.DataServiceNoAuth)
            {
                Username = TestsConstants.UserUsernameValid,
                Password = TestsConstants.UserPasswordValid
            };

            await loginViewModel.LoginCommand.ExecuteAsync(null);

            Assert.False(loginViewModel.HasError);
        }

        [Fact]
        public async Task Login_ValidUsernamePassword_NavigatesToClients()
        {
            // We use a new instance of FixtureNavigationService so other tests does not interfere with this one
            var navigationService = new MockNavigationService();
            var loginViewModel = new LoginViewModel(navigationService.Object, _fixture.DataServiceNoAuth)
            {
                Username = TestsConstants.UserUsernameValid,
                Password = TestsConstants.UserPasswordValid
            };

            await loginViewModel.LoginCommand.ExecuteAsync(null);

            navigationService.Verify(x => x.NavigateToAsync("//clients", null), Times.Once);
        }
    }
}