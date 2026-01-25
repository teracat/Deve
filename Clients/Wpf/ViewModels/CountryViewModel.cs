using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Resources.Strings;
using Deve.Dto.Responses.Results;
using Deve.Customers.Countries;

namespace Deve.Clients.Wpf.ViewModels;

internal sealed class CountryViewModel : BaseEditViewModel, INavigationAwareWithType<CountryResponse>
{
    #region Fields
    private CountryResponse? _country;
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
    public CountryViewModel(INavigationService navigationService, Data.IData data, IMessageHandler messageHandler)
        : base(navigationService, data, messageHandler)
    {
    }
    #endregion

    #region Overrides
    protected override async Task GetData()
    {
        if (_country is null)
        {
            if (Id == Guid.Empty)
            {
                _country = new CountryResponse
                {
                    Id = Guid.Empty,
                    Name = string.Empty,
                    IsoCode = string.Empty
                };
            }
            else
            {
                var res = await Data.Customers.Countries.GetByIdAsync(Id);
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
            IResult res;
            if (_country.Id == Guid.Empty)
            {
                var request = new CountryAddRequest
                {
                    Name = Name!.Trim(),
                    IsoCode = IsoCode!.Trim()
                };
                res = await Data.Customers.Countries.AddAsync(request);
            }
            else
            {
                var request = new CountryUpdateRequest
                {
                    Name = Name!.Trim(),
                    IsoCode = IsoCode!.Trim()
                };
                res = await Data.Customers.Countries.UpdateAsync(_country.Id, request);
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
    public void OnNavigatedToWithType(CountryResponse parameter)
    {
        _country = parameter;
        _ = LoadData();
    }
    #endregion
}
