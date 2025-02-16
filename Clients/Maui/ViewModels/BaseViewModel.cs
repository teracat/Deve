using CommunityToolkit.Mvvm.ComponentModel;
using Deve.Clients.Maui.Interfaces;

namespace Deve.Clients.Maui.ViewModels
{
    public abstract partial class BaseViewModel : ObservableValidator
    {
        #region Fields
        private readonly INavigationService _navigationService;
        private readonly IDataService _dataService;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsIdle))]
        private bool _isBusy = false;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasError))]
        private string _errorText = string.Empty;
        #endregion

        #region Properties
        protected INavigationService NavigationService => _navigationService;

        protected IDataService DataService => _dataService;

        public bool IsIdle => !IsBusy;

        public bool HasError => !string.IsNullOrWhiteSpace(ErrorText);
        #endregion

        #region Constructor
        protected BaseViewModel(INavigationService navigationService, IDataService dataService)
        {
            _navigationService = navigationService;
            _dataService = dataService;
        }
        #endregion

        #region Virtual Methods
        public virtual bool OnViewBackButtonPressed()
        {
            if (IsBusy)
            {
                return true;
            }

            return false;
        }

        protected virtual bool Validate()
        {
            ErrorText = string.Empty;
            ClearErrors();
            ValidateAllProperties();
            if (HasErrors)
            {
                ErrorText = string.Join("\n", GetErrors());
                return false;
            }
            return true;
        }
        #endregion

        #region Helper Methods
        protected void GoBack() => _ = NavigationService.PopAsync();
        #endregion
    }
}