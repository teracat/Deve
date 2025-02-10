using Deve.Clients.Maui.Interfaces;

namespace Deve.Clients.Maui
{
    public partial class App : Application
    {
        public App(INavigationService navigationService)
        {
            InitializeComponent();

            MainPage = new AppShell(navigationService);
        }
    }
}