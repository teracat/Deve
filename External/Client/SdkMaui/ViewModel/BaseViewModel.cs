namespace Deve.External.ClientApp.Maui
{
    public abstract class BaseViewModel : UIBase
    {
        #region Fields
        private bool _isBusy = false;
        private string _errorText = string.Empty;
        #endregion

        #region Properties
        protected BasePage Page { get; private set; }

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
        public BaseViewModel(BasePage page)
        {
            Page = page;
        }
        #endregion

        #region Virtual Methods
        public virtual void OnViewAppearing()
        {
        }

        public virtual void OnViewDisappearing()
        {
        }

        protected virtual void OnIsBusyChanged()
        {
        }

        public virtual bool OnViewBackButtonPressed()
        {
            if (IsBusy)
                return true;
            return false;
        }
        #endregion
    }
}