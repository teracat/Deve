using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using Deve.Clients.Wpf.Helpers;
using Deve.Clients.Wpf.Interfaces;
using Deve.Clients.Wpf.Resources.Strings;
using Deve.Dto.Responses.Results;
using Deve.Customers.Countries;
using Deve.Customers.States;

namespace Deve.Clients.Wpf.ViewModels;

internal sealed partial class StateViewModel : BaseEditViewModel, INavigationAwareWithType<StateResponse>
{
    #region Fields
    private StateResponse? _state;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [Required(ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingName))]
    private string? _name;

    [ObservableProperty]
    private IReadOnlyList<CountryResponse>? _countries;

    [ObservableProperty]
    [NotifyDataErrorInfo]
    [GreaterThanOrEqual(nameof(SelectedCountry.Id), 1, ErrorMessageResourceType = typeof(AppResources), ErrorMessageResourceName = nameof(AppResources.MissingCountry))]
    private CountryResponse? _selectedCountry;
    #endregion

    #region Constructor
    public StateViewModel(INavigationService navigationService, Data.IData data, IMessageHandler messageHandler)
        : base(navigationService, data, messageHandler)
    {
    }
    #endregion

    #region Overrides
    protected override async Task GetData()
    {
        await GetDataState();
        await GetDataCountries();
    }

    internal override async Task Save()
    {
        if (_state is null)
        {
            return;
        }

        if (!Validate())
        {
            return;
        }

        IsBusy = true;
        try
        {
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

    #region Methods
    private async Task GetDataState()
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
                    return;
                }

                _state = res.Data;
            }
        }

        // Only assign values if it's not a new state to avoid validation errors
        if (_state.Id != Guid.Empty)
        {
            Name = _state.Name;
        }
    }

    private async Task GetDataCountries()
    {
        var res = await Data.Customers.Countries.GetAsync();
        if (!res.Success)
        {
            MessageHandler.ShowError(res.Errors);
            IsBusy = false; // When IsBusy=true the Window will not be closed
            Close();
            return;
        }

        Countries = res.Data;
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
        _ = LoadData();
    }
    #endregion
}
