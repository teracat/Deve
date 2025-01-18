using System.Windows;
using System.Windows.Input;
using Deve.Internal;
using Deve.ClientApp.Wpf.Window;

namespace Deve.ClientApp.Wpf.ViewModel
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
        private ICommand? _addCountryCommand;
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
            set
            {
                if (SetProperty(ref _isLoadingClientStats, value))
                    OnPropertyChanged(nameof(IsLoadingClientStatsVisibility));
            }
        }
        public Visibility IsLoadingClientStatsVisibility => _isLoadingClientStats ? Visibility.Visible : Visibility.Collapsed;
        public ClientStats? ClientStats
        {
            get => _clientStats;
            set => SetProperty(ref _clientStats, value);
        }
        #endregion

        #region Constructor
        public MainViewModel(MainWindow window)
            : base(window)
        {
            _ctrlDataClients = new(LoadDataClients);
            _ctrlDataCities = new(LoadDataCities);
            _ctrlDataStates = new(LoadDataStates);
            _ctrlDataCountries = new(LoadDataCountries);
        }
        #endregion

        #region Overrides
        public override void OnWindowLoaded()
        {
            base.OnWindowLoaded();
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
            await LoadDataList(_ctrlDataClients, Globals.Data.Clients, criteria, x => new ListDataClient()
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
            await LoadDataList(_ctrlDataCities, Globals.Data.Cities, criteria, x => new ListData()
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
            await LoadDataList(_ctrlDataStates, Globals.Data.States, criteria, x => new ListData()
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
            await LoadDataList(_ctrlDataCountries, Globals.Data.Countries, criteria, x => new ListData()
            {
                Id = x.Id,
                Main = x.Name,
                Detail = x.IsoCode,
            });
        }

        private async Task LoadDataList<ModelList, Model, Criteria>(ListControlData data, IDataAll<ModelList, Model, Criteria> dataAll, Criteria criteria, Func<ModelList, ListData> selector) where ModelList: ModelId where Model: ModelId where Criteria : CriteriaId
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

        private async Task LoadClientStats()
        {
            IsLoadingClientStats = true;
            try
            {
                var res = await Globals.Data.Stats.GetClientStats();
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

        }

        private async Task DoAddCountry()
        {
            var wnd = new CountryWindow(new Country());
            if (wnd.ShowDialog() == true)
                await LoadDataCountries();
        }
        #endregion

        #region Commands
        public ICommand AddState => _addStateCommand ??= new Command(() => _ = DoAddState(), () => !_ctrlDataStates.IsBusy);
        public ICommand AddCountry => _addCountryCommand ??= new Command(() => _ = DoAddCountry(), () => !_ctrlDataCountries.IsBusy);
        #endregion
    }
}
