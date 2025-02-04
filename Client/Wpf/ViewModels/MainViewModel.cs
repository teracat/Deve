﻿using System.Windows.Input;
using Deve.Internal;
using Deve.ClientApp.Wpf.Views;
using Deve.ClientApp.Wpf.Resources.Strings;
using Deve.ClientApp.Wpf.Helpers;
using Deve.ClientApp.Wpf.Models;
using Deve.ClientApp.Wpf.Interfaces;

namespace Deve.ClientApp.Wpf.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Fields
        private ListControlData _ctrlDataClients;
        private ListControlData _ctrlDataCities;
        private ListControlData _ctrlDataStates;
        private ListControlData _ctrlDataCountries;
        private bool _isLoadingClientStats = false;
        private ClientStats? _clientStats;

        private ICommand? _addStateCommand;
        private ICommand? _editStateCommand;
        private ICommand? _deleteStateCommand;
        private ICommand? _addCountryCommand;
        private ICommand? _editCountryCommand;
        private ICommand? _deleteCountryCommand;
        #endregion

        #region Properties
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
        public MainViewModel(INavigationService navigationService, IDataService dataService)
            : base(navigationService, dataService)
        {
            _ctrlDataClients = new(LoadDataClients);
            _ctrlDataCities = new(LoadDataCities);
            _ctrlDataStates = new(LoadDataStates);
            _ctrlDataCountries = new(LoadDataCountries);
            _ = LoadData();
        }
        #endregion

        #region Methods
        private async Task LoadData()
        {
            await LoadDataClients();
            await LoadDataCities();
            await LoadDataStates();
            await LoadDataCountries();
            await LoadClientStats();
        }

        private async Task LoadDataClients()
        {
            var criteria = new CriteriaClient()
            {
                Name = _ctrlDataClients.SearchText,
            };
            await LoadDataList(_ctrlDataClients, DataService.Data.Clients, criteria, x => new ListDataClient()
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
            await LoadDataList(_ctrlDataCities, DataService.Data.Cities, criteria, x => new ListData()
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
            await LoadDataList(_ctrlDataStates, DataService.Data.States, criteria, x => new ListData()
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
            await LoadDataList(_ctrlDataCountries, DataService.Data.Countries, criteria, x => new ListData()
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
                var res = await DataService.Data.Stats.GetClientStats();
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
                await LoadDataStates();
        }

        private async Task DoEditState(ListData? listData)
        {
            await DoEdit(listData, _ctrlDataStates, DataService.Data.States, (o) =>
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
            await DoDelete(listData, AppResources.ConfirmDeleteState, _ctrlDataStates, DataService.Data.States, LoadDataStates);
        }

        private async Task DoAddCountry()
        {
            if (NavigationService.NavigateModalTo<CountryView>())
                await LoadDataCountries();
        }

        private async Task DoEditCountry(ListData? listData)
        {
            await DoEdit(listData, _ctrlDataCountries, DataService.Data.Countries, (o) =>
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
            await DoDelete(listData, AppResources.ConfirmDeleteCountry, _ctrlDataCountries, DataService.Data.Countries, LoadDataCountries);
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
                        Globals.ShowError(res.Errors);
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
                if (Globals.ShowQuestion(string.Format(message, listData.Main), AppResources.Delete))
                {
                    data.IsBusy = true;
                    try
                    {
                        var res = await dataAll.Delete(listData.Id);
                        if (!res.Success)
                        {
                            Globals.ShowError(res.Errors);
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
        public ICommand AddState => _addStateCommand ??= new Command(() => _ = DoAddState(), () => !_ctrlDataStates.IsBusy);
        public ICommand EditState => _editStateCommand ??= new Command((listData) => _ = DoEditState((ListData?)listData), () => !_ctrlDataStates.IsBusy);
        public ICommand DeleteState => _deleteStateCommand ??= new Command((listData) => _ = DoDeleteState((ListData?)listData), () => !_ctrlDataStates.IsBusy);

        public ICommand AddCountry => _addCountryCommand ??= new Command(() => _ = DoAddCountry(), () => !_ctrlDataCountries.IsBusy);
        public ICommand EditCountry => _editCountryCommand ??= new Command((listData) => _ = DoEditCountry((ListData?)listData), () => !_ctrlDataCountries.IsBusy);
        public ICommand DeleteCountry => _deleteCountryCommand ??= new Command((listData) => _ = DoDeleteCountry((ListData?)listData), () => !_ctrlDataCountries.IsBusy);
        #endregion
    }
}
