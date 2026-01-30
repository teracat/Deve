using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Models;
using Deve.Customers.Clients;

namespace Deve.Clients.Maui.ViewModels;

internal sealed class ClientsViewModel : ListDataViewModel
{
    #region Constructor
    public ClientsViewModel(INavigationService navigationService, Data.IData data)
        : base(navigationService, data)
    {
    }
    #endregion

    #region Overrides
    protected override async Task GetListData()
    {
        ClientGetListRequest? request = null;
        var res = await Data.Customers.Clients.GetAsync(request);
        if (!res.Success)
        {
            ErrorText = Utils.ErrorsToString(res.Errors);
            return;
        }

        ListData = res.Data.Select(x => new ListData()
        {
            Id = x.Id,
            Main = x.TradeName ?? x.Name,
            Detail = x.CityName + ", " + x.StateName + " (" + x.CountryName + ")",
        }).ToArray();
    }
    #endregion
}
