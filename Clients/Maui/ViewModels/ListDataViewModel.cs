using Deve.Clients.Maui.Helpers;
using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Models;

namespace Deve.Clients.Maui.ViewModels;

internal abstract class ListDataViewModel : BaseViewModel, IAsyncInitialization
{
    #region Properties
    public Task Initialization { get; private set; }

    public IEnumerable<ListData>? ListData
    {
        get;
        set => SetProperty(ref field, value);
    }

    public ListData? SelectedData
    {
        get => null;
        set
        {
            if (value is not null)
            {
                DoSelected(value);
            }

            OnPropertyChanged();
        }
    }
    #endregion

    #region Constructor
    protected ListDataViewModel(INavigationService navigationService, Data.IData data)
        : base(navigationService, data)
    {
        Initialization = InitializeAsync();
    }
    #endregion

    #region Methods
    protected async Task InitializeAsync() => await LoadData();

    public async Task LoadData()
    {
        ErrorText = string.Empty;
        IsBusy = true;
        try
        {
            await GetListData();
        }
        finally
        {
            IsBusy = false;
        }
    }
    #endregion

    #region Abstract/Virtual Methods
    protected abstract Task GetListData();

    protected virtual void DoSelected(ListData data)
    {
        var navigationParameter = new NavigationParameters
        {
            { nameof(BaseDetailsViewModel.Id), data.Id }
        };
        _ = NavigationService.NavigateToAsync("details", navigationParameter);
    }
    #endregion
}