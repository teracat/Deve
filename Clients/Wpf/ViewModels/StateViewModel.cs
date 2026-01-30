using ReactiveUI;
using ReactiveUI.SourceGenerators;
using ReactiveUI.Validation.Extensions;
using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Resources.Strings;
using Deve.Customers.States;
using Deve.Dto.Responses.Results;
using Deve.Customers.Countries;

namespace Deve.Clients.Wpf.ViewModels;

internal sealed partial class StateViewModel : BaseEditViewModel, INavigationAwareWithType<StateResponse>
{
    #region Fields
    private StateResponse? _state;

    [Reactive]
    private string? _name;

    [Reactive]
    private IReadOnlyList<CountryResponse>? _countries;

    [Reactive]
    private CountryResponse? _selectedCountry;
    #endregion

    #region Constructor
    public StateViewModel(INavigationService navigationService, Data.IData data, IMessageHandler messageHandler, ISchedulerProvider scheduler)
        : base(navigationService, data, messageHandler, scheduler)
    {
        // Validation Rules
        _ = this.ValidationRule(vm => vm.Name,
                            this.WhenAnyValue(vm => vm.ShouldValidate, vm => vm.Name,
                                                (shouldValidate, name) => !shouldValidate || !string.IsNullOrWhiteSpace(name)),
                            AppResources.MissingName);

        _ = this.ValidationRule(vm => vm.SelectedCountry,
                            this.WhenAnyValue(vm => vm.ShouldValidate, vm => vm.SelectedCountry,
                                                (shouldValidate, selectedCountry) => !shouldValidate || (selectedCountry is not null && selectedCountry.Id != Guid.Empty)),
                            AppResources.MissingCountry);
    }
    #endregion

    #region Overrides
    protected override async Task<IResult> GetData()
    {
        var taskState = GetDataState();
        var taskCountries = GetDataCountries();

        _ = await Task.WhenAll(taskState, taskCountries);

        var resState = await taskState;
        var resCountry = await taskCountries;

        // If GetDataState failed, return its result
        if (!resState.Success)
        {
            return resState;
        }

        // If GetDataCountries failed, return its result
        if (!resCountry.Success)
        {
            return resCountry;
        }

        SetSelectedCountry();
        return Result.Ok();
    }

    protected override async Task<IResult> Save()
    {
        if (_state is null)
        {
            return Result.Fail(ResultErrorType.MissingRequiredField, null);
        }

        if (!Validate())
        {
            return Result.Fail(ResultErrorType.MissingRequiredField, null);
        }

        IResult res;
        if (_state.Id == Guid.Empty)
        {
            var request = new StateAddRequest
            {
                Name = Name!.Trim(),
                CountryId = SelectedCountry!.Id
            };
            res = await Data.Customers.States.AddAsync(request);
        }
        else
        {
            var request = new StateUpdateRequest
            {
                Name = Name!.Trim(),
                CountryId = SelectedCountry!.Id
            };
            res = await Data.Customers.States.UpdateAsync(_state.Id, request);
        }

        return res;
    }
    #endregion

    #region Methods
    private async Task<IResult> GetDataState()
    {
        if (_state is null)
        {
            if (Id == Guid.Empty)
            {
                _state = new StateResponse
                {
                    Id = Guid.Empty,
                    Name = string.Empty,
                    CountryId = Guid.Empty,
                    CountryName = string.Empty
                };
            }
            else
            {
                var res = await Data.Customers.States.GetByIdAsync(Id);
                if (!res.Success || res.Data is null)
                {
                    MessageHandler.ShowError(res.Errors);
                    IsBusy = false; // When IsBusy=true the Window will not be closed
                    Close();
                    return res;
                }

                _state = res.Data;
            }
        }

        // Only assign values if it's not a new state to avoid validation errors
        if (_state.Id != Guid.Empty)
        {
            Name = _state.Name;
        }

        return Result.Ok();
    }

    private async Task<IResult> GetDataCountries()
    {
        var res = await Data.Customers.Countries.GetAsync();
        if (!res.Success)
        {
            return res;
        }

        Countries = res.Data;

        return Result.Ok();
    }

    private void SetSelectedCountry()
    {
        if (_state is not null && _state.CountryId != Guid.Empty)
        {
            SelectedCountry = Countries?.FirstOrDefault(x => x.Id == _state.CountryId);
        }
    }
    #endregion

    #region INavigationAwareWithType
    public void OnNavigatedToWithType(StateResponse parameter)
    {
        _state = parameter;
        _ = LoadCommand.Execute().Subscribe();
    }
    #endregion
}