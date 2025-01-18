using System.Windows.Input;
using Deve.ClientApp.Wpf.Resources.Strings;
using Deve.ClientApp.Wpf.Window;

namespace Deve.ClientApp.Wpf.ViewModel
{
    public class CountryViewModel : BaseViewModel
    {
        #region Fields
        private CountryWindow _countryWindow;
        private Country _country;
        private string _name;
        private string _isoCode;

        private ICommand? _saveCommand;
        private ICommand? _cancelCommand;
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
            _countryWindow = window;
            _country = country;
            _name = _country.Name;
            _isoCode = _country.IsoCode;
        }
        #endregion

        #region Methods
        internal void DoCancel()
        {
            Window.DialogResult = false;
            Window.Close();
        }

        internal async Task DoSave()
        {
            if (Utils.SomeIsNullOrWhiteSpace(_name,_isoCode))
            {
                Globals.ShowError(AppResources.MissingName);
                return;
            }

            IsBusy = true;
            try
            {
                _country.Name = _name;
                _country.IsoCode = _isoCode;

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

                Window.DialogResult = true;
                Window.Close();
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion

        #region Commands
        public ICommand Cancel => _cancelCommand ??= new Command(() => DoCancel(), () => IsIdle);
        public ICommand Save => _saveCommand ??= new Command(() => _ = DoSave(), () => IsIdle);
        #endregion
    }
}