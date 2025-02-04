using Deve.ClientApp.Wpf.Interfaces;
using Deve.ClientApp.Wpf.Resources.Strings;

namespace Deve.ClientApp.Wpf.ViewModels
{
    public class StateViewModel : BaseEditViewModel, INavigationAwareWithType<State>
    {
        #region Fields
        private State? _state;
        private string? _name;
        private IList<Country>? _countries;
        private Country? _selectedCountry;
        #endregion

        #region Properties
        public string? Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public IList<Country>? Countries
        {
            get => _countries;
            set => SetProperty(ref _countries, value);
        }

        public Country? SelectedCountry
        {
            get => _selectedCountry;
            set => SetProperty(ref _selectedCountry, value);
        }
        #endregion

        #region Constructor
        public StateViewModel(INavigationService navigationService, IDataService dataService)
            : base(navigationService, dataService)
        {
        }
        #endregion

        #region Overrides
        protected async override Task LoadData()
        {
            IsBusy = true;
            try
            {
                if (_state is null)
                {
                    if (Id <= 0)
                    {
                        _state = new State();
                    }
                    else
                    {
                        var res = await DataService.Data.States.Get(Id);
                        if (!res.Success || res.Data is null)
                        {
                            Globals.ShowError(res.Errors);
                            IsBusy = false; // When IsBusy=true the Window will not be closed
                            Close();
                            return;
                        }

                        _state = res.Data;
                    }
                }

                Name = _state.Name;
                await LoadCountries();
            }
            finally
            {
                IsBusy = false;
            }
        }

        internal async override Task DoSave()
        {
            if (_state is null)
                return;

            if (Utils.SomeIsNullOrWhiteSpace(_name) || _selectedCountry is null || _selectedCountry.Id <= 0)
            {
                Globals.ShowError(AppResources.MissingField);
                return;
            }

            IsBusy = true;
            try
            {
                _state.Name = _name!.Trim();
                _state.CountryId = _selectedCountry.Id;
                _state.Country = _selectedCountry.Name;

                Result res;
                if (_state.Id == 0)
                    res = await DataService.Data.States.Add(_state);
                else
                    res = await DataService.Data.States.Update(_state);

                if (!res.Success)
                {
                    Globals.ShowError(res.Errors);
                    return;
                }
            }
            finally
            {
                IsBusy = false;
            }

            SetResult(true);
            Close();
        }
        #endregion

        #region Methods
        private async Task LoadCountries()
        {
            IsBusy = true;
            try
            {
                var res = await DataService.Data.Countries.Get();
                if (!res.Success)
                {
                    Globals.ShowError(res.Errors);
                    IsBusy = false; // When IsBusy=true the Window will not be closed
                    Close();
                    return;
                }

                Countries = res.Data;
                if (_state is not null && _state.CountryId > 0)
                    SelectedCountry = _countries?.FirstOrDefault(x => x.Id == _state.CountryId);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

        #region INavigationAwareWithType
        public void OnNavigatedToWithType(State parameter)
        {
            _state = parameter;
            _ = LoadData();
        }
        #endregion
    }
}