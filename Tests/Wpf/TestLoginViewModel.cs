using System.Reactive.Threading.Tasks;
using System.Security;
using Moq;
using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.ViewModels;
using Deve.Clients.Wpf.Views;
using Deve.Tests.Wpf.Fixtures;

namespace Deve.Tests.Wpf;

public class TestLoginViewModel : IClassFixture<FixtureWpf>
{
    private readonly FixtureWpf _fixture;

    public TestLoginViewModel(FixtureWpf fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Login_EmptyUsernamePassword_HasErrors()
    {
        var schedulerProvider = new TestSchedulers();
        using var loginViewModel = new LoginViewModel(_fixture.NavigationService.Object, _fixture.DataNoAuth, _fixture.MessageHandler.Object, schedulerProvider)
        {
            Username = string.Empty,
        };

        _ = await loginViewModel.LoginCommand.Execute(string.Empty).ToTask();

        Assert.True(loginViewModel.HasErrors);  // Validation errors uses the HasErrors property
    }

    private static SecureString SecureStringConverter(string pass)
    {
        SecureString ret = new SecureString();

        foreach (char chr in pass)
        {
            ret.AppendChar(chr);
        }

        return ret;
    }

    [Fact]
    public async Task Login_InvalidUsernamePassword_HasError()
    {
        var schedulerProvider = new TestSchedulers();
        using var loginViewModel = new LoginViewModel(_fixture.NavigationService.Object, _fixture.DataNoAuth, _fixture.MessageHandler.Object, schedulerProvider)
        {
            Username = TestsConstants.UserUsernameInactive,
            Password = SecureStringConverter(TestsConstants.UserPasswordInactive),  // To avoid Validation errors
        };

        _ = await loginViewModel.LoginCommand.Execute(TestsConstants.UserPasswordInactive).ToTask();

        Assert.True(loginViewModel.HasError);   // HasError is a custom property for other types of errors
    }

    [Fact]
    public async Task Login_ValidUsernamePassword_HasNoError()
    {
        var schedulerProvider = new TestSchedulers();
        using var loginViewModel = new LoginViewModel(_fixture.NavigationService.Object, _fixture.DataNoAuth, _fixture.MessageHandler.Object, schedulerProvider)
        {
            Username = TestsConstants.UserUsernameValid,
            Password = SecureStringConverter(TestsConstants.UserPasswordValid),  // To avoid Validation errors
        };

        _ = await loginViewModel.LoginCommand.Execute(TestsConstants.UserPasswordValid).ToTask();

        Assert.False(loginViewModel.HasError);
    }

    [Fact]
    public async Task Login_ValidUsernamePassword_NavigatesToClients()
    {
        // We use a new instance of Mock<IMessageHandler> so other tests does not interfere with this one
        var navigationService = new Mock<INavigationService>();
        var schedulerProvider = new TestSchedulers();
        using var loginViewModel = new LoginViewModel(navigationService.Object, _fixture.DataNoAuth, _fixture.MessageHandler.Object, schedulerProvider)
        {
            Username = TestsConstants.UserUsernameValid,
            Password = SecureStringConverter(TestsConstants.UserPasswordValid),  // To avoid Validation errors
        };

        _ = await loginViewModel.LoginCommand.Execute(TestsConstants.UserPasswordValid).ToTask();

        navigationService.Verify(x => x.NavigateTo<MainView>(), Times.Once);
    }
}
