﻿using Microsoft.Extensions.DependencyInjection;
using Deve.ClientApp.Wpf.Interfaces;
using Deve.ClientApp.Wpf.Views;

namespace Deve.ClientApp.Wpf.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo<TView>(object? parameter = null) where TView : BaseView
        {
            BaseView view = _serviceProvider.GetRequiredService<TView>();
            if (view.ViewModel is INavigationAware navAware)
            {
                navAware.OnNavigatedTo(parameter);
            }
            view.Show();
        }

        public bool NavigateModalTo<TView>(object? parameter = null) where TView : BaseView
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
    }
}