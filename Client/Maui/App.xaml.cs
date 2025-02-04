using Deve.ClientApp.Maui.Views;

namespace Deve.ClientApp.Maui
{
    public partial class App : Application
    {
        IServiceProvider _serviceProvider;

        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            _serviceProvider = serviceProvider;

            MainPage = _serviceProvider.GetRequiredService<LoginView>();
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
                MainPage = _serviceProvider.GetRequiredService<LoginView>();
            });
        }
    }
}
