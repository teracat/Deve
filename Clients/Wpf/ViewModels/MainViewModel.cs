using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Models;
using Deve.Clients.Wpf.Resources.Strings;
using Deve.Clients.Wpf.Views;
using Deve.Criteria;
using Deve.Internal.Criteria;
using Deve.Internal.Data;
using Deve.Internal.Model;
using Deve.Model;

namespace Deve.Clients.Wpf.ViewModels
{
    public partial class MainViewModel : BaseViewModel, IAsyncInitialization
    {
        #region Fields
        [ObservableProperty]
        private ListControlData _ctrlDataClients;

        [ObservableProperty]
        private ListControlData _ctrlDataCities;

        [ObservableProperty]
        private ListControlData _ctrlDataStates;

        [ObservableProperty]
        private ListControlData _ctrlDataCountries;

        [ObservableProperty]
        private bool _isLoadingClientStats = false;

        [ObservableProperty]
        private ClientStats? _clientStats;
        #endregion

        #region Properties
        public Task Initialization { get; private set; }
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
                Name = CtrlDataClients.SearchText,
            };
            await GetDataList(CtrlDataClients, Data.Clients, criteria, x => new ListDataClient()
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
                Name = CtrlDataCities.SearchText,
            };
            await GetDataList(CtrlDataCities, Data.Cities, criteria, x => new ListData()
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
                Name = CtrlDataStates.SearchText,
            };
            await GetDataList(CtrlDataStates, Data.States, criteria, x => new ListData()
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
                Name = CtrlDataCountries.SearchText,
            };
            await GetDataList(CtrlDataCountries, Data.Countries, criteria, x => new ListData()
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

        [RelayCommand(CanExecute = nameof(CtrlDataStates.IsIdle))]
        private async Task AddState()
        {
            if (NavigationService.NavigateModalTo<StateView>())
            {
                await LoadDataStates();
            }
        }

        [RelayCommand(CanExecute = nameof(CtrlDataStates.IsIdle))]
        private async Task EditState(ListData? listData)
        {
            await Edit(listData, CtrlDataStates, Data.States, (o) =>
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

        [RelayCommand(CanExecute = nameof(CtrlDataStates.IsIdle))]
        private async Task DeleteState(ListData? listData) => await Delete(listData, AppResources.ConfirmDeleteState, CtrlDataStates, Data.States, LoadDataStates);

        [RelayCommand(CanExecute = nameof(CtrlDataCountries.IsIdle))]
        private async Task AddCountry()
        {
            if (NavigationService.NavigateModalTo<CountryView>())
            {
                await LoadDataCountries();
            }
        }

        [RelayCommand(CanExecute = nameof(CtrlDataCountries.IsIdle))]
        private async Task EditCountry(ListData? listData)
        {
            await Edit(listData, CtrlDataCountries, Data.Countries, (o) =>
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

        [RelayCommand(CanExecute = nameof(CtrlDataCountries.IsIdle))]
        private async Task DeleteCountry(ListData? listData) => await Delete(listData, AppResources.ConfirmDeleteCountry, CtrlDataCountries, Data.Countries, LoadDataCountries);
        #endregion

        #region Generic Methods
        private async Task GetDataList<ModelList, Model, Criteria>(ListControlData data, IDataAll<ModelList, Model, Criteria> dataAll, Criteria criteria, Func<ModelList, ListData> selector) where ModelList : ModelId where Model : ModelId where Criteria : CriteriaId
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

        private async Task Edit<ModelList, Model, Criteria>(ListData? listData, ListControlData data, IDataAll<ModelList, Model, Criteria> dataAll, Func<Model, bool> editFunc) where ModelList : ModelId where Model : ModelId where Criteria : CriteriaId
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
                    _ = editFunc(obj);
                }
            }
        }

        private async Task Delete<ModelList, Model, Criteria>(ListData? listData, string message, ListControlData data, IDataAll<ModelList, Model, Criteria> dataAll, Func<Task> actionWhenDone) where ModelList : ModelId where Model : ModelId where Criteria : CriteriaId
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
    }
}