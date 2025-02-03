using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security;
using CommunityToolkit.Mvvm.ComponentModel;
using Deve.ClientApp.Wpf.Helpers;
using Deve.ClientApp.Wpf.Interfaces;
using Deve.ClientApp.Wpf.Resources.Strings;
using Deve.ClientApp.Wpf.Views;

namespace Deve.ClientApp.Wpf.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        #region Fields
        private ILoginView _loginView;

        [ObservableProperty]
        private List<CultureInfo> _languages = [new CultureInfo("en"), new CultureInfo("es-ES")];

        [ObservableProperty]
        private CultureInfo? _selectedLanguage;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingUsername))]
        private string _username = string.Empty;

        [ObservableProperty]
        private bool _remember = false;

        private SecureString _password = new();
        #endregion

        #region Properties
        [SecurePasswordValidation(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingPassword))]
        public SecureString Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                ValidateProperty(value);
                OnPropertyChanged(nameof(HasErrorPassword));
            }
        }

        public bool HasErrorPassword => GetErrors(nameof(Password)).Any();
        #endregion

        #region OnPropertyChanged
        partial void OnSelectedLanguageChanged(CultureInfo? value)
        {
            if (value is not null)
            {
                Properties.Settings.Default.LangCode = value.LCID;
                Properties.Settings.Default.Save();

                _loginView?.ChangeCulture(value, Username);
            }
        }
        #endregion

        #region Constructor
        public LoginViewModel(ILoginView loginView, string? username = null)
        {
            _loginView = loginView;
            _selectedLanguage = _languages.FirstOrDefault(x => x.LCID == Thread.CurrentThread.CurrentCulture.LCID) ?? _languages.FirstOrDefault();
            if (username is not null)
                _username = username;
            else
                _username = Properties.Settings.Default.Username;
            _remember = !string.IsNullOrEmpty(Properties.Settings.Default.Username);
        }
        #endregion

        #region Overrides
        protected override bool Validate()
        {
            bool res = base.Validate();
            OnPropertyChanged(nameof(HasErrorPassword));
            return res;
        }
        #endregion

        #region Methods
        internal async Task Login(string password)
        {
            if (!Validate())
                return;

            IsBusy = true;
            try
            {
                var resLogin = await Globals.Data.Authenticate.Login(new UserCredentials(Username, password));
                if (!resLogin.Success || resLogin.Data is null)
                {
                    ErrorText = Utils.ErrorsToString(resLogin.Errors);
                    return;
                }

                Globals.UserToken = resLogin.Data;

                Properties.Settings.Default.Username = Remember ? Username : string.Empty;
                Properties.Settings.Default.Save();
            }
            finally
            {
                IsBusy = false;
            }

            // Go to MainWindow
            var mainView = new MainView();
            mainView.Show();
            Close();
        }
        #endregion
    }
}