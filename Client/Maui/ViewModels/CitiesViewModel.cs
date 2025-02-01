using Deve.ClientApp.Maui.Models;

namespace Deve.ClientApp.Maui.ViewModels
{
    public class CitiesViewModel : ListDataViewModel
    {
        #region Constructor
        public CitiesViewModel() 
        {
        }
        #endregion

        #region Overrides
        protected override async Task LoadListData()
        {
            CriteriaCity? criteria = null;
            var res = await Globals.Data.Cities.Get(criteria);
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