using Deve.Internal;
using Deve.ClientApp.Maui.Models;
using Deve.ClientApp.Maui.Interfaces;

namespace Deve.ClientApp.Maui.ViewModels
{
    public partial class ClientsViewModel : ListDataViewModel
    {
        #region Constructor
        public ClientsViewModel(INavigationService navigationService, IDataService dataService)
            : base(navigationService, dataService)
        {
        }
        #endregion

        #region Overrides
        protected override async Task GetListData()
        {
            CriteriaClient? criteria = null;
            var res = await DataService.Data.Clients.Get(criteria);
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