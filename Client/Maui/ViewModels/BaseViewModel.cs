using Deve.ClientApp.Maui.Helpers;
using Deve.ClientApp.Maui.Interfaces;

namespace Deve.ClientApp.Maui.ViewModels
{
    public abstract class BaseViewModel : UIBase
    {
        #region Fields
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        private bool _isBusy = false;
        private string _errorText = string.Empty;
        #endregion

        #region Properties
        protected INavigationService NavigationService => _navigationService;

        protected IDataService DataService => _dataService;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged(nameof(IsBusy));
                    OnPropertyChanged(nameof(IsIdle));
                    OnIsBusyChanged();
                }
            }
        }

        public bool IsIdle
        {
            get => !IsBusy;
            set => IsBusy = !value;
        }

        public string ErrorText
        {
            get => _errorText;
            set
            {
                if (SetProperty(ref _errorText, value))
                    OnPropertyChanged(nameof(HasError));
            }
        }

        public bool HasError => !string.IsNullOrWhiteSpace(_errorText);
        #endregion

        #region Constructor
        public BaseViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
        }
        #endregion

        #region Virtual Methods
        protected virtual void OnIsBusyChanged() {}

        public virtual bool OnViewBackButtonPressed()
        {
            if (IsBusy)
                return true;
            return false;
        }
        #endregion

        #region Helper Methods
        protected void GoBack() => _ = NavigationService.PopAsync();
        #endregion
    }
}