using Deve.ClientApp.Wpf.Views;

namespace Deve.ClientApp.Wpf.Interfaces
{
    public interface INavigationService
    {
        void NavigateTo<TView>(object? parameter = null) where TView : BaseView;

        bool NavigateModalTo<TView>(object? parameter = null) where TView : BaseView;

        void NavigateTo<TView, TParamType>(TParamType parameter) where TView : BaseView where TParamType : class;

        bool NavigateModalTo<TView, TParamType>(TParamType parameter) where TView : BaseView where TParamType : class;
    }
}