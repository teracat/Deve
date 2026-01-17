using Deve.Clients.Wpf.Helpers;
using Deve.Clients.Wpf.Interfaces;
using Deve.Data;

namespace Deve.Clients.Wpf.ViewModels;

internal abstract class BaseViewModel : UIBase
{
    #region Properties
    protected INavigationService NavigationService { get; }

    protected IData Data { get; }

    protected IMessageHandler MessageHandler { get; }

    public bool IsBusy
    {
        get; set
        {
            if (field != value)
            {
                field = value;
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
        get; set
        {
            if (SetProperty(ref field, value))
            {
                OnPropertyChanged(nameof(HasError));
            }
        }
    } = string.Empty;

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

    #region Virtual Methods
    protected virtual void OnIsBusyChanged() { }
    #endregion

    #region Helper Methods
    protected void SetResult(bool value) => SetResultAction?.Invoke(value);

    protected void Close() => CloseAction?.Invoke();
    #endregion
}
