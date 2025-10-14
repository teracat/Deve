//-:cnd
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
#endif
//+:cnd
using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
//using Sentry.OpenTelemetry;
using Deve.Clients.Maui.Helpers;
using Deve.Logging;
using Deve.Diagnostics;

namespace Deve.Clients.Maui
{
    public static class MauiProgram
    {
        private static ILogger<MauiApp>? _logger;

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder.Logging.SetMinimumLevel(LogLevel.Debug);

//-:cnd
#if DEBUG
            // We add the Debug logger for the logs generated before the MauiApp is built.
            Log.Providers.AddDebug();

            builder.Logging.AddDebug();

            // For development purposes only - to avoid setting the environment variable on your machine. Remove the values before pushing your code to a public repository.
            Environment.SetEnvironmentVariable("APPLICATIONINSIGHTS_CONNECTION_STRING", "");
            //Environment.SetEnvironmentVariable("SENTRY_DSN", "");
            Environment.SetEnvironmentVariable("ZIPKIN_URL", "http://localhost:9411");
#endif
//+:cnd

            builder
                .UseMauiApp<App>()
                .RegisterServices()
                .RegisterViewModels()
                .RegisterViews()
                // Diagnostics
                // OpenTelemetry - if you don't want to use OpenTelemetry, remove the project Deve.Diagnostics.OpenTelemetry.Maui as a reference and comment the next line.
                .AddDiagnosticsOpenTelemetry(funcConfigTracing: (tracing) =>
                {
                    // Configure Tracing exporters here (if you want to use other exporters).

                    // Example for Sentry:
                    //tracing.AddSentry();    // You need to uncomment the "using Sentry.OpenTelemetry;" line at the top of this file.
                })
                // Sentry - if you want to use Sentry, add the project Deve.Diagnostics.Sentry.Maui as a reference, uncomment the next line and
                // change the DSN with your own (you can create a free account at https://sentry.io/welcome/).
                // You should also modify the ServiceProviderHelper.cs file to use Sentry instead of OpenTelemetry.
                //.AddDiagnosticsSentry()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureLifecycleEvents(events =>
                {
//-:cnd
#if WINDOWS
                    events.AddWindows(w =>
                    {
                        w.OnWindowCreated(window =>
                        {
                            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(window);
                            WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
                            var appWindow = AppWindow.GetFromWindowId(myWndId);
                            if (appWindow.Presenter is OverlappedPresenter overlappedPresenter)
                            {
                                overlappedPresenter.IsMinimizable = true;
                                overlappedPresenter.IsMaximizable = true;
                                overlappedPresenter.IsAlwaysOnTop = false;
                                overlappedPresenter.IsResizable = true;

                                overlappedPresenter.Maximize();
                            }

                            //If you don't want the top bar, use the following line instead
                            //appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
                        });
                    });
#endif
//+:cnd
                });

            var app = builder.Build();

            _logger = app.Services.GetService<ILogger<MauiApp>>();
            if (_logger is not null)
            {
                Log.Providers.AddNetCore(_logger);
                Log.Providers.RemoveDebug(); // We remove the Debug logger because it's already included in the NetCore provider.
            }

            return app;
        }
    }
}
