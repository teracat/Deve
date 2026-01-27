using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Models;
using Deve.Customers.States;

namespace Deve.Clients.Maui.ViewModels;

internal sealed class StatesViewModel : ListDataViewModel
{
    #region Constructor
    public StatesViewModel(INavigationService navigationService, Data.IData data)
        : base(navigationService, data)
    {
    }
    #endregion

    #region Overrides
    protected override async Task GetListData()
    {
        StateGetListRequest? request = null;
        var res = await Data.Customers.States.GetAsync(request);
        if (!res.Success)
        {
            ErrorText = Utils.ErrorsToString(res.Errors);
            return;
        }

        ListData = res.Data.Select(x => new ListData()
        {
            Id = x.Id,
            Main = x.Name,
            Detail = x.CountryName ?? string.Empty,
        }).ToArray();
    }
    #endregion
}
