﻿using Deve.ClientApp.Wpf.Helpers;
using Deve.ClientApp.Wpf.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Deve.ClientApp.Wpf.ViewModels
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

        #region OnPropertyChanged
        partial void OnIsBusyChanged(bool value) => OnIsBusyChanged();
        #endregion

        #region Virtual Methods
        protected virtual void OnIsBusyChanged() {}

        protected virtual bool Validate()
        {
            ErrorText = string.Empty;
            ClearErrors();
            ValidateAllProperties();
            return !HasErrors;
        }
        #endregion

        #region Helper Methods
        protected void SetResult(bool value) => SetResultAction?.Invoke(value);

        protected void Close() => CloseAction?.Invoke();
        #endregion
    }
}