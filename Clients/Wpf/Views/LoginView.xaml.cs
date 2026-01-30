using System.Globalization;
using System.Windows;
using Deve.Clients.Wpf.ViewModels;

namespace Deve.Clients.Wpf.Views;

internal sealed partial class LoginView : BaseView, ILoginView
{
    #region Fields
    private readonly LoginViewModel _viewModel;
    #endregion

    #region Constructors
    public LoginView(LoginViewModel viewModel)
    {
        InitializeComponent();

        viewModel.LoginView = this;
        ViewModel = _viewModel = viewModel;

        //-:cnd
#if DEBUG
        if (string.IsNullOrEmpty(Properties.Settings.Default.Username))
        {
            SetUsernamePassword("teracat", "teracat");
        }
#endif
        //+:cnd
    }
    #endregion

    #region Methods
    public void SetUsernamePassword(string username, string password)
    {
        _viewModel.Username = username;
        uxPassword.Password = password;
    }
    #endregion

    #region Overrides
    protected override void OnWindowLoaded()
    {
        base.OnWindowLoaded();
        if (string.IsNullOrEmpty(uxUsername.Text))
        {
            _ = uxUsername.Focus();
        }
        else
        {
            _ = uxPassword.Focus();
        }
    }
    #endregion

    #region IViewLogin
    public void ChangeCulture(CultureInfo value, string username) => ((App)Application.Current).ChangeCulture(value, username, uxPassword.Password);
    #endregion

    #region Events
    private void OnUsernameKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        if (e.Key == System.Windows.Input.Key.Return)
        {
            _ = uxPassword.Focus();
        }
    }

    private void OnPasswordKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
    {
        // The Password property is not a dependency property for security reasons.
        if (e.Key == System.Windows.Input.Key.Return)
        {
            _ = _viewModel.DoLogin(uxPassword.Password);
        }
    }

    private void OnLoginClick(object sender, RoutedEventArgs e) => _ = _viewModel.DoLogin(uxPassword.Password);
    #endregion
}