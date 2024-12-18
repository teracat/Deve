namespace Deve.External.ClientApp.Maui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            GoToLogin();
        }

        public void GoToMain()
        {
            MainPage = new AppShell();
        }

        public void GoToLogin()
        {
            MainPage = new LoginPage();
        }
    }
}
