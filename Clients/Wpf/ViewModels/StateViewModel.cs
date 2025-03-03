using ReactiveUI;
using ReactiveUI.SourceGenerators;
using ReactiveUI.Validation.Extensions;
using Deve.Model;
using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Resources.Strings;

namespace Deve.Clients.Wpf.ViewModels
{
    public partial class StateViewModel : BaseEditViewModel, INavigationAwareWithType<State>
    {
        #region Fields
        private State? _state;

        [Reactive]
        private string? _name;

        [Reactive]
        private IList<Country>? _countries;

        [Reactive]
        private Country? _selectedCountry;
        #endregion

        #region Constructor
        public StateViewModel(INavigationService navigationService, Internal.Data.IData data, IMessageHandler messageHandler, ISchedulerProvider scheduler)
            : base(navigationService, data, messageHandler, scheduler)
        {
            // Validation Rules
            this.ValidationRule(vm => vm.Name,
                                this.WhenAnyValue(vm => vm.ShouldValidate, vm => vm.Name,
                                                  (shouldValidate, name) => !shouldValidate || !string.IsNullOrWhiteSpace(name)),
                                AppResources.MissingName);

            this.ValidationRule(vm => vm.SelectedCountry,
                                this.WhenAnyValue(vm => vm.ShouldValidate, vm => vm.SelectedCountry,
                                                  (shouldValidate, selectedCountry) => !shouldValidate || (selectedCountry is not null && selectedCountry.Id > 0)),
                                AppResources.MissingCountry);
        }
        #endregion

        #region Overrides
        protected async override Task<Result> GetData()
        {
            var taskState = GetDataState();
            var taskCountries = GetDataCountries();

            await Task.WhenAll(taskState, taskCountries);

            var resState = await taskState;
            var resCountry = await taskCountries;

            // If GetDataState failed, return its result
            if (!resState.Success)
            {
                return resState;
            }

            // Otherwise, return resCountry
            return resCountry;
        }

        protected async override Task<Result> Save()
        {
            if (_state is null)
            {
                return Utils.ResultError();
            }

            if (!Validate())
            {
                return Utils.ResultError();
            }

            _state.Name = Name!.Trim();
            _state.CountryId = SelectedCountry!.Id;
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

            return res;
        }
        #endregion

        #region Methods
        private async Task<Result> GetDataState()
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
                        return res;
                    }

                    _state = res.Data;
                }
            }

            // Only assign values if it's not a new state to avoid validation errors
            if (_state.Id > 0)
            {
                Name = _state.Name;
            }

            return Utils.ResultOk();
        }

        private async Task<Result> GetDataCountries()
        {
            var res = await Data.Countries.Get();
            if (!res.Success)
            {
                return res;
            }

            Countries = res.Data;
            if (_state is not null && _state.CountryId > 0)
            {
                SelectedCountry = Countries?.FirstOrDefault(x => x.Id == _state.CountryId);
            }

            return Utils.ResultOk();
        }
        #endregion

        #region INavigationAwareWithType
        public void OnNavigatedToWithType(State parameter)
        {
            _state = parameter;
            LoadCommand.Execute().Subscribe();
        }
        #endregion
    }
}