using Deve.ClientApp.Maui.ViewModels;

namespace Deve.ClientApp.Maui.Views
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
                    if (_viewModel is not null)
                    {
                        _viewModel.GoBackAction = GoBack;
                    }
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

        #region Methods
        protected virtual void GoBack()
        {
            Navigation?.PopAsync();
        }
        #endregion
    }
}