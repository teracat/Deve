using Deve.Clients.Maui.Helpers;
using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Resources.Strings;
using Deve.Auth.Login;

namespace Deve.Clients.Maui.ViewModels;

internal sealed class LoginViewModel : BaseViewModel
{
    #region Properties
    public string Username
    {
        get;
        set => SetProperty(ref field, value);
    } = string.Empty;

    public string Password
    {
        get;
        set => SetProperty(ref field, value);
    } = string.Empty;
    #endregion

    #region Constructor
    public LoginViewModel(INavigationService navigationService, Data.IData data)
        : base(navigationService, data)
    {
//-:cnd
#if DEBUG
        Username = "teracat";
        Password = "teracat";
#endif
//+:cnd
    }
    #endregion

    #region Methods
    internal async Task DoLogin()
    {
        ErrorText = string.Empty;

        if (Utils.SomeIsNullOrWhiteSpace(Username, Password))
        {
            ErrorText = AppResources.MissingUsernamePassword;
            return;
        }

        IsBusy = true;
        try
        {
            var resLogin = await Data.Auth.Login(new LoginRequest(Username, Password));
            if (!resLogin.Success || resLogin.Data is null)
            {
                ErrorText = Utils.ErrorsToString(resLogin.Errors);
                return;
            }

            Globals.LoginResponseData = resLogin.Data;

            await NavigationService.NavigateToAsync("//clients");
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion

    #region Commands
    public AsyncCommand Login => field ??= new AsyncCommand(DoLogin, () => IsIdle);
    #endregion
}
