using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using Deve.ClientApp.Wpf.Interfaces;
using Deve.ClientApp.Wpf.Resources.Strings;

namespace Deve.ClientApp.Wpf.ViewModels
{
    public partial class CountryViewModel : BaseEditViewModel, INavigationAwareWithType<Country>
    {
        #region Fields
        private Country? _country;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingName))]
        private string? _name;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingIsoCode))]
        [MinLength(2, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MinLengthIsoCode))]
        private string? _isoCode;
        #endregion

        #region Constructor
        public CountryViewModel(INavigationService navigationService, IDataService dataService)
            : base(navigationService, dataService)
        {
        }
        #endregion

        #region Overrides
        protected async override Task GetData()
        {
            if (_country is null)
            {
                if (Id <= 0)
                {
                    _country = new Country();
                }
                else
                {
                    var res = await DataService.Data.Countries.Get(Id);
                    if (!res.Success || res.Data is null)
                    {
                        Globals.ShowError(res.Errors);
                        Close();
                        return;
                    }

                    _country = res.Data;
                }
            }

            Name = _country!.Name;
            IsoCode = _country!.IsoCode;
        }

        internal async override Task Save()
        {
            if (_country is null)
                return;

            if (!Validate())
                return;

            IsBusy = true;
            try
            {
                _country.Name = Name!.Trim();
                _country.IsoCode = IsoCode!.Trim();

                Result res;
                if (_country.Id == 0)
                    res = await DataService.Data.Countries.Add(_country);
                else
                    res = await DataService.Data.Countries.Update(_country);

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

        #region INavigationAwareWithType
        public void OnNavigatedToWithType(Country parameter)
        {
            _country = parameter;
            _ = LoadData();
        }
        #endregion
    }
}