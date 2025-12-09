using Moq;
using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.ViewModels;
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
            var loginViewModel = new LoginViewModel(_fixture.NavigationService.Object, _fixture.DataNoAuth)
            {
                Username = string.Empty,
                Password = string.Empty
            };

            await loginViewModel.Login.ExecuteAsync(null);

            Assert.True(loginViewModel.HasError);
        }

        [Fact]
        public async Task Login_InvalidUsernamePassword_HasError()
        {
            var loginViewModel = new LoginViewModel(_fixture.NavigationService.Object, _fixture.DataNoAuth)
            {
                Username = TestsConstants.UserUsernameInactive,
                Password = TestsConstants.UserPasswordInactive
            };

            await loginViewModel.Login.ExecuteAsync(null);

            Assert.True(loginViewModel.HasError);
        }

        [Fact]
        public async Task Login_ValidUsernamePassword_HasNoError()
        {
            var loginViewModel = new LoginViewModel(_fixture.NavigationService.Object, _fixture.DataNoAuth)
            {
                Username = TestsConstants.UserUsernameValid,
                Password = TestsConstants.UserPasswordValid
            };

            await loginViewModel.Login.ExecuteAsync(null);

            Assert.False(loginViewModel.HasError);
        }

        [Fact]
        public async Task Login_ValidUsernamePassword_NavigatesToClients()
        {
            // We use a new instance so other tests does not interfere with this one
            var navigationService = new Mock<INavigationService>();
            var loginViewModel = new LoginViewModel(navigationService.Object, _fixture.DataNoAuth)
            {
                Username = TestsConstants.UserUsernameValid,
                Password = TestsConstants.UserPasswordValid
            };

            await loginViewModel.Login.ExecuteAsync(null);

            navigationService.Verify(x => x.NavigateToAsync("//clients"), Times.Once);
        }
    }
}