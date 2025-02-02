using CommunityToolkit.Mvvm.ComponentModel;

namespace Deve.ClientApp.Maui.ViewModels
{
    public abstract partial class BaseViewModel : ObservableValidator
    {
        #region Fields
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsIdle))]
        private bool _isBusy = false;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasError))]
        private string _errorText = string.Empty;
        #endregion

        #region Properties
        public bool IsIdle => !IsBusy;

        public bool HasError => !string.IsNullOrWhiteSpace(ErrorText);
        #endregion

        #region Virtual Methods
        public virtual void OnViewAppearing() {}

        public virtual void OnViewDisappearing() {}

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
    }
}