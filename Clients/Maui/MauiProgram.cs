//-:cnd
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
#endif
//+:cnd
using Microsoft.Maui.LifecycleEvents;
using Deve.Logging;
using Deve.Clients.Maui.Helpers;

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
                // Sentry - if you want to use Sentry, add the project Deve.Diagnostics.Sentry.Maui as a reference, uncomment the next line and
                // change the DSN with your own (you can create a free account at https://sentry.io/welcome/).
                // You should also modify the App.xaml.cs file to start and finish a transaction for user sessions and
                // use the SentryHttpMessageHandler in the ServiceProviderHelper.cs file to capture HTTP requests.
                //.UseSentryForMaui("Use your DSN")
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