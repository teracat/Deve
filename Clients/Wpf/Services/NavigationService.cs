using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Views;
using Deve.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Deve.Clients.Wpf.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo<TView>(object? parameter) where TView : BaseView
        {
            try
            {
                BaseView view = _serviceProvider.GetRequiredService<TView>();
                if (view.ViewModel is INavigationAware navAware)
                {
                    navAware.OnNavigatedTo(parameter);
                }
                view.Show();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        public void NavigateTo<TView>() where TView : BaseView => NavigateTo<TView>(null);

        public bool NavigateModalTo<TView>(object? parameter) where TView : BaseView
        {
            try
            {
                BaseView view = _serviceProvider.GetRequiredService<TView>();
                if (view.ViewModel is INavigationAware navAware)
                {
                    navAware.OnNavigatedTo(parameter);
                }
                return view.ShowDialog() ?? false;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
        }

        public bool NavigateModalTo<TView>() where TView : BaseView => NavigateModalTo<TView>(null);

        public void NavigateTo<TView, TParamType>(TParamType parameter) where TView : BaseView where TParamType : class
        {
            try
            {
                BaseView view = _serviceProvider.GetRequiredService<TView>();
                if (view.ViewModel is INavigationAwareWithType<TParamType> navAwareWithType)
                {
                    navAwareWithType.OnNavigatedToWithType(parameter);
                }
                else if (view.ViewModel is INavigationAware navAware)
                {
                    navAware.OnNavigatedTo(parameter);
                }
                view.Show();
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        public bool NavigateModalTo<TView, TParamType>(TParamType parameter) where TView : BaseView where TParamType : class
        {
            try
            {
                BaseView view = _serviceProvider.GetRequiredService<TView>();
                if (view.ViewModel is INavigationAwareWithType<TParamType> navAwareWithType)
                {
                    navAwareWithType.OnNavigatedToWithType(parameter);
                }
                else if (view.ViewModel is INavigationAware navAware)
                {
                    navAware.OnNavigatedTo(parameter);
                }
                return view.ShowDialog() ?? false;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
            }
        }
    }
}