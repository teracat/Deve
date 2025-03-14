using System.Reactive.Linq;
using Deve.Clients.Maui.ViewModels;

namespace Deve.Clients.Maui.Views
{
    public partial class LoginView : BaseView
    {
        private readonly LoginViewModel _viewModel;

        public LoginView(LoginViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();

            ViewModel = _viewModel = viewModel;
        }

        private void OnUsernameCompleted(object sender, EventArgs e) => uxPassword.Focus();

        private void OnPasswordCompleted(object sender, EventArgs e) => _ = _viewModel.LoginCommand.Execute().FirstAsync();
    }
}