using System.Globalization;
using Deve.ClientApp.Wpf.ViewModel;

namespace Deve.ClientApp.Wpf.Window
{
    public partial class LoginWindow : BaseWindow
    {
        #region Fields
        private readonly LoginViewModel _viewModel;
        #endregion

        #region Constructors
        public LoginWindow()
        {
            InitializeComponent();
            string? username = null;
//-:cnd
#if DEBUG
            if (string.IsNullOrEmpty(Properties.Settings.Default.Username))
            {
                username = "teracat";
                uxPassword.Password = "teracat";
            }
#endif
//+:cnd
            ViewModel = _viewModel = new LoginViewModel(this, username);
        }

        public LoginWindow(string? username, string? password)
        {
            InitializeComponent();

            uxPassword.Password = password;

            ViewModel = _viewModel = new LoginViewModel(this, username);
        }
        #endregion

        #region Overrides
        protected override void OnWindowLoaded()
        {
            base.OnWindowLoaded();

            if (string.IsNullOrEmpty(uxUsername.Text))
                uxUsername.Focus();
            else
                uxPassword.Focus();
        }
        #endregion

        #region Methods
        internal void ChangeCulture(CultureInfo value, string username)
        {
            App.ChangeCulture(value, username, uxPassword.Password);
        }
        #endregion

        #region Events
        private void OnUsernameKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Return)
                uxPassword.Focus();
        }

        private void OnPasswordKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            // The Password property is not a dependency property for security reasons.
            if (e.Key == System.Windows.Input.Key.Return)
                _ = _viewModel.DoLogin(uxPassword.Password);
        }
        #endregion
    }
}
