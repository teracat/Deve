using Deve.ClientApp.Maui.ViewModels;

namespace Deve.ClientApp.Maui.Views
{
    public partial class LoginView : BaseView
    {
        private LoginViewModel viewModel;

        public LoginView()
        {
            InitializeComponent();

            ViewModel = viewModel = new LoginViewModel();
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
