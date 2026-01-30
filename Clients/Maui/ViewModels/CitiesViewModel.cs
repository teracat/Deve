using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Models;
using Deve.Customers.Cities;
using Deve.Dto.Responses.Results;

namespace Deve.Clients.Maui.ViewModels;

internal sealed partial class CitiesViewModel : ListDataViewModel
{
    #region Constructor
    public CitiesViewModel(INavigationService navigationService, Data.IData data, ISchedulerProvider scheduler)
        : base(navigationService, data, scheduler)
    {
    }
    #endregion

    #region Overrides
    protected override async Task<IResult> GetListData()
    {
        CityGetListRequest? request = null;
        var res = await Data.Customers.Cities.GetAsync(request);
        if (res.Success)
        {
            ListData = res.Data.Select(x => new ListData()
            {
                Id = x.Id,
                Main = x.Name,
                Detail = x.StateName + " (" + x.CountryName + ")",
            }).ToArray();
        }

        return res;
    }
    #endregion
}