using Deve.Authenticate;
using Deve.Clients.Maui.Helpers;
using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Resources.Strings;

namespace Deve.Clients.Maui.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields
        private string _username = string.Empty;
        private string _password = string.Empty;

        private AsyncCommand? _loginCommand;
        #endregion

        #region Properties
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        #endregion

        #region Constructor
        public LoginViewModel(INavigationService navigationService, IDataService dataService)
            : base(navigationService, dataService)
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

            if (Utils.SomeIsNullOrWhiteSpace(_username, _password))
            {
                ErrorText = AppResources.MissingUsernamePassword;
                return;
            }

            IsBusy = true;
            try
            {
                var resLogin = await DataService.Data.Authenticate.Login(new UserCredentials(_username, _password));
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
        public AsyncCommand Login => _loginCommand ??= new AsyncCommand(DoLogin, () => IsIdle);
        #endregion
    }
}