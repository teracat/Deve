using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Models;
using Deve.Customers.Countries;
using Deve.Dto.Responses.Results;

namespace Deve.Clients.Maui.ViewModels;

internal sealed partial class CountriesViewModel : ListDataViewModel
{
    #region Constructor
    public CountriesViewModel(INavigationService navigationService, Data.IData data, ISchedulerProvider scheduler)
        : base(navigationService, data, scheduler)
    {
    }
    #endregion

    #region Overrides
    protected override async Task<IResult> GetListData()
    {
        CountryGetListRequest? request = null;
        var res = await Data.Customers.Countries.GetAsync(request);
        if (res.Success)
        {
            ListData = res.Data.Select(x => new ListData()
            {
                Id = x.Id,
                Main = x.Name,
                Detail = x.IsoCode,
            }).ToArray();
        }

        return res;
    }
    #endregion
}