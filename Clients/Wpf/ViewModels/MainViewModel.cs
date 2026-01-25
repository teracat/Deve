using Deve.Clients.Wpf.Helpers;
using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Models;
using Deve.Clients.Wpf.Resources.Strings;
using Deve.Clients.Wpf.Views;
using Deve.Data;
using Deve.Dto.Responses.Results;
using Deve.Customers.Cities;
using Deve.Customers.Clients;
using Deve.Customers.Countries;
using Deve.Customers.States;
using Deve.Customers.Stats;

namespace Deve.Clients.Wpf.ViewModels;

internal sealed class MainViewModel : BaseViewModel, IAsyncInitialization
{
    #region Fields
    private ListControlData _ctrlDataClients;
    private ListControlData _ctrlDataCities;
    private ListControlData _ctrlDataStates;
    private ListControlData _ctrlDataCountries;
    #endregion

    #region Properties
    public Task Initialization { get; private set; }

    public ListControlData CtrlDataClients
    {
        get => _ctrlDataClients;
        set => SetProperty(ref _ctrlDataClients, value);
    }
    public ListControlData CtrlDataCities
    {
        get => _ctrlDataCities;
        set => SetProperty(ref _ctrlDataCities, value);
    }
    public ListControlData CtrlDataStates
    {
        get => _ctrlDataStates;
        set => SetProperty(ref _ctrlDataStates, value);
    }
    public ListControlData CtrlDataCountries
    {
        get => _ctrlDataCountries;
        set => SetProperty(ref _ctrlDataCountries, value);
    }
    public bool IsLoadingClientStats
    {
        get;
        set => SetProperty(ref field, value);
    }
    public ClientStatsResponse? ClientStats
    {
        get;
        set => SetProperty(ref field, value);
    }
    #endregion

    #region Constructor
    public MainViewModel(INavigationService navigationService, IData data, IMessageHandler messageHandler)
        : base(navigationService, data, messageHandler)
    {
        _ctrlDataClients = new(LoadDataClients);
        _ctrlDataCities = new(LoadDataCities);
        _ctrlDataStates = new(LoadDataStates);
        _ctrlDataCountries = new(LoadDataCountries);
        Initialization = LoadData();
    }
    #endregion

    #region Methods
    private async Task LoadData()
    {
        var taskClients = LoadDataClients();
        var taskCities = LoadDataCities();
        var taskStates = LoadDataStates();
        var taskCountries = LoadDataCountries();
        var taskStats = LoadClientStats();

        await Task.WhenAll(taskClients, taskCities, taskStates, taskCountries, taskStats);
    }

    private async Task LoadDataClients()
    {
        var request = new ClientGetListRequest
        {
            Name = _ctrlDataClients.SearchText
        };
        await LoadDataList(_ctrlDataClients, Data.Customers.Clients.GetAsync, request, x => new ListDataClient()
        {
            Id = x.Id,
            Main = x.TradeName ?? x.Name,
            Detail = x.CityName + ", " + x.StateName + " (" + x.CountryName + ")",
            Balance = x.Balance,
        });
    }

    private async Task LoadDataCities()
    {
        var request = new CityGetListRequest
        {
            Name = _ctrlDataCities.SearchText
        };
        await LoadDataList(_ctrlDataCities, Data.Customers.Cities.GetAsync, request, x => new ListData()
        {
            Id = x.Id,
            Main = x.Name,
            Detail = x.StateName ?? string.Empty,
        });
    }

    private async Task LoadDataStates()
    {
        var request = new StateGetListRequest
        {
            Name = _ctrlDataStates.SearchText
        };
        await LoadDataList(_ctrlDataStates, Data.Customers.States.GetAsync, request, x => new ListData()
        {
            Id = x.Id,
            Main = x.Name,
            Detail = x.CountryName ?? string.Empty,
        });
    }

    private async Task LoadDataCountries()
    {
        var request = new CountryGetListRequest
        {
            Name = _ctrlDataCountries.SearchText
        };
        await LoadDataList(_ctrlDataCountries, Data.Customers.Countries.GetAsync, request, x => new ListData()
        {
            Id = x.Id,
            Main = x.Name,
            Detail = x.IsoCode,
        });
    }

    private async Task LoadClientStats()
    {
        IsLoadingClientStats = true;
        try
        {
            var res = await Data.Customers.Stats.GetClientStatsAsync();
            if (!res.Success)
            {
                _ctrlDataCountries.ErrorText = Utils.ErrorsToString(res.Errors);
                return;
            }

            ClientStats = res.Data;
        }
        finally
        {
            IsLoadingClientStats = false;
        }
    }

    private async Task DoAddState()
    {
        if (NavigationService.NavigateModalTo<StateView>())
        {
            await LoadDataStates();
        }
    }

    private async Task DoEditState(ListData? listData)
    {
        await DoEdit(listData, _ctrlDataStates, Data.Customers.States.GetByIdAsync, (o) =>
        {
            // Use case 1: sending Id to force data download again
            if (NavigationService.NavigateModalTo<StateView>(o.Id))
            {
                _ = LoadDataStates();
                return true;
            }
            return false;
        });
    }

    private async Task DoDeleteState(ListData? listData) => await DoDelete(listData, AppResources.ConfirmDeleteState, _ctrlDataStates, Data.Customers.States.DeleteAsync, LoadDataStates);

    private async Task DoAddCountry()
    {
        if (NavigationService.NavigateModalTo<CountryView>())
        {
            await LoadDataCountries();
        }
    }

    private async Task DoEditCountry(ListData? listData)
    {
        await DoEdit(listData, _ctrlDataCountries, Data.Customers.Countries.GetByIdAsync, (o) =>
        {
            // Use case 2: sending Country object to avoid data download again
            if (NavigationService.NavigateModalTo<CountryView, CountryResponse>(o))
            {
                _ = LoadDataCountries();
                return true;
            }
            return false;
        });
    }

    private async Task DoDeleteCountry(ListData? listData) => await DoDelete(listData, AppResources.ConfirmDeleteCountry, _ctrlDataCountries, Data.Customers.Countries.DeleteAsync, LoadDataCountries);
    #endregion

    #region Generic Methods
    private static async Task LoadDataList<TRequest, TResponse>(ListControlData data, Func<TRequest, CancellationToken, Task<ResultGetList<TResponse>>> funcGetListAsync, TRequest request, Func<TResponse, ListData> selector) where TResponse : class
    {
        data.IsBusy = true;
        try
        {
            var res = await funcGetListAsync(request, default);
            if (!res.Success)
            {
                data.ErrorText = Utils.ErrorsToString(res.Errors);
                return;
            }

            data.Items = res.Data
                            .Select(selector)
                            .ToList();
        }
        finally
        {
            data.IsBusy = false;
        }
    }

    private async Task DoEdit<TResponse>(ListData? listData, ListControlData data, Func<Guid, CancellationToken, Task<ResultGet<TResponse>>> funcGetByIdAsync, Func<TResponse, bool> editFunc) where TResponse : class
    {
        if (listData is not null)
        {
            TResponse? obj = null;

            data.IsBusy = true;
            try
            {
                var res = await funcGetByIdAsync(listData.Id, default);
                if (!res.Success)
                {
                    MessageHandler.ShowError(res.Errors);
                    return;
                }

                obj = res.Data;
            }
            finally
            {
                data.IsBusy = false;
            }

            if (obj is not null)
            {
                _ = editFunc(obj);
            }
        }
    }

    private async Task DoDelete(ListData? listData, string message, ListControlData data, Func<Guid, CancellationToken, Task<Result>> funcDeleteAsync, Func<Task> actionWhenDone)
    {
        if (listData is null)
        {
            return;
        }
        if (!MessageHandler.ShowQuestion(string.Format(Thread.CurrentThread.CurrentCulture, message, listData.Main), AppResources.Delete))
        {
            return;
        }

        data.IsBusy = true;
        try
        {
            var res = await funcDeleteAsync(listData.Id, default);
            if (!res.Success)
            {
                MessageHandler.ShowError(res.Errors);
                return;
            }

            await actionWhenDone();
        }
        finally
        {
            data.IsBusy = false;
        }
    }
    #endregion

    #region Commands
    public AsyncCommand AddState => field ??= new AsyncCommand(DoAddState, () => !_ctrlDataStates.IsBusy);
    public AsyncCommand EditState => field ??= new AsyncCommand((listData) => DoEditState((ListData?)listData), () => !_ctrlDataStates.IsBusy);
    public AsyncCommand DeleteState => field ??= new AsyncCommand((listData) => DoDeleteState((ListData?)listData), () => !_ctrlDataStates.IsBusy);

    public AsyncCommand AddCountry => field ??= new AsyncCommand(DoAddCountry, () => !_ctrlDataCountries.IsBusy);
    public AsyncCommand EditCountry => field ??= new AsyncCommand((listData) => DoEditCountry((ListData?)listData), () => !_ctrlDataCountries.IsBusy);
    public AsyncCommand DeleteCountry => field ??= new AsyncCommand((listData) => DoDeleteCountry((ListData?)listData), () => !_ctrlDataCountries.IsBusy);
    #endregion
}
