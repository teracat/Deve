using System.Windows.Input;
using Deve.External.ClientApp.Maui.Resources.Strings;

namespace Deve.External.ClientApp.Maui
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields
        private string _username = string.Empty;
        private string _password = string.Empty;
        private string _errorText = string.Empty;

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

        public string ErrorText
        {
            get => _errorText;
            set
            {
                if (SetProperty(ref _errorText, value))
                    OnPropertyChanged(nameof(HasError));
            }
        }

        public bool HasError => !string.IsNullOrWhiteSpace(_errorText);
        #endregion

        #region Constructor
        public LoginViewModel(LoginPage page) 
            : base(page)
        {
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
                var resLogin = await Api.Authenticate.Login(new UserCredentials(_username, _password));
                if (!resLogin.Success)
                {
                    ErrorText = Utils.ErrorsToString(resLogin.Errors);
                    return;
                }
                //TODO: go to main page
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