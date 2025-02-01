using Deve.ClientApp.Maui.Views;

namespace Deve.ClientApp.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("//clients/details", typeof(ClientDetailsView));
        }

        private void LogoutClicked(object sender, EventArgs e) => ((App?)Application.Current)?.GoToLogin();
    }
}
