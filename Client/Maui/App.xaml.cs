using Deve.ClientApp.Maui.Views;

namespace Deve.ClientApp.Maui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new LoginView();
        }

        public void GoToMain()
        {
            Dispatcher.Dispatch(() =>
            {
                MainPage = new AppShell();
            });
        }

        public void GoToLogin()
        {
            Dispatcher.Dispatch(() =>
            {
                MainPage = new LoginView();
            });
        }
    }
}
