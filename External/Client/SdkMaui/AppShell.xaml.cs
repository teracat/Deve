namespace Deve.External.ClientApp.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("clients/details", typeof(ClientDetailsPage));
        }

        private void LogoutClicked(object sender, EventArgs e) => ((App?)Application.Current)?.GoToLogin();
    }
}
