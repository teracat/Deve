using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using Deve.ClientApp.Wpf.Resources.Strings;

namespace Deve.ClientApp.Wpf.ViewModels
{
    public partial class CountryViewModel : BaseEditViewModel
    {
        #region Fields
        [ObservableProperty]
        private Country _country;

        [ObservableProperty]
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingName))]
        private string _name;

        [ObservableProperty]
        [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingIsoCode))]
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
                Country.Name = Name.Trim();
                Country.IsoCode = IsoCode.Trim();

                Result res;
                if (Country.Id == 0)
                    res = await Globals.Data.Countries.Add(Country);
                else
                    res = await Globals.Data.Countries.Update(Country);

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