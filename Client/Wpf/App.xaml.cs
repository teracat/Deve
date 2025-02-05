using System;
using System.Globalization;
using System.Windows;
using Deve.ClientApp.Wpf.Helpers;
using Deve.ClientApp.Wpf.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Deve.ClientApp.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider { get; set; }

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            ServiceProviderHelper.RegisterServices(services);
            ServiceProviderHelper.RegisterViewModels(services);
            ServiceProviderHelper.RegisterViews(services);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (Wpf.Properties.Settings.Default.LangCode > 0)
            {
                var culture = new CultureInfo(Wpf.Properties.Settings.Default.LangCode);
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }

            var loginView = serviceProvider.GetRequiredService<LoginView>();
            loginView.Show();
        }

        public void ChangeCulture(CultureInfo newCulture, string username, string password)
        {
            Thread.CurrentThread.CurrentCulture = newCulture;
            Thread.CurrentThread.CurrentUICulture = newCulture;

            var oldWindow = Current.MainWindow;

            var loginView = serviceProvider.GetRequiredService<LoginView>();
            loginView.SetUsernamePassword(username, password);
            loginView.Show();
            Current.MainWindow = loginView;

            oldWindow.Close();
        }
    }
}
