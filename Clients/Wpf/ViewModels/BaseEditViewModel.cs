using Deve.Clients.Wpf.Helpers;
using Deve.Clients.Wpf.Interfaces;

namespace Deve.Clients.Wpf.ViewModels;

internal abstract class BaseEditViewModel : BaseViewModel, INavigationAware
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
    internal virtual void DoCancel()
    {
        SetResult(false);
        Close();
    }

    internal abstract Task DoSave();

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

    #region Commands
    public Command Cancel => field ??= new Command(DoCancel, () => IsIdle);
    public AsyncCommand Save => field ??= new AsyncCommand(DoSave, () => IsIdle);
    #endregion
}
