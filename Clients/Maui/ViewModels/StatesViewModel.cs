using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Models;
using Deve.Customers.States;
using Deve.Dto.Responses.Results;

namespace Deve.Clients.Maui.ViewModels;

internal sealed partial class StatesViewModel : ListDataViewModel
{
    #region Constructor
    public StatesViewModel(INavigationService navigationService, Data.IData data, ISchedulerProvider scheduler)
        : base(navigationService, data, scheduler)
    {
    }
    #endregion

    #region Overrides
    protected override async Task<IResult> GetListData()
    {
        StateGetListRequest? request = null;
        var res = await Data.Customers.States.GetAsync(request);
        if (res.Success)
        {
            ListData = res.Data.Select(x => new ListData()
            {
                Id = x.Id,
                Main = x.Name,
                Detail = x.CountryName ?? string.Empty,
            }).ToArray();
        }

        return res;
    }
    #endregion
}
