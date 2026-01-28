using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Models;
using Deve.Customers.Countries;

namespace Deve.Clients.Maui.ViewModels;

internal sealed partial class CountriesViewModel : ListDataViewModel
{
    #region Constructor
    public CountriesViewModel(INavigationService navigationService, Data.IData data)
        : base(navigationService, data)
    {
    }
    #endregion

    #region Overrides
    protected override async Task GetListData()
    {
        CountryGetListRequest? request = null;
        var res = await Data.Customers.Countries.GetAsync(request);
        if (!res.Success)
        {
            ErrorText = Utils.ErrorsToString(res.Errors);
            return;
        }

        ListData = res.Data.Select(x => new ListData()
        {
            Id = x.Id,
            Main = x.Name,
            Detail = x.IsoCode,
        }).ToArray();
    }
    #endregion
}