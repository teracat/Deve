using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using Deve.Model;
using Deve.Clients.Wpf.Helpers;
using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Resources.Strings;

namespace Deve.Clients.Wpf.ViewModels
{
    public partial class StateViewModel : BaseEditViewModel, INavigationAwareWithType<State>
    {
        #region Fields
        private State? _state;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingName))]
        private string? _name;

        [ObservableProperty]
        private IList<Country>? _countries;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [GreaterThanOrEqual(nameof(SelectedCountry.Id), 1, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingCountry))]
        private Country? _selectedCountry;
        #endregion

        #region Constructor
        public StateViewModel(INavigationService navigationService, IDataService dataService, IMessageHandler messageHandler)
            : base(navigationService, dataService, messageHandler)
        {
        }
        #endregion

        #region Overrides
        protected async override Task GetData()
        {
            await GetDataState();
            await GetDataCountries();
        }

        internal async override Task Save()
        {
            if (_state is null)
            {
                return;
            }

            if (!Validate())
            {
                return;
            }

            IsBusy = true;
            try
            {
                _state.Name = Name!.Trim();
                _state.CountryId = SelectedCountry!.Id;
                _state.Country = SelectedCountry.Name;

                Result res;
                if (_state.Id == 0)
                {
                    res = await DataService.Data.States.Add(_state);
                }
                else
                {
                    res = await DataService.Data.States.Update(_state);
                }

                if (!res.Success)
                {
                    MessageHandler.ShowError(res.Errors);
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
        private async Task GetDataState()
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
                        MessageHandler.ShowError(res.Errors);
                        IsBusy = false; // When IsBusy=true the Window will not be closed
                        Close();
                        return;
                    }

                    _state = res.Data;
                }
            }

            // Only assign values if it's not a new state to avoid validation errors
            if (_state.Id > 0)
            {
                Name = _state.Name;
            }
        }

        private async Task GetDataCountries()
        {
            var res = await DataService.Data.Countries.Get();
            if (!res.Success)
            {
                MessageHandler.ShowError(res.Errors);
                IsBusy = false; // When IsBusy=true the Window will not be closed
                Close();
                return;
            }

            Countries = res.Data;
            if (_state is not null && _state.CountryId > 0)
            {
                SelectedCountry = Countries?.FirstOrDefault(x => x.Id == _state.CountryId);
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