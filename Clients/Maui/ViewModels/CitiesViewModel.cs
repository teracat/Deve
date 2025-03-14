using Deve.Criteria;
using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Models;

namespace Deve.Clients.Maui.ViewModels
{
    public partial class CitiesViewModel : ListDataViewModel
    {
        #region Constructor
        public CitiesViewModel(INavigationService navigationService, Internal.Data.IData data, ISchedulerProvider scheduler) 
            : base(navigationService, data, scheduler)
        {
        }
        #endregion

        #region Overrides
        protected override async Task GetListData()
        {
            CriteriaCity? criteria = null;
            var res = await Data.Cities.Get(criteria);
            if (!res.Success)
            {
                ErrorText = Utils.ErrorsToString(res.Errors);
                return;
            }

            ListData = res.Data.Select(x => new ListData()
            {
                Id = x.Id,
                Main = x.Name,
                Detail = x.State + " (" + x.Country + ")",
            }).ToArray();
        }
        #endregion
    }
}