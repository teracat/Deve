using Deve.Internal.Criteria;
using Deve.Clients.Maui.Models;
using Deve.Clients.Maui.Interfaces;

namespace Deve.Clients.Maui.ViewModels
{
    public class ClientsViewModel : ListDataViewModel
    {
        #region Constructor
        public ClientsViewModel(INavigationService navigationService, Internal.Data.IData data)
            : base(navigationService, data)
        {
        }
        #endregion

        #region Overrides
        protected override async Task GetListData()
        {
            CriteriaClient? criteria = null;
            var res = await Data.Clients.Get(criteria);
            if (!res.Success)
            {
                ErrorText = Utils.ErrorsToString(res.Errors);
                return;
            }

            ListData = res.Data.Select(x => new ListData()
            {
                Id = x.Id,
                Main = x.DisplayName,
                Detail = x.Location.City + ", " + x.Location.State + " (" + x.Location.Country + ")",
            }).ToArray();
        }
        #endregion
    }
}