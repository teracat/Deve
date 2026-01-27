using CommunityToolkit.Mvvm.Input;
using Deve.Clients.Wpf.Interfaces;

namespace Deve.Clients.Wpf.ViewModels;

internal abstract partial class BaseEditViewModel : BaseViewModel, INavigationAware
{
    #region Fields
    public Action? LoadDataDoneAction { get; set; }
    #endregion

    #region Properties
    public Guid Id { get; set; } = Guid.Empty;
    #endregion

    #region Constructor
    protected BaseEditViewModel(INavigationService navigationService, Data.IData data, IMessageHandler messageHandler)
        : base(navigationService, data, messageHandler)
    {
    }
    #endregion

    #region Methods
    [RelayCommand(CanExecute = nameof(IsIdle))]
    internal void Cancel()
    {
        SetResult(false);
        Close();
    }

    [RelayCommand(CanExecute = nameof(IsIdle))]
    internal abstract Task Save();

    protected async Task LoadData()
    {
        IsBusy = true;
        try
        {
            await GetData();
        }
        finally
        {
            IsBusy = false;
        }

        LoadDataDoneAction?.Invoke();
    }

    protected abstract Task GetData();
    #endregion

    #region INavigationAware
    public void OnNavigatedTo(object? parameter)
    {
        if (parameter is Guid id)
        {
            Id = id;
        }
        else
        {
            Id = Guid.Empty;
        }

        _ = LoadData();
    }
    #endregion

    #region Overrides
    protected override void OnIsBusyChanged()
    {
        CancelCommand.NotifyCanExecuteChanged();
        SaveCommand.NotifyCanExecuteChanged();
    }
    #endregion
}