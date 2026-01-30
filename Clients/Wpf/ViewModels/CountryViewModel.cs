using System.Reactive.Linq;
using ReactiveUI;
using ReactiveUI.SourceGenerators;
using ReactiveUI.Validation.Extensions;
using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Resources.Strings;
using Deve.Customers.Countries;
using Deve.Dto.Responses.Results;

namespace Deve.Clients.Wpf.ViewModels;

internal sealed partial class CountryViewModel : BaseEditViewModel, INavigationAwareWithType<CountryResponse>
{
    #region Fields
    private CountryResponse? _country;

    [Reactive]
    private string _name = string.Empty;

    [Reactive]
    private string _isoCode = string.Empty;
    #endregion

    #region Constructor
    public CountryViewModel(INavigationService navigationService, Data.IData data, IMessageHandler messageHandler, ISchedulerProvider scheduler)
        : base(navigationService, data, messageHandler, scheduler)
    {
        // Validation Rules
        _ = this.ValidationRule(vm => vm.Name,
                                this.WhenAnyValue(vm => vm.ShouldValidate, vm => vm.Name,
                                                    (shouldValidate, name) => !shouldValidate || !string.IsNullOrWhiteSpace(name)),
                                AppResources.MissingName);
        _ = this.ValidationRule(vm => vm.IsoCode,
                                this.WhenAnyValue(vm => vm.ShouldValidate, vm => vm.IsoCode,
                                                    (shouldValidate, isoCode) => !shouldValidate || !string.IsNullOrWhiteSpace(isoCode)),
                                AppResources.MissingIsoCode);
        _ = this.ValidationRule(vm => vm.IsoCode,
                                this.WhenAnyValue(vm => vm.ShouldValidate, vm => vm.IsoCode,
                                                    (shouldValidate, isoCode) => !shouldValidate || isoCode.Length >= 2),
                                AppResources.MinLengthIsoCode);
    }
    #endregion

    #region Overrides
    protected override async Task<IResult> GetData()
    {
        if (_country is null)
        {
            if (Id == Guid.Empty)
            {
                _country = new CountryResponse();
            }
            else
            {
                var res = await Data.Customers.Countries.GetByIdAsync(Id);
                if (!res.Success || res.Data is null)
                {
                    return res;
                }

                _country = res.Data;
            }
        }

        // Only assign values if it's not a new country to avoid validation errors
        if (_country.Id != Guid.Empty)
        {
            Name = _country!.Name;
            IsoCode = _country!.IsoCode;
        }

        return Result.Ok();
    }

    protected override async Task<IResult> Save()
    {
        if (_country is null)
        {
            return Result.Fail(ResultErrorType.MissingRequiredField, null);
        }

        if (!Validate())
        {
            return Result.Fail(ResultErrorType.MissingRequiredField, null);
        }

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

        return res;
    }
    #endregion

    #region INavigationAwareWithType
    public void OnNavigatedToWithType(CountryResponse parameter)
    {
        _country = parameter;
        _ = LoadCommand.Execute().Subscribe();
    }
    #endregion
}
