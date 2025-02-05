using CommunityToolkit.Mvvm.ComponentModel;
using Deve.ClientApp.Maui.Helpers;
using Deve.ClientApp.Maui.Interfaces;

namespace Deve.ClientApp.Maui.ViewModels
{
    public abstract partial class BaseViewModel : ObservableValidator
    {
        #region Fields
        private readonly IServiceProvider _serviceProvider;
        private readonly IDataService _dataService;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsIdle))]
        private bool _isBusy = false;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasError))]
        private string _errorText = string.Empty;
        #endregion

        #region Properties
        protected IDataService DataService => _dataService;

        protected IServiceProvider ServiceProvider => _serviceProvider;

        public bool IsIdle => !IsBusy;

        public bool HasError => !string.IsNullOrWhiteSpace(ErrorText);

        public Action? GoBackAction { get; set; }
        #endregion

        #region Constructor
        public BaseViewModel(IServiceProvider serviceProvider, IDataService dataService)
        {
            _serviceProvider = serviceProvider;
            _dataService = dataService;
        }
        #endregion

        #region Virtual Methods
        public virtual bool OnViewBackButtonPressed()
        {
            if (IsBusy)
                return true;
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
        protected void GoBack() => GoBackAction?.Invoke();
        #endregion
    }
}