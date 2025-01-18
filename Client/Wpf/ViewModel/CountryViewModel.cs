using System.Windows.Input;
using Deve.ClientApp.Wpf.Resources.Strings;
using Deve.ClientApp.Wpf.Window;

namespace Deve.ClientApp.Wpf.ViewModel
{
    public class CountryViewModel : BaseEditViewModel
    {
        #region Fields
        private Country _country;
        private string _name;
        private string _isoCode;
        #endregion

        #region Properties
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public string IsoCode
        {
            get => _isoCode;
            set => SetProperty(ref _isoCode, value);
        }
        #endregion

        #region Constructor
        public CountryViewModel(CountryWindow window, Country country)
            : base(window)
        {
            _country = country;
            _name = _country.Name;
            _isoCode = _country.IsoCode;
        }
        #endregion

        #region Overrides
        internal async override Task DoSave()
        {
            if (Utils.SomeIsNullOrWhiteSpace(_name,_isoCode))
            {
                Globals.ShowError(AppResources.MissingField);
                return;
            }

            IsBusy = true;
            try
            {
                _country.Name = _name.Trim();
                _country.IsoCode = _isoCode.Trim();

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

            Window.DialogResult = true;
            Window.Close();
        }
        #endregion
    }
}