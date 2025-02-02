using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Deve.Internal;
using Deve.ClientApp.Wpf.Views;
using Deve.ClientApp.Wpf.Resources.Strings;
using Deve.ClientApp.Wpf.Models;

namespace Deve.ClientApp.Wpf.ViewModels
{
    public partial class MainViewModel : BaseViewModel
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

        #region Constructor
        public MainViewModel()
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
                Name = CtrlDataClients.SearchText,
            };
            await LoadDataList(CtrlDataClients, Globals.Data.Clients, criteria, x => new ListDataClient()
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
            await LoadDataList(CtrlDataCities, Globals.Data.Cities, criteria, x => new ListData()
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
            await LoadDataList(CtrlDataStates, Globals.Data.States, criteria, x => new ListData()
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
            await LoadDataList(CtrlDataCountries, Globals.Data.Countries, criteria, x => new ListData()
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
                var res = await Globals.Data.Stats.GetClientStats();
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
            var wnd = new StateView(new State());
            if (wnd.ShowDialog() == true)
                await LoadDataStates();
        }

        [RelayCommand(CanExecute = nameof(CtrlDataStates.IsIdle))]
        private async Task EditState(ListData? listData)
        {
            await Edit(listData, CtrlDataStates, Globals.Data.States, (o) =>
            {
                var wnd = new StateView(o);
                if (wnd.ShowDialog() == true)
                {
                    _ = LoadDataStates();
                    return true;
                }
                return false;
            });
        }

        [RelayCommand(CanExecute = nameof(CtrlDataStates.IsIdle))]
        private async Task DeleteState(ListData? listData)
        {
            await Delete(listData, AppResources.ConfirmDeleteState, CtrlDataStates, Globals.Data.States, LoadDataStates);
        }

        [RelayCommand(CanExecute = nameof(CtrlDataCountries.IsIdle))]
        private async Task AddCountry()
        {
            var wnd = new CountryView(new Country());
            if (wnd.ShowDialog() == true)
                await LoadDataCountries();
        }

        [RelayCommand(CanExecute = nameof(CtrlDataCountries.IsIdle))]
        private async Task EditCountry(ListData? listData)
        {
            await Edit(listData, CtrlDataCountries, Globals.Data.Countries, (o) =>
            {
                var wnd = new CountryView(o);
                if (wnd.ShowDialog() == true)
                {
                    _ = LoadDataCountries();
                    return true;
                }
                return false;
            });
        }

        [RelayCommand(CanExecute = nameof(CtrlDataCountries.IsIdle))]
        private async Task DeleteCountry(ListData? listData)
        {
            await Delete(listData, AppResources.ConfirmDeleteCountry, CtrlDataCountries, Globals.Data.Countries, LoadDataCountries);
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

        private async Task Edit<ModelList, Model, Criteria>(ListData? listData, ListControlData data, IDataAll<ModelList, Model, Criteria> dataAll, Func<Model, bool> editFunc) where ModelList: ModelId where Model: ModelId where Criteria : CriteriaId
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

        private async Task Delete<ModelList, Model, Criteria>(ListData? listData, string message, ListControlData data, IDataAll<ModelList, Model, Criteria> dataAll, Func<Task> actionWhenDone) where ModelList : ModelId where Model : ModelId where Criteria : CriteriaId
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
    }
}