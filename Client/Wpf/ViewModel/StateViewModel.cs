using Deve.ClientApp.Wpf.Resources.Strings;
using Deve.ClientApp.Wpf.Window;

namespace Deve.ClientApp.Wpf.ViewModel
{
    public class StateViewModel : BaseEditViewModel
    {
        #region Fields
        private State _state;
        private string _name;
        private IList<Country>? _countries;
        private Country? _selectedCountry;
        #endregion

        #region Properties
        public string Name
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
        public StateViewModel(StateWindow window, State state)
            : base(window)
        {
            _state = state;
            _name = _state.Name;
        }
        #endregion

        #region Overrides
        public override void OnWindowLoaded()
        {
            base.OnWindowLoaded();
            _ = LoadCountries();
        }

        internal async override Task DoSave()
        {
            IsBusy = true;
            try
            {
                _state.Name = _name.Trim();
                _state.CountryId = _selectedCountry?.Id ?? 0;
                _state.Country = _selectedCountry?.Name ?? string.Empty;

                Result res;
                if (_state.Id == 0)
                    res = await Globals.Data.States.Add(_state);
                else
                    res = await Globals.Data.States.Update(_state);

                if (!res.Success)
                {
                    Globals.ShowError(res.Errors);
                    return;
                }

                await Task.Delay(5000);
            }
            finally
            {
                IsBusy = false;
            }

            Window.DialogResult = true;
            Window.Close();
        }
        #endregion

        #region Methods

        private async Task LoadCountries()
        {
            IsBusy = true;
            try
            {
                var res = await Globals.Data.Countries.Get();
                if (!res.Success)
                {
                    Globals.ShowError(res.Errors);
                    IsBusy = false; // When IsBusy=true the Window will not be closed
                    Window.Close();
                    return;
                }

                Countries = res.Data;
                if (_state.CountryId > 0)
                    SelectedCountry = _countries?.FirstOrDefault(x => x.Id == _state.CountryId);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}