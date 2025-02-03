using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using Deve.ClientApp.Wpf.Helpers;
using Deve.ClientApp.Wpf.Resources.Strings;

namespace Deve.ClientApp.Wpf.ViewModels
{
    public partial class StateViewModel : BaseEditViewModel
    {
        #region Fields
        private readonly State _state;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingName))]
        private string _name;

        [ObservableProperty]
        private IList<Country>? _countries;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [GreaterThanOrEqual(nameof(SelectedCountry.Id), 1, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingCountry))]
        private Country? _selectedCountry;
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
        internal async override Task Save()
        {
            if (!Validate())
                return;

            IsBusy = true;
            try
            {
                _state.Name = Name.Trim();
                _state.CountryId = SelectedCountry!.Id;
                _state.Country = SelectedCountry.Name;

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
                    SelectedCountry = Countries?.FirstOrDefault(x => x.Id == _state.CountryId);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}