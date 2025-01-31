﻿using System.Windows;
using Deve.ClientApp.Wpf.Helpers;

namespace Deve.ClientApp.Wpf.ViewModels
{
    public abstract class BaseViewModel : UIBase
    {
        #region Fields
        private bool _isBusy = false;
        private string _errorText = string.Empty;
        #endregion

        #region Properties
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
        public BaseViewModel()
        {
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