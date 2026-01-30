using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Resources.Strings;
using Deve.Dto.Responses.Results;
using Deve.Customers.Countries;

namespace Deve.Clients.Wpf.ViewModels;

internal sealed partial class CountryViewModel : BaseEditViewModel, INavigationAwareWithType<CountryResponse>
{
    #region Fields
    private CountryResponse? _country;
    #endregion

    #region Properties
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

        // Only assign values if it's not a new country to avoid validation errors
        if (_country.Id != Guid.Empty)
        {
            Name = _country.Name;
            IsoCode = _country.IsoCode;
        }
    }

    internal override async Task Save()
    {
        if (_country is null)
        {
            return;
        }

        if (Utils.SomeIsNullOrWhiteSpace(Name, IsoCode))
        {
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
