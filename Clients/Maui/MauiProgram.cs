//-:cnd
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
#endif
//+:cnd
using Microsoft.Maui.LifecycleEvents;
//using Sentry.OpenTelemetry;
using Deve.Clients.Maui.Helpers;
using Deve.Logging;
using Deve.Diagnostics;

namespace Deve.Clients.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
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
                //.AddDiagnosticsSentry("Your Sentry DSN")
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
//-:cnd
#if DEBUG
            Log.Providers.AddDebug();
#endif
//+:cnd
            return builder.Build();
        }
    }
}
