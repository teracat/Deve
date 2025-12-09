using CommunityToolkit.Mvvm.ComponentModel;
using Deve.Clients.Wpf.Interfaces;
using Deve.Internal.Data;

namespace Deve.Clients.Wpf.ViewModels
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
        protected INavigationService NavigationService { get; }

        protected IData Data { get; }

        protected IMessageHandler MessageHandler { get; }
        public bool IsIdle => !IsBusy;

        public bool HasError => !string.IsNullOrWhiteSpace(ErrorText);

        public Action<bool>? SetResultAction { get; set; }

        public Action? CloseAction { get; set; }
        #endregion

        #region Constructor
        protected BaseViewModel(INavigationService navigationService, IData data, IMessageHandler messageHandler)
        {
            NavigationService = navigationService;
            Data = data;
            MessageHandler = messageHandler;
        }
        #endregion

        #region OnPropertyChanged
        partial void OnIsBusyChanged(bool value) => OnIsBusyChanged();
        #endregion

        #region Virtual Methods
        protected virtual void OnIsBusyChanged() { }

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