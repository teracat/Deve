using Deve.Internal;
using Deve.ClientApp.Maui.Models;

namespace Deve.ClientApp.Maui.ViewModels
{
    public class ClientsViewModel : ListDataViewModel
    {
        #region Constructor
        public ClientsViewModel() 
        {
        }
        #endregion

        #region Overrides
        protected override async Task LoadListData()
        {
            CriteriaClient? criteria = null;
            var res = await Globals.Data.Clients.Get(criteria);
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