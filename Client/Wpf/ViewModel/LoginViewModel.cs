using System.Windows.Input;
using Deve.ClientApp.Wpf.Resources.Strings;
using Deve.ClientApp.Wpf.Window;

namespace Deve.ClientApp.Wpf.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields
        private LoginWindow _loginWindow;
        private string _username = string.Empty;

        private ICommand? _loginCommand;
        #endregion

        #region Properties
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }
        #endregion

        #region Constructor
        public LoginViewModel(LoginWindow window)
            : base(window)
        {
            _loginWindow = window;
        }
        #endregion

        #region Overrides
#if DEBUG
        public override void OnWindowLoaded()
        {
            base.OnWindowLoaded();
            Username = "teracat";
        }
#endif
        #endregion

        #region Methods
        internal async Task DoLogin(string password)
        {
            ErrorText = string.Empty;

            if (Utils.SomeIsNullOrWhiteSpace(_username, password))
            {
                ErrorText = AppResources.MissingUsernamePassword;
                return;
            }

            IsBusy = true;
            try
            {
                var resLogin = await Globals.Data.Authenticate.Login(new UserCredentials(_username, password));
                if (!resLogin.Success || resLogin.Data is null)
                {
                    ErrorText = Utils.ErrorsToString(resLogin.Errors);
                    return;
                }

                Globals.UserToken = resLogin.Data;

                var mainWindow = new MainWindow();
                mainWindow.Show();
                Window.Close();
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

        #region Commands
        public ICommand Login => _loginCommand ??= new Command(() => _ = DoLogin(_loginWindow.uxPassword.Password), () => IsIdle);
        #endregion
    }
}
