using Deve.ClientApp.Wpf.Window;
using System.Runtime.InteropServices;
using System.Windows;

namespace Deve.ClientApp.Wpf.ViewModel
{
    public abstract class BaseViewModel : UIBase
    {
        #region Fields
        private bool _isBusy = false;
        private string _errorText = string.Empty;
        #endregion

        #region Properties
        protected BaseWindow Window { get; private set; }

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

        public Visibility IsBusyVisibility => IsBusy ? Visibility.Visible : Visibility.Collapsed;

        public Visibility IsIdleVisibility => IsIdle ? Visibility.Visible : Visibility.Collapsed;

        public string ErrorText
        {
            get => _errorText;
            set
            {
                if (SetProperty(ref _errorText, value))
                {
                    OnPropertyChanged(nameof(HasError));
                    OnPropertyChanged(nameof(HasErrorVisibility));
                }
            }
        }

        public bool HasError => !string.IsNullOrWhiteSpace(_errorText);
        public Visibility HasErrorVisibility => HasError ? Visibility.Visible : Visibility.Collapsed;
        #endregion

        #region Constructor
        public BaseViewModel(BaseWindow window)
        {
            Window = window;
        }
        #endregion

        #region Virtual Methods
        public virtual void OnWindowLoaded() {}

        public virtual void OnWindowUnloaded() {}

        protected virtual void OnIsBusyChanged() {}
        #endregion
    }
}