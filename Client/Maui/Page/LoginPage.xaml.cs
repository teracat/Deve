namespace Deve.ClientApp.Maui
{
    public partial class LoginPage : BasePage
    {
        private LoginViewModel viewModel;

        public LoginPage()
        {
            InitializeComponent();

            ViewModel = viewModel = new LoginViewModel(this);
        }

        private void uxUsername_Completed(object sender, EventArgs e)
        {
            uxPassword.Focus();
        }

        private void uxPassword_Completed(object sender, EventArgs e)
        {
            _ = viewModel.DoLogin();
        }
    }
}
