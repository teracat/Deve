﻿using Deve.ClientApp.Wpf.Helpers;
using Deve.ClientApp.Wpf.Interfaces;

namespace Deve.ClientApp.Wpf.ViewModels
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
                {
                    OnPropertyChanged(nameof(HasError));
                }
            }
        }

        public bool HasError => !string.IsNullOrWhiteSpace(_errorText);

        public Action<bool>? SetResultAction { get; set; }

        public Action? CloseAction { get; set; }
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
        #endregion

        #region Helper Methods
        protected void SetResult(bool value) => SetResultAction?.Invoke(value);

        protected void Close() => CloseAction?.Invoke();
        #endregion
    }
}