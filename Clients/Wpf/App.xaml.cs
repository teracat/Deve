using System.Globalization;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Deve.Clients.Wpf.Helpers;
using Deve.Clients.Wpf.Views;

namespace Deve.Clients.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Fields
        private readonly ServiceProvider _serviceProvider;
        #endregion

        #region Constructor
        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }
        #endregion

        #region Lifecycle Methods
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (Wpf.Properties.Settings.Default.LangCode > 0)
            {
                var culture = new CultureInfo(Wpf.Properties.Settings.Default.LangCode);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }

            var loginView = _serviceProvider.GetRequiredService<LoginView>();
            loginView.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _serviceProvider.Dispose();
            base.OnExit(e);
        }
        #endregion

        #region Methods
        private void ConfigureServices(ServiceCollection services)
        {
            ServiceProviderHelper.RegisterServices(services);
            ServiceProviderHelper.RegisterViewModels(services);
            ServiceProviderHelper.RegisterViews(services);
        }

        public void ChangeCulture(CultureInfo newCulture, string username, string password)
        {
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;

            var oldWindow = Current.MainWindow;

            var loginView = _serviceProvider.GetRequiredService<LoginView>();
            loginView.SetUsernamePassword(username, password);
            loginView.Show();
            Current.MainWindow = loginView;

            oldWindow.Close();
        }
        #endregion
    }
}
