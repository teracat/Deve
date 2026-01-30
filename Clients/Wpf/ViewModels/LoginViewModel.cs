using System.Globalization;
using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Resources.Strings;
using Deve.Clients.Wpf.Views;
using Deve.Auth.Login;

namespace Deve.Clients.Wpf.ViewModels;

internal sealed class LoginViewModel : BaseViewModel
{
    #region Fields
    private CultureInfo? _selectedLanguage;
    #endregion

    #region Properties
    public ILoginView? LoginView { get; set; }

    public string Username
    {
        get => field;
        set => SetProperty(ref field, value);
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

                LoginView?.ChangeCulture(value, Username);
            }
        }
    }

    public bool Remember
    {
        get => field;
        set => SetProperty(ref field, value);
    }
    #endregion

    #region Constructor
    public LoginViewModel(INavigationService navigationService, Data.IData data, IMessageHandler messageHandler)
        : base(navigationService, data, messageHandler)
    {
        _selectedLanguage = Languages.FirstOrDefault(x => x.LCID == Thread.CurrentThread.CurrentCulture.LCID) ?? Languages.FirstOrDefault();
        Username = Properties.Settings.Default.Username;
        Remember = !string.IsNullOrEmpty(Properties.Settings.Default.Username);
    }
    #endregion

    #region Methods
    public async Task DoLogin(string password)
    {
        ErrorText = string.Empty;

        if (Utils.SomeIsNullOrWhiteSpace(Username, password))
        {
            ErrorText = AppResources.MissingUsernamePassword;
            return;
        }

        IsBusy = true;
        try
        {
            var resLogin = await Data.Auth.Login(new LoginRequest(Username, password));
            if (!resLogin.Success || resLogin.Data is null)
            {
                ErrorText = Utils.ErrorsToString(resLogin.Errors);
                return;
            }

            Globals.LoggedUserData = resLogin.Data;

            Properties.Settings.Default.Username = Remember ? Username : string.Empty;
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
