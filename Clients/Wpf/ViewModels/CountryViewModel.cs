using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Resources.Strings;
using Deve.Model;

namespace Deve.Clients.Wpf.ViewModels
{
    public class CountryViewModel : BaseEditViewModel, INavigationAwareWithType<Country>
    {
        #region Fields
        private Country? _country;
        #endregion

        #region Properties
        public string? Name
        {
            get;
            set => SetProperty(ref field, value);
        }
        public string? IsoCode
        {
            get;
            set => SetProperty(ref field, value);
        }
        #endregion

        #region Constructor
        public CountryViewModel(INavigationService navigationService, Internal.Data.IData data, IMessageHandler messageHandler)
            : base(navigationService, data, messageHandler)
        {
        }
        #endregion

        #region Overrides
        protected override async Task GetData()
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
                        MessageHandler.ShowError(res.Errors);
                        Close();
                        return;
                    }

                    _country = res.Data;
                }
            }

            Name = _country.Name;
            IsoCode = _country.IsoCode;
        }

        internal override async Task DoSave()
        {
            if (_country is null)
            {
                return;
            }

            if (Utils.SomeIsNullOrWhiteSpace(Name, IsoCode))
            {
                MessageHandler.ShowError(AppResources.MissingField);
                return;
            }

            IsBusy = true;
            try
            {
                _country.Name = Name!.Trim();
                _country.IsoCode = IsoCode!.Trim();

                Result res;
                if (_country.Id == 0)
                {
                    res = await Data.Countries.Add(_country);
                }
                else
                {
                    res = await Data.Countries.Update(_country);
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

        #region INavigationAwareWithType
        public void OnNavigatedToWithType(Country parameter)
        {
            _country = parameter;
            _ = LoadData();
        }
        #endregion
    }
}