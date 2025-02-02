using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using Deve.ClientApp.Wpf.Helpers;
using Deve.ClientApp.Wpf.Resources.Strings;

namespace Deve.ClientApp.Wpf.ViewModels
{
    public partial class StateViewModel : BaseEditViewModel
    {
        #region Fields
        [ObservableProperty]
        private State _state;

        [ObservableProperty]
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingName))]
        private string _name;

        [ObservableProperty]
        private IList<Country>? _countries;

        [ObservableProperty]
        [GreaterThanOrEqual(nameof(SelectedCountry.Id), 0, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingCountry))]
        private Country? _selectedCountry;
        #endregion

        #region Constructor
        public StateViewModel(State state)
        {
            State = state;
            Name = _state.Name;
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
                State.Name = Name.Trim();
                State.CountryId = SelectedCountry!.Id;
                State.Country = SelectedCountry.Name;

                Result res;
                if (State.Id == 0)
                    res = await Globals.Data.States.Add(State);
                else
                    res = await Globals.Data.States.Update(State);

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
                if (State.CountryId > 0)
                    SelectedCountry = Countries?.FirstOrDefault(x => x.Id == State.CountryId);
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}