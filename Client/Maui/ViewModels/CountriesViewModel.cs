using Deve.ClientApp.Maui.Models;

namespace Deve.ClientApp.Maui.ViewModels
{
    public partial class CountriesViewModel : ListDataViewModel
    {
        #region Overrides
        protected override async Task LoadListData()
        {
            CriteriaCountry? criteria = null;
            var res = await Globals.Data.Countries.Get(criteria);
            if (!res.Success)
            {
                ErrorText = Utils.ErrorsToString(res.Errors);
                return;
            }

            ListData = res.Data.Select(x => new ListData()
            {
                Id = x.Id,
                Main = x.Name,
                Detail = x.IsoCode,
            }).ToArray();
        }
        #endregion
    }
}