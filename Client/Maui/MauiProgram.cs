﻿using Microsoft.Extensions.Logging;
using Microsoft.Maui.LifecycleEvents;
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
#endif

namespace Deve.ClientApp.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .ConfigureLifecycleEvents(events =>
                {
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
                });

#if DEBUG
            Log.Providers.AddDebug();
#endif

            return builder.Build();
        }
    }
}
