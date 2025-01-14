using System.Globalization;
using System.Windows.Input;
using Deve.ClientApp.Wpf.Resources.Strings;
using Deve.ClientApp.Wpf.Window;

namespace Deve.ClientApp.Wpf.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields
        private LoginWindow _loginWindow;
        private List<CultureInfo> _languages = [new CultureInfo("en"), new CultureInfo("es-ES")];
        private CultureInfo? _selectedLanguage;
        private string _username = string.Empty;
        private bool _remember = false;

        private ICommand? _loginCommand;
        #endregion

        #region Properties
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public List<CultureInfo> Languages
        {
            get => _languages;
            set => SetProperty(ref _languages, value);
        }

        public CultureInfo? SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (SetProperty(ref _selectedLanguage, value) && value is not null)
                {
                    Properties.Settings.Default.LangCode = value.LCID;
                    Properties.Settings.Default.Save();

                    _loginWindow.ChangeCulture(value, _username);
                }
            }
        }

        public bool Remember
        {
            get => _remember;
            set => SetProperty(ref _remember, value);
        }
        #endregion

        #region Constructor
        public LoginViewModel(LoginWindow window, string? username = null)
            : base(window)
        {
            _loginWindow = window;
            _selectedLanguage = _languages.FirstOrDefault(x => x.LCID == Thread.CurrentThread.CurrentCulture.LCID) ?? _languages.FirstOrDefault();
            if (username is not null)
                _username = username;
            else
                _username = Properties.Settings.Default.Username;
            _remember = !string.IsNullOrEmpty(Properties.Settings.Default.Username);
        }
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

                Properties.Settings.Default.Username = _remember ? _username : string.Empty;
                Properties.Settings.Default.Save();

                // Go to MainWindow
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
