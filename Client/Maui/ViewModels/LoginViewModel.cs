using System.Windows.Input;
using Deve.ClientApp.Maui.Resources.Strings;

namespace Deve.ClientApp.Maui.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields
        private string _username = string.Empty;
        private string _password = string.Empty;

        private ICommand? _loginCommand;
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
        public LoginViewModel()
        {
        }
        #endregion

        #region Overrides
//-:cnd
#if DEBUG
        public override void OnViewAppearing()
        {
            base.OnViewAppearing();
            Username = "teracat";
            Password = "teracat";
        }
#endif
//+:cnd
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
                var resLogin = await Globals.Data.Authenticate.Login(new UserCredentials(_username, _password));
                if (!resLogin.Success || resLogin.Data is null)
                {
                    ErrorText = Utils.ErrorsToString(resLogin.Errors);
                    return;
                }

                Globals.UserToken = resLogin.Data;

                ((App?)Application.Current)?.GoToMain();
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

        #region Commands
        public ICommand Login => _loginCommand ??= new Command(() => _ = DoLogin(), () => IsIdle);
        #endregion
    }
}