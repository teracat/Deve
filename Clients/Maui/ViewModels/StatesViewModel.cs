using Deve.Criteria;
using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Models;

namespace Deve.Clients.Maui.ViewModels
{
    public class StatesViewModel : ListDataViewModel
    {
        #region Constructor
        public StatesViewModel(INavigationService navigationService, Internal.Data.IData data)
            : base(navigationService, data)
        {
        }
        #endregion

        #region Overrides
        protected override async Task GetListData()
        {
            CriteriaState? criteria = null;
            var res = await Data.States.Get(criteria);
            if (!res.Success)
            {
                ErrorText = Utils.ErrorsToString(res.Errors);
                return;
            }

            ListData = res.Data.Select(x => new ListData()
            {
                Id = x.Id,
                Main = x.Name,
                Detail = x.Country,
            }).ToArray();
        }
        #endregion
    }
}