using System.Windows.Input;

namespace Deve.External.ClientApp.Maui
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
        public LoginViewModel(LoginPage page) 
            : base(page)
        {
        }
        #endregion

        #region Methods
        private async Task DoLogin()
        {
            if (Utils.SomeIsNullOrWhiteSpace(_username, _password))
            {
                //TODO: show error
                return;
            }

            IsBusy = true;
            try
            {
                var resLogin = await Api.Authenticate.Login(new UserCredentials(_username, _password));
                //TODO: check Login response
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

        #region Commands
        public ICommand Login => _loginCommand ??= new Command(() => _ = DoLogin());
        #endregion
    }
}