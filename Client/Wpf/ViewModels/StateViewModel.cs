﻿using Deve.ClientApp.Wpf.Resources.Strings;
using Deve.ClientApp.Wpf.Views;

namespace Deve.ClientApp.Wpf.ViewModels
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
        public StateViewModel(State state)
        {
            _state = state;
            _name = _state.Name;
            _ = LoadCountries();
        }
        #endregion

        #region Overrides
        internal async override Task DoSave()
        {
            if (Utils.SomeIsNullOrWhiteSpace(_name) || _selectedCountry is null || _selectedCountry.Id <= 0)
            {
                Globals.ShowError(AppResources.MissingField);
                return;
            }

            IsBusy = true;
            try
            {
                _state.Name = _name.Trim();
                _state.CountryId = _selectedCountry.Id;
                _state.Country = _selectedCountry.Name;

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
                var res = await Globals.Data.Countries.Get();
                if (!res.Success)
                {
                    Globals.ShowError(res.Errors);
                    IsBusy = false; // When IsBusy=true the Window will not be closed
                    Close();
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