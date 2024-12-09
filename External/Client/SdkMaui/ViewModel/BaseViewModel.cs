namespace Deve.External.ClientApp.Maui
{
    public abstract class BaseViewModel : UIBase
    {
        #region Fields
        private bool _isBusy = false;
        private IData? _data;
        #endregion

        #region Properties
        protected BasePage Page { get; private set; }

        protected IData Api => _data ??= Sdk.SdkFactory.Get(Deve.Sdk.EnvironmentType.Staging);

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