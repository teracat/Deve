using Deve.ClientApp.Maui.ViewModels;

namespace Deve.ClientApp.Maui.Views
{
    public partial class LoginView : BaseView
    {
        private LoginViewModel _viewModel;

        public LoginView(LoginViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();

            ViewModel = _viewModel = viewModel;
        }

        private void uxUsername_Completed(object sender, EventArgs e)
        {
            uxPassword.Focus();
        }

        private void uxPassword_Completed(object sender, EventArgs e)
        {
            _ = _viewModel.DoLogin();
        }
    }
}