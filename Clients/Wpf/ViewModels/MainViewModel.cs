using ReactiveUI;
using ReactiveUI.SourceGenerators;
using Deve.Clients.Wpf.Views;
using Deve.Clients.Wpf.Resources.Strings;
using Deve.Clients.Wpf.Models;
using Deve.Clients.Wpf.Interfaces;
using Deve.Dto.Responses.Results;
using Deve.Customers.Cities;
using Deve.Customers.Clients;
using Deve.Customers.Countries;
using Deve.Customers.States;
using Deve.Customers.Stats;

namespace Deve.Clients.Wpf.ViewModels;

internal sealed partial class MainViewModel : BaseViewModel, IAsyncInitialization
{
    #region Fields
    [Reactive]
    private ListControlData _ctrlDataClients;

    [Reactive]
    private ListControlData _ctrlDataCities;

    [Reactive]
    private ListControlData _ctrlDataStates;

    [Reactive]
    private ListControlData _ctrlDataCountries;

    [Reactive]
    private bool _isLoadingClientStats = false;

    [Reactive]
    private ClientStatsResponse? _clientStats;
    #endregion

    #region Properties
    public Task Initialization { get; private set; }
    #endregion

    #region Constructor
    public MainViewModel(INavigationService navigationService, Data.IData data, IMessageHandler messageHandler, ISchedulerProvider scheduler)
        : base(navigationService, data, messageHandler, scheduler)
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
            Search = CtrlDataClients.SearchText
        };
        await LoadDataList(CtrlDataClients, Data.Customers.Clients.GetAsync, request, x => new ListDataClient()
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
            Name = CtrlDataCities.SearchText,
        };
        await LoadDataList(CtrlDataCities, Data.Customers.Cities.GetAsync, request, x => new ListData()
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
            Name = CtrlDataStates.SearchText
        };
        await LoadDataList(CtrlDataStates, Data.Customers.States.GetAsync, request, x => new ListData()
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
            Name = CtrlDataCountries.SearchText
        };
        await LoadDataList(CtrlDataCountries, Data.Customers.Countries.GetAsync, request, x => new ListData()
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
                CtrlDataCountries.ErrorText = Utils.ErrorsToString(res.Errors);
                return;
            }

            ClientStats = res.Data;
        }
        finally
        {
            IsLoadingClientStats = false;
        }
    }

    [ReactiveCommand(CanExecute = nameof(CtrlDataStates.IsIdle))]
    private async Task AddState()
    {
        if (NavigationService.NavigateModalTo<StateView>())
        {
            await LoadDataStates();
        }
    }

    [ReactiveCommand(CanExecute = nameof(CtrlDataStates.IsIdle))]
    private async Task EditState(ListData? listData)
    {
        await Edit(listData, CtrlDataStates, Data.Customers.States.GetByIdAsync, (o) =>
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

    [ReactiveCommand(CanExecute = nameof(CtrlDataStates.IsIdle))]
    private async Task DeleteState(ListData? listData) => await Delete(listData, AppResources.ConfirmDeleteState, CtrlDataStates, Data.Customers.States.DeleteAsync, LoadDataStates);

    [ReactiveCommand(CanExecute = nameof(CtrlDataCountries.IsIdle))]
    private async Task AddCountry()
    {
        if (NavigationService.NavigateModalTo<CountryView>())
        {
            await LoadDataCountries();
        }
    }

    [ReactiveCommand(CanExecute = nameof(CtrlDataCountries.IsIdle))]
    private async Task EditCountry(ListData? listData)
    {
        await Edit(listData, CtrlDataCountries, Data.Customers.Countries.GetByIdAsync, (o) =>
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

    [ReactiveCommand(CanExecute = nameof(CtrlDataCountries.IsIdle))]
    private async Task DeleteCountry(ListData? listData) => await Delete(listData, AppResources.ConfirmDeleteCountry, CtrlDataCountries, Data.Customers.Countries.DeleteAsync, LoadDataCountries);
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

    private async Task Edit<TResponse>(ListData? listData, ListControlData data, Func<Guid, CancellationToken, Task<ResultGet<TResponse>>> funcGetByIdAsync, Func<TResponse, bool> editFunc) where TResponse : class
    {
        if (listData is null)
        {
            return;
        }

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

    private async Task Delete(ListData? listData, string message, ListControlData data, Func<Guid, CancellationToken, Task<Result>> funcDeleteAsync, Func<Task> actionWhenDone)
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
}
