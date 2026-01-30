using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Models;
using Deve.Customers.Cities;

namespace Deve.Clients.Maui.ViewModels;

internal sealed partial class CitiesViewModel : ListDataViewModel
{
    #region Constructor
    public CitiesViewModel(INavigationService navigationService, Data.IData data)
        : base(navigationService, data)
    {
    }
    #endregion

    #region Overrides
    protected override async Task GetListData()
    {
        CityGetListRequest? request = null;
        var res = await Data.Customers.Cities.GetAsync(request);
        if (!res.Success)
        {
            ErrorText = Utils.ErrorsToString(res.Errors);
            return;
        }

        ListData = res.Data.Select(x => new ListData()
        {
            Id = x.Id,
            Main = x.Name,
            Detail = x.StateName + " (" + x.CountryName + ")",
        }).ToArray();
    }
    #endregion
}
