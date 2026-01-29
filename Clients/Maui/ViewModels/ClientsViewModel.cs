using Deve.Clients.Maui.Models;
using Deve.Customers.Clients;
using Deve.Clients.Maui.Interfaces;
using Deve.Dto.Responses.Results;

namespace Deve.Clients.Maui.ViewModels;

internal sealed partial class ClientsViewModel : ListDataViewModel
{
    #region Constructor
    public ClientsViewModel(INavigationService navigationService, Data.IData data, ISchedulerProvider scheduler)
        : base(navigationService, data, scheduler)
    {
    }
    #endregion

    #region Overrides
    protected override async Task<IResult> GetListData()
    {
        ClientGetListRequest? request = null;
        var res = await Data.Customers.Clients.GetAsync(request);
        if (res.Success)
        {
            ListData = res.Data.Select(x => new ListData()
            {
                Id = x.Id,
                Main = x.TradeName ?? x.Name,
                Detail = x.CityName + ", " + x.StateName + " (" + x.CountryName + ")",
            }).ToArray();
        }

        return res;
    }
    #endregion
}