using Deve.Authenticate;
using Deve.Clients.Maui.Helpers;
using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Resources.Strings;

namespace Deve.Clients.Maui.ViewModels
{
    public class LoginViewModel : BaseViewModel
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
        public LoginViewModel(INavigationService navigationService, Internal.Data.IData data)
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
                var resLogin = await Data.Authenticate.Login(new UserCredentials(Username, Password));
                if (!resLogin.Success || resLogin.Data is null)
                {
                    ErrorText = Utils.ErrorsToString(resLogin.Errors);
                    return;
                }

                Globals.UserToken = resLogin.Data;

                await NavigationService.NavigateToAsync("//clients");
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

        #region Commands
        public AsyncCommand Login { get => field ??= new AsyncCommand(DoLogin, () => IsIdle); private set; }
        #endregion
    }
}