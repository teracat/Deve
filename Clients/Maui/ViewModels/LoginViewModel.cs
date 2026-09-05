using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Resources.Strings;
using Deve.Auth.Login;

namespace Deve.Clients.Maui.ViewModels;

internal sealed partial class LoginViewModel : BaseViewModel
{
    #region Fields
    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingUsername))]
    private string _username;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingPassword))]
    private string _password;
    #endregion

    #region Constructor
    public LoginViewModel(INavigationService navigationService, Data.IData data)
        : base(navigationService, data)
    {
        //-:cnd
#if DEBUG
#pragma warning disable S2068 // Credentials should not be hard-coded
        Username = "teracat";
        Password = "teracat";
#pragma warning restore S2068 // Credentials should not be hard-coded
#else
        Username = string.Empty;
        Password = string.Empty;
#endif
        //+:cnd
    }
    #endregion

    #region Methods
    [RelayCommand(CanExecute = nameof(IsIdle))]
    internal async Task Login()
    {
        if (!Validate())
        {
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
}