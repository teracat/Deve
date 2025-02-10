using Moq;
using Deve.Clients.Wpf.ViewModels;
using Deve.Clients.Wpf.Views;
using Deve.Tests.Wpf.Fixtures;
using Deve.Tests.Wpf.Mocks;

namespace Deve.Tests.Wpf
{
    public class TestLoginViewModel : IClassFixture<FixtureWpf>
    {
        private readonly FixtureWpf _fixture;

        public TestLoginViewModel(FixtureWpf fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Login_EmptyUsernamePassword_HasError()
        {
            var loginViewModel = new LoginViewModel(_fixture.NavigationService.Object, _fixture.DataServiceNoAuth)
            {
                Username = string.Empty,
            };

            await loginViewModel.DoLogin(string.Empty);

            Assert.True(loginViewModel.HasError);
        }

        [Fact]
        public async Task Login_InvalidUsernamePassword_HasError()
        {
            var loginViewModel = new LoginViewModel(_fixture.NavigationService.Object, _fixture.DataServiceNoAuth)
            {
                Username = TestsConstants.UserUsernameInactive,
            };

            await loginViewModel.DoLogin(TestsConstants.UserPasswordInactive);

            Assert.True(loginViewModel.HasError);
        }

        [Fact]
        public async Task Login_ValidUsernamePassword_HasNoError()
        {
            var loginViewModel = new LoginViewModel(_fixture.NavigationService.Object, _fixture.DataServiceNoAuth)
            {
                Username = TestsConstants.UserUsernameValid,
            };

            await loginViewModel.DoLogin(TestsConstants.UserPasswordValid);

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
            };

            await loginViewModel.DoLogin(TestsConstants.UserPasswordValid);

            navigationService.Verify(x => x.NavigateTo<MainView>(null), Times.Once);
        }
    }
}