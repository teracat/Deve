using System.Security;
using System.Reactive.Linq;
using Moq;
using Deve.Clients.Wpf.ViewModels;
using Deve.Clients.Wpf.Views;
using Deve.Clients.Wpf.Interfaces;
using Deve.Tests.Wpf.Fixtures;

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
        public void Login_EmptyUsernamePassword_HasErrors()
        {
            var schedulerProvider = new TestSchedulers();
            var loginViewModel = new LoginViewModel(_fixture.NavigationService.Object, _fixture.DataNoAuth, _fixture.MessageHandler.Object, schedulerProvider)
            {
                Username = string.Empty,
            };

            loginViewModel.LoginCommand.Execute(string.Empty).Subscribe(res =>
            {
                Assert.True(loginViewModel.HasErrors);  // Validation errors uses the HasErrors property
            });
        }

        private static SecureString SecureStringConverter(string pass)
        {
            SecureString ret = new SecureString();

            foreach (char chr in pass.ToCharArray())
            {
                ret.AppendChar(chr);
            }

            return ret;
        }

        [Fact]
        public void Login_InvalidUsernamePassword_HasError()
        {
            var schedulerProvider = new TestSchedulers();
            var loginViewModel = new LoginViewModel(_fixture.NavigationService.Object, _fixture.DataNoAuth, _fixture.MessageHandler.Object, schedulerProvider)
            {
                Username = TestsConstants.UserUsernameInactive,
                Password = SecureStringConverter(TestsConstants.UserPasswordInactive),  // To avoid Validation errors
            };

            loginViewModel.LoginCommand.Execute(TestsConstants.UserPasswordInactive).Subscribe(res =>
            {
                Assert.True(loginViewModel.HasError);   // HasError is a custom property for other types of errors
            });
        }

        [Fact]
        public void Login_ValidUsernamePassword_HasNoError()
        {
            var schedulerProvider = new TestSchedulers();
            var loginViewModel = new LoginViewModel(_fixture.NavigationService.Object, _fixture.DataNoAuth, _fixture.MessageHandler.Object, schedulerProvider)
            {
                Username = TestsConstants.UserUsernameValid,
                Password = SecureStringConverter(TestsConstants.UserPasswordValid),  // To avoid Validation errors
            };

            loginViewModel.LoginCommand.Execute(TestsConstants.UserPasswordValid).Subscribe(res =>
            {
                Assert.False(loginViewModel.HasError);
            });
        }

        [Fact]
        public void Login_ValidUsernamePassword_NavigatesToClients()
        {
            // We use a new instance of Mock<IMessageHandler> so other tests does not interfere with this one
            var navigationService = new Mock<INavigationService>();
            var schedulerProvider = new TestSchedulers();
            var loginViewModel = new LoginViewModel(navigationService.Object, _fixture.DataNoAuth, _fixture.MessageHandler.Object, schedulerProvider)
            {
                Username = TestsConstants.UserUsernameValid,
                Password = SecureStringConverter(TestsConstants.UserPasswordValid),  // To avoid Validation errors
            };

            loginViewModel.LoginCommand.Execute(TestsConstants.UserPasswordValid).Subscribe(res =>
            {
                navigationService.Verify(x => x.NavigateTo<MainView>(), Times.Once);
            });
        }
    }
}