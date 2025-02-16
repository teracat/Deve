using Deve.Clients.Wpf.Views;

namespace Deve.Clients.Wpf.Interfaces
{
    public interface INavigationService
    {
        void NavigateTo<TView>(object? parameter) where TView : BaseView;

        void NavigateTo<TView>() where TView : BaseView;

        bool NavigateModalTo<TView>(object? parameter) where TView : BaseView;

        bool NavigateModalTo<TView>() where TView : BaseView;

        void NavigateTo<TView, TParamType>(TParamType parameter) where TView : BaseView where TParamType : class;

        bool NavigateModalTo<TView, TParamType>(TParamType parameter) where TView : BaseView where TParamType : class;
    }
}