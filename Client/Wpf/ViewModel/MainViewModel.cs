using System.Windows;
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
            _ctrlDataClients.IsBusy = true;
            try
            {
                var criteria = new CriteriaClient()
                {
                    Name = _ctrlDataClients.SearchText,
                };
                var res = await Globals.Data.Clients.Get(criteria);
                if (!res.Success)
                {
                    _ctrlDataClients.ErrorText = Utils.ErrorsToString(res.Errors);
                    return;
                }

                _ctrlDataClients.Items = res.Data.Select(x => new ListData()
                {
                    Id = x.Id,
                    Main = x.DisplayName,
                    Detail = x.Location.City + ", " + x.Location.State + " (" + x.Location.Country + ")",
                }).ToList();
            }
            finally
            {
                _ctrlDataClients.IsBusy = false;
            }
        }

        private async Task LoadDataCities()
        {
            _ctrlDataCities.IsBusy = true;
            try
            {
                var criteria = new CriteriaCity()
                {
                    Name = _ctrlDataCities.SearchText,
                };
                var res = await Globals.Data.Cities.Get(criteria);
                if (!res.Success)
                {
                    _ctrlDataCities.ErrorText = Utils.ErrorsToString(res.Errors);
                    return;
                }

                _ctrlDataCities.Items = res.Data.Select(x => new ListData()
                {
                    Id = x.Id,
                    Main = x.Name,
                    Detail = x.State,
                }).ToList();
            }
            finally
            {
                _ctrlDataCities.IsBusy = false;
            }
        }

        private async Task LoadDataStates()
        {
            _ctrlDataStates.IsBusy = true;
            try
            {
                var criteria = new CriteriaState()
                {
                    Name = _ctrlDataStates.SearchText,
                };
                var res = await Globals.Data.States.Get(criteria);
                if (!res.Success)
                {
                    _ctrlDataStates.ErrorText = Utils.ErrorsToString(res.Errors);
                    return;
                }

                _ctrlDataStates.Items = res.Data.Select(x => new ListData()
                {
                    Id = x.Id,
                    Main = x.Name,
                    Detail = x.Country,
                }).ToList();
            }
            finally
            {
                _ctrlDataStates.IsBusy = false;
            }
        }

        private async Task LoadDataCountries()
        {
            _ctrlDataCountries.IsBusy = true;
            try
            {
                var criteria = new CriteriaCountry()
                {
                    Name = _ctrlDataCountries.SearchText,
                };
                var res = await Globals.Data.Countries.Get(criteria);
                if (!res.Success)
                {
                    _ctrlDataCountries.ErrorText = Utils.ErrorsToString(res.Errors);
                    return;
                }

                _ctrlDataCountries.Items = res.Data.Select(x => new ListData()
                {
                    Id = x.Id,
                    Main = x.Name,
                }).ToList();
            }
            finally
            {
                _ctrlDataCountries.IsBusy = false;
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
        #endregion
    }
}
