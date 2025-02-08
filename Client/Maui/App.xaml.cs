using Deve.ClientApp.Maui.Interfaces;

namespace Deve.ClientApp.Maui
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