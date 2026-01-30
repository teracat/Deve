using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Security;
using CommunityToolkit.Mvvm.ComponentModel;
using Deve.Auth.Login;
using Deve.Clients.Wpf.Helpers;
using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Resources.Strings;
using Deve.Clients.Wpf.Views;

namespace Deve.Clients.Wpf.ViewModels;

internal sealed partial class LoginViewModel : BaseViewModel
{
    #region Fields
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
    #endregion

    #region Properties
    public ILoginView? LoginView { get; set; }

    [SecurePasswordValidation(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingPassword))]
    public SecureString Password
    {
        get;
        set
        {
            _ = SetProperty(ref field, value);
            ValidateProperty(value);
            OnPropertyChanged(nameof(HasErrorPassword));
        }
    } = new();

    public bool HasErrorPassword => GetErrors(nameof(Password)).Any();
    #endregion

    #region OnPropertyChanged
    partial void OnSelectedLanguageChanged(CultureInfo? value)
    {
        if (value is not null)
        {
            Properties.Settings.Default.LangCode = value.LCID;
            Properties.Settings.Default.Save();

            LoginView?.ChangeCulture(value, Username);
        }
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

    #region Overrides
    protected override bool Validate()
    {
        bool res = base.Validate();
        OnPropertyChanged(nameof(HasErrorPassword));
        return res;
    }
    #endregion

    #region Methods
    public async Task Login(string password)
    {
        if (!Validate())
        {
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
