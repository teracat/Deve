using System.Globalization;
using Deve.Authenticate;
using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Resources.Strings;
using Deve.Clients.Wpf.Views;

namespace Deve.Clients.Wpf.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Fields
        private CultureInfo? _selectedLanguage;
        private string _username = string.Empty;
        private bool _remember = false;
        #endregion

        #region Properties
        public ILoginView? LoginView { get; set; }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value);
        }

        public List<CultureInfo> Languages
        {
            get;
            set => SetProperty(ref field, value);
        } = [new CultureInfo("en"), new CultureInfo("es-ES")];

        public CultureInfo? SelectedLanguage
        {
            get => _selectedLanguage;
            set
            {
                if (SetProperty(ref _selectedLanguage, value) && value is not null)
                {
                    Properties.Settings.Default.LangCode = value.LCID;
                    Properties.Settings.Default.Save();

                    LoginView?.ChangeCulture(value, _username);
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
        public LoginViewModel(INavigationService navigationService, Internal.Data.IData data, IMessageHandler messageHandler)
            : base(navigationService, data, messageHandler)
        {
            _selectedLanguage = Languages.FirstOrDefault(x => x.LCID == Thread.CurrentThread.CurrentCulture.LCID) ?? Languages.FirstOrDefault();
            _username = Properties.Settings.Default.Username;
            _remember = !string.IsNullOrEmpty(Properties.Settings.Default.Username);
        }
        #endregion

        #region Methods
        public async Task DoLogin(string password)
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
                var resLogin = await Data.Authenticate.Login(new UserCredentials(_username, password));
                if (!resLogin.Success || resLogin.Data is null)
                {
                    ErrorText = Utils.ErrorsToString(resLogin.Errors);
                    return;
                }

                Globals.UserToken = resLogin.Data;

                Properties.Settings.Default.Username = _remember ? _username : string.Empty;
                Properties.Settings.Default.Save();
            }
            finally
            {
                IsBusy = false;
            }

            // Go to MainWindow
            NavigationService.NavigateTo<MainView>();
            Close();
        }
        #endregion
    }
}