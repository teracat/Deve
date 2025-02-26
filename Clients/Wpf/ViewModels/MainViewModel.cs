﻿using Deve.Criteria;
using Deve.Model;
using Deve.Internal.Data;
using Deve.Internal.Criteria;
using Deve.Internal.Model;
using Deve.Clients.Wpf.Views;
using Deve.Clients.Wpf.Resources.Strings;
using Deve.Clients.Wpf.Helpers;
using Deve.Clients.Wpf.Models;
using Deve.Clients.Wpf.Interfaces;

namespace Deve.Clients.Wpf.ViewModels
{
    public class MainViewModel : BaseViewModel, IAsyncInitialization
    {
        #region Fields
        private ListControlData _ctrlDataClients;
        private ListControlData _ctrlDataCities;
        private ListControlData _ctrlDataStates;
        private ListControlData _ctrlDataCountries;
        private bool _isLoadingClientStats = false;
        private ClientStats? _clientStats;

        private AsyncCommand? _addStateCommand;
        private AsyncCommand? _editStateCommand;
        private AsyncCommand? _deleteStateCommand;
        private AsyncCommand? _addCountryCommand;
        private AsyncCommand? _editCountryCommand;
        private AsyncCommand? _deleteCountryCommand;
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
            get => _isLoadingClientStats;
            set => SetProperty(ref _isLoadingClientStats, value);
        }
        public ClientStats? ClientStats
        {
            get => _clientStats;
            set => SetProperty(ref _clientStats, value);
        }
        #endregion

        #region Constructor
        public MainViewModel(INavigationService navigationService, Internal.Data.IData data, IMessageHandler messageHandler)
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
            var criteria = new CriteriaClient()
            {
                Name = _ctrlDataClients.SearchText,
            };
            await LoadDataList(_ctrlDataClients, Data.Clients, criteria, x => new ListDataClient()
            {
                Id = x.Id,
                Main = x.DisplayName,
                Detail = x.Location.City + ", " + x.Location.State + " (" + x.Location.Country + ")",
                Balance = x.Balance,
            });
        }

        private async Task LoadDataCities()
        {
            var criteria = new CriteriaCity()
            {
                Name = _ctrlDataCities.SearchText,
            };
            await LoadDataList(_ctrlDataCities, Data.Cities, criteria, x => new ListData()
            {
                Id = x.Id,
                Main = x.Name,
                Detail = x.State,
            });
        }

        private async Task LoadDataStates()
        {
            var criteria = new CriteriaState()
            {
                Name = _ctrlDataStates.SearchText,
            };
            await LoadDataList(_ctrlDataStates, Data.States, criteria, x => new ListData()
            {
                Id = x.Id,
                Main = x.Name,
                Detail = x.Country,
            });
        }

        private async Task LoadDataCountries()
        {
            var criteria = new CriteriaCountry()
            {
                Name = _ctrlDataCountries.SearchText,
            };
            await LoadDataList(_ctrlDataCountries, Data.Countries, criteria, x => new ListData()
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
                var res = await Data.Stats.GetClientStats();
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
            await DoEdit(listData, _ctrlDataStates, Data.States, (o) =>
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

        private async Task DoDeleteState(ListData? listData)
        {
            await DoDelete(listData, AppResources.ConfirmDeleteState, _ctrlDataStates, Data.States, LoadDataStates);
        }

        private async Task DoAddCountry()
        {
            if (NavigationService.NavigateModalTo<CountryView>())
            {
                await LoadDataCountries();
            }
        }

        private async Task DoEditCountry(ListData? listData)
        {
            await DoEdit(listData, _ctrlDataCountries, Data.Countries, (o) =>
            {
                // Use case 2: sending Country object to avoid data download again
                if (NavigationService.NavigateModalTo<CountryView, Country>(o))
                {
                    _ = LoadDataCountries();
                    return true;
                }
                return false;
            });
        }

        private async Task DoDeleteCountry(ListData? listData)
        {
            await DoDelete(listData, AppResources.ConfirmDeleteCountry, _ctrlDataCountries, Data.Countries, LoadDataCountries);
        }
        #endregion

        #region Generic Methods
        private async Task LoadDataList<ModelList, Model, Criteria>(ListControlData data, IDataAll<ModelList, Model, Criteria> dataAll, Criteria criteria, Func<ModelList, ListData> selector) where ModelList : ModelId where Model : ModelId where Criteria : CriteriaId
        {
            data.IsBusy = true;
            try
            {
                var res = await dataAll.Get(criteria);
                if (!res.Success)
                {
                    data.ErrorText = Utils.ErrorsToString(res.Errors);
                    return;
                }

                data.Items = res.Data.Select(selector).ToList();
            }
            finally
            {
                data.IsBusy = false;
            }
        }

        private async Task DoEdit<ModelList, Model, Criteria>(ListData? listData, ListControlData data, IDataAll<ModelList, Model, Criteria> dataAll, Func<Model, bool> editFunc) where ModelList: ModelId where Model: ModelId where Criteria : CriteriaId
        {
            if (listData is not null)
            {
                Model? obj = null;

                data.IsBusy = true;
                try
                {
                    var res = await dataAll.Get(listData.Id);
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
                    editFunc(obj);
                }
            }
        }

        private async Task DoDelete<ModelList, Model, Criteria>(ListData? listData, string message, ListControlData data, IDataAll<ModelList, Model, Criteria> dataAll, Func<Task> actionWhenDone) where ModelList : ModelId where Model : ModelId where Criteria : CriteriaId
        {
            if (listData is not null)
            {
                if (MessageHandler.ShowQuestion(string.Format(message, listData.Main), AppResources.Delete))
                {
                    data.IsBusy = true;
                    try
                    {
                        var res = await dataAll.Delete(listData.Id);
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
            }
        }
        #endregion

        #region Commands
        public AsyncCommand AddState => _addStateCommand ??= new AsyncCommand(DoAddState, () => !_ctrlDataStates.IsBusy);
        public AsyncCommand EditState => _editStateCommand ??= new AsyncCommand((listData) => DoEditState((ListData?)listData), () => !_ctrlDataStates.IsBusy);
        public AsyncCommand DeleteState => _deleteStateCommand ??= new AsyncCommand((listData) => DoDeleteState((ListData?)listData), () => !_ctrlDataStates.IsBusy);

        public AsyncCommand AddCountry => _addCountryCommand ??= new AsyncCommand(() => DoAddCountry(), () => !_ctrlDataCountries.IsBusy);
        public AsyncCommand EditCountry => _editCountryCommand ??= new AsyncCommand((listData) => DoEditCountry((ListData?)listData), () => !_ctrlDataCountries.IsBusy);
        public AsyncCommand DeleteCountry => _deleteCountryCommand ??= new AsyncCommand((listData) => DoDeleteCountry((ListData?)listData), () => !_ctrlDataCountries.IsBusy);
        #endregion
    }
}