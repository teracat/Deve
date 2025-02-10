using Deve.Clients.Maui.ViewModels;

namespace Deve.Clients.Maui.Views
{
    public abstract class BaseView : ContentPage
    {
        #region Fields
        private BaseViewModel? _viewModel;
        #endregion

        #region Properties
        protected BaseViewModel? ViewModel
        {
            get => _viewModel;
            set
            {
                if (_viewModel != value)
                {
                    BindingContext = _viewModel = value;
                }
            }
        }
        #endregion

        #region Constructor
        public BaseView(BaseViewModel viewModel)
        {
            ViewModel = viewModel;
        }
        #endregion

        #region Overrides
        protected override bool OnBackButtonPressed()
        {
            if (ViewModel is not null)
                return ViewModel.OnViewBackButtonPressed();
            else
                return base.OnBackButtonPressed();
        }
        #endregion
    }
}