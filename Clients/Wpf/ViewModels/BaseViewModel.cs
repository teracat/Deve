using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using ReactiveUI.Validation.Helpers;
using Deve.Clients.Wpf.Interfaces;
using Deve.Data;

namespace Deve.Clients.Wpf.ViewModels;

internal abstract partial class BaseViewModel : ReactiveValidationObject
{
    #region Fields
    [Reactive]
    private bool _isBusy = false;

    [Reactive]
    private bool _shouldValidate = false;

    [Reactive]
    private string _errorText = string.Empty;

    [ObservableAsProperty]
    private bool _isIdle;

    [ObservableAsProperty]
    private bool _hasError;
    #endregion

    #region Properties
    protected INavigationService NavigationService { get; }

    protected IData Data { get; }

    protected IMessageHandler MessageHandler { get; }

    public Action<bool>? SetResultAction { get; set; }

    public Action? CloseAction { get; set; }
    #endregion

    #region Constructor
    protected BaseViewModel(INavigationService navigationService, IData data, IMessageHandler messageHandler, ISchedulerProvider scheduler)
    {
        NavigationService = navigationService;
        Data = data;
        MessageHandler = messageHandler;

        // Properties
        _isIdleHelper = this.WhenAnyValue(vm => vm.IsBusy)
                            .Select(isBusy => !isBusy)
                            .ToProperty(this, vm => vm.IsIdle, initialValue: true, scheduler: scheduler.MainThread);

        _hasErrorHelper = this.WhenAnyValue(x => x.ErrorText)
                              .Select((errorText) => !string.IsNullOrWhiteSpace(errorText))
                              .ToProperty(this, vm => vm.HasError, initialValue: false, scheduler: scheduler.MainThread);
    }
    #endregion

    #region Virtual Methods
    protected virtual bool Validate()
    {
        ErrorText = string.Empty;
        ShouldValidate = true;
        return ValidationContext.IsValid;
    }
    #endregion

    #region Helper Methods
    protected void SetResult(bool value) => SetResultAction?.Invoke(value);

    protected void Close() => CloseAction?.Invoke();
    #endregion
}