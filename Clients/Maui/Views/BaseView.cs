using Deve.Clients.Maui.ViewModels;

namespace Deve.Clients.Maui.Views;

internal abstract class BaseView : ContentPage
{
    #region Properties
    protected BaseViewModel? ViewModel
    {
        get;
        set
        {
            if (field != value)
            {
                BindingContext = field = value;
            }
        }
    }
    #endregion

    #region Constructor
    protected BaseView(BaseViewModel viewModel)
    {
        ViewModel = viewModel;
    }
    #endregion

    #region Overrides
    protected override bool OnBackButtonPressed()
    {
        if (ViewModel is not null)
        {
            return ViewModel.OnViewBackButtonPressed();
        }
        else
        {
            return base.OnBackButtonPressed();
        }
    }
    #endregion
}