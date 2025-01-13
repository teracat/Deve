using Deve.ClientApp.Wpf.ViewModel;

namespace Deve.ClientApp.Wpf.Window
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : BaseWindow
    {
        #region Fields
        private readonly LoginViewModel _viewModel;
        #endregion

        #region Constructor
        public LoginWindow()
        {
            InitializeComponent();

            ViewModel = _viewModel = new LoginViewModel(this);
        }
        #endregion

        #region Overrides
        protected override void OnWindowLoaded()
        {
            base.OnWindowLoaded();
#if DEBUG
            uxUsername.Text = "teracat";
            uxPassword.Password = "teracat";
#endif
            uxUsername.Focus();
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
