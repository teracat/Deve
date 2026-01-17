using System.Globalization;
using System.Windows;
using System.Windows.Threading;
using Microsoft.Extensions.DependencyInjection;
using Deve.Clients.Wpf.Helpers;
using Deve.Clients.Wpf.Views;
using Deve.Logging;

namespace Deve.Clients.Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
internal sealed partial class App : Application
{
    #region Fields
    private ServiceProvider _serviceProvider;
    #endregion

    #region Constructor
    public App()
    {
        if (Wpf.Properties.Settings.Default.LangCode > 0)
        {
            var culture = new CultureInfo(Wpf.Properties.Settings.Default.LangCode);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
        _serviceProvider = CreateServiceProvider();

        Log.Providers.AddTrace();

        // Diagnostics
        // Sentry - if you want to use Sentry, add the project Deve.Diagnostics.Sentry as a reference, uncomment the next lines and
        // change the DSN with your own (you can create a free account at https://sentry.io/welcome/).
        /*SentrySdk.Init(options =>
        {
            options.ConfigureSentry("Use your DSN");

            // Enable Global Mode since this is a client app
            options.IsGlobalModeEnabled = true;
        });
        Log.Providers.AddSentry();*/

        DispatcherUnhandledException += OnAppDispatcherUnhandledException;
    }
    #endregion

    #region Lifecycle Methods
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
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
    private static ServiceProvider CreateServiceProvider()
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        return services.BuildServiceProvider();
    }

    private static void ConfigureServices(ServiceCollection services)
    {
        ServiceProviderHelper.RegisterServices(services);
        ServiceProviderHelper.RegisterViewModels(services);
        ServiceProviderHelper.RegisterViews(services);
    }

    public void ChangeCulture(CultureInfo newCulture, string username, string password)
    {
        Thread.CurrentThread.CurrentCulture = newCulture;
        Thread.CurrentThread.CurrentUICulture = newCulture;

        _serviceProvider = CreateServiceProvider();

        var oldWindow = Current.MainWindow;

        var loginView = _serviceProvider.GetRequiredService<LoginView>();
        loginView.SetUsernamePassword(username, password);
        loginView.Show();

        Current.MainWindow = loginView;

        oldWindow.Close();

        Log.Debug($"Changed culture to {newCulture}");
    }
    #endregion

    #region Events
    private static void OnAppDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        Log.Error(e.Exception);

        // If you want to avoid the application from crashing, change it to true
        e.Handled = false;
    }
    #endregion
}
