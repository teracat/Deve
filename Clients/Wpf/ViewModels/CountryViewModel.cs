using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using ReactiveUI.Validation.Extensions;
using Deve.Model;
using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Resources.Strings;

namespace Deve.Clients.Wpf.ViewModels
{
    public partial class CountryViewModel : BaseEditViewModel, INavigationAwareWithType<Country>
    {
        #region Fields
        private Country? _country;

        [Reactive]
        private string _name = string.Empty;

        [Reactive]
        private string _isoCode = string.Empty;
        #endregion

        #region Constructor
        public CountryViewModel(INavigationService navigationService, Internal.Data.IData data, IMessageHandler messageHandler, ISchedulerProvider scheduler)
            : base(navigationService, data, messageHandler, scheduler)
        {
            // Validation Rules
            this.ValidationRule(vm => vm.Name,
                                this.WhenAnyValue(vm => vm.ShouldValidate, vm => vm.Name,
                                                  (shouldValidate, name) => !shouldValidate || !string.IsNullOrWhiteSpace(name)),
                                AppResources.MissingName);
            this.ValidationRule(vm => vm.IsoCode,
                                this.WhenAnyValue(vm => vm.ShouldValidate, vm => vm.IsoCode,
                                                  (shouldValidate, isoCode) => !shouldValidate || !string.IsNullOrWhiteSpace(isoCode)),
                                AppResources.MissingIsoCode);
            this.ValidationRule(vm => vm.IsoCode,
                                this.WhenAnyValue(vm => vm.ShouldValidate, vm => vm.IsoCode,
                                                  (shouldValidate, isoCode) => !shouldValidate || isoCode.Length >= 2),
                                AppResources.MinLengthIsoCode);
        }
        #endregion

        #region Overrides
        protected async override Task<Result> GetData()
        {
            if (_country is null)
            {
                if (Id <= 0)
                {
                    _country = new Country();
                }
                else
                {
                    var res = await Data.Countries.Get(Id);
                    if (!res.Success || res.Data is null)
                    {
                        return res;
                    }

                    _country = res.Data;
                }
            }

            // Only assign values if it's not a new country to avoid validation errors
            if (_country.Id > 0)
            {
                Name = _country!.Name;
                IsoCode = _country!.IsoCode;
            }

            return Utils.ResultOk();
        }

        protected async override Task<Result> Save()
        {
            if (_country is null)
            {
                return Utils.ResultError();
            }

            if (!Validate())
            {
                return Utils.ResultError();
            }

            _country.Name = Name.Trim();
            _country.IsoCode = IsoCode.Trim();

            Result res;
            if (_country.Id == 0)
            {
                res = await Data.Countries.Add(_country);
            }
            else
            {
                res = await Data.Countries.Update(_country);
            }

            return res;
        }
        #endregion

        #region INavigationAwareWithType
        public void OnNavigatedToWithType(Country parameter)
        {
            _country = parameter;
            _ = LoadCommand.Execute().Subscribe();
        }
        #endregion
    }
}