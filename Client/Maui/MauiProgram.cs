using Deve.ClientApp.Maui.Interfaces;
using Deve.ClientApp.Maui.Services;
using Deve.ClientApp.Maui.ViewModels;
using Deve.ClientApp.Maui.Views;
using Microsoft.Maui.LifecycleEvents;
using System.Reflection;
//-:cnd
#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
#endif
//+:cnd

namespace Deve.ClientApp.Maui
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

        public static MauiAppBuilder RegisterServices(this MauiAppBuilder mauiAppBuilder)
        {
            mauiAppBuilder.Services.AddTransient<IDataService, DataService>();

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var viewModels = assembly.GetTypes()
                                     .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseViewModel)));
            foreach (var vm in viewModels)
            {
                mauiAppBuilder.Services.AddTransient(vm);
            }

            return mauiAppBuilder;
        }

        public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var views = assembly.GetTypes()
                                .Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(BaseView)));
            foreach (var v in views)
            {
                mauiAppBuilder.Services.AddTransient(v);
            }

            return mauiAppBuilder;
        }
    }
}
