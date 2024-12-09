namespace Deve.External.ClientApp.Maui
{
    public abstract class BasePage : ContentPage
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

        #region Overrides
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel?.OnViewAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel?.OnViewDisappearing();
        }

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
