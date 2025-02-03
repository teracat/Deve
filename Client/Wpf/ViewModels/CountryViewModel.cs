using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using Deve.ClientApp.Wpf.Resources.Strings;

namespace Deve.ClientApp.Wpf.ViewModels
{
    public partial class CountryViewModel : BaseEditViewModel
    {
        #region Fields
        private readonly Country _country;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingName))]
        private string _name;

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingIsoCode))]
        [MinLength(2, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MinLengthIsoCode))]
        private string _isoCode;
        #endregion

        #region Constructor
        public CountryViewModel(Country country)
        {
            _country = country;
            _name = _country.Name;
            _isoCode = _country.IsoCode;
        }
        #endregion

        #region Override
        internal async override Task Save()
        {
            if (!Validate())
                return;

            IsBusy = true;
            try
            {
                _country.Name = Name.Trim();
                _country.IsoCode = IsoCode.Trim();

                Result res;
                if (_country.Id == 0)
                    res = await Globals.Data.Countries.Add(_country);
                else
                    res = await Globals.Data.Countries.Update(_country);

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
    }
}