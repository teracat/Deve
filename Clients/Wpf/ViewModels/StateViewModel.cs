using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Resources.Strings;
using Deve.Model;

namespace Deve.Clients.Wpf.ViewModels
{
    public class StateViewModel : BaseEditViewModel, INavigationAwareWithType<State>
    {
        #region Fields
        private State? _state;
        #endregion

        #region Properties
        public string? Name
        {
            get;
            set => SetProperty(ref field, value);
        }

        public IList<Country>? Countries
        {
            get;
            set => SetProperty(ref field, value);
        }

        public Country? SelectedCountry
        {
            get;
            set => SetProperty(ref field, value);
        }
        #endregion

        #region Constructor
        public StateViewModel(INavigationService navigationService, Internal.Data.IData data, IMessageHandler messageHandler)
            : base(navigationService, data, messageHandler)
        {
        }
        #endregion

        #region Overrides
        protected override async Task GetData()
        {
            await GetDataState();
            await GetDataCountries();
        }

        internal override async Task DoSave()
        {
            if (_state is null)
            {
                return;
            }

            if (Utils.SomeIsNullOrWhiteSpace(Name) || SelectedCountry is null || SelectedCountry.Id <= 0)
            {
                MessageHandler.ShowError(AppResources.MissingField);
                return;
            }

            IsBusy = true;
            try
            {
                _state.Name = Name!.Trim();
                _state.CountryId = SelectedCountry.Id;
                _state.Country = SelectedCountry.Name;

                Result res;
                if (_state.Id == 0)
                {
                    res = await Data.States.Add(_state);
                }
                else
                {
                    res = await Data.States.Update(_state);
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
                    var res = await Data.States.Get(Id);
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

            Name = _state.Name;
        }

        private async Task GetDataCountries()
        {
            var res = await Data.Countries.Get();
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