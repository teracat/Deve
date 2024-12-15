namespace Deve.External.ClientApp.Maui
{
    public partial class LoginPage : BasePage
    {
        private LoginViewModel viewModel;

        public LoginPage()
        {
            InitializeComponent();

            ViewModel = viewModel = new LoginViewModel(this);
        }

        private void UsernamCompleted(object sender, EventArgs e)
        {
            uxPassword.Focus();
        }

        private void PasswordCompleted(object sender, EventArgs e)
        {
            _ = viewModel.DoLogin();
        }
    }
}
