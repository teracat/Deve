using Deve.ClientApp.Maui.Models;

namespace Deve.ClientApp.Maui.ViewModels
{
    public class StatesViewModel : ListDataViewModel
    {
        #region Constructor
        public StatesViewModel()
        {
        }
        #endregion

        #region Overrides
        protected override async Task LoadListData()
        {
            CriteriaState? criteria = null;
            var res = await Globals.Data.States.Get(criteria);
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