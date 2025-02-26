using CommunityToolkit.Mvvm.ComponentModel;
using Deve.Internal.Data;
using Deve.Clients.Wpf.Interfaces;

namespace Deve.Clients.Wpf.ViewModels
{
    public abstract partial class BaseViewModel : ObservableValidator
    {
        #region Fields
        private readonly INavigationService _navigationService;
        private readonly IData _data;
        private readonly IMessageHandler _messageHandler;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsIdle))]
        private bool _isBusy = false;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasError))]
        private string _errorText = string.Empty;
        #endregion

        #region Properties
        protected INavigationService NavigationService => _navigationService;

        protected IData Data => _data;

        protected IMessageHandler MessageHandler => _messageHandler;
        public bool IsIdle => !IsBusy;

        public bool HasError => !string.IsNullOrWhiteSpace(ErrorText);

        public Action<bool>? SetResultAction { get; set; }

        public Action? CloseAction { get; set; }
        #endregion

        #region Constructor
        protected BaseViewModel(INavigationService navigationService, IData data, IMessageHandler messageHandler)
        {
            _navigationService = navigationService;
            _data = data;
            _messageHandler = messageHandler;
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