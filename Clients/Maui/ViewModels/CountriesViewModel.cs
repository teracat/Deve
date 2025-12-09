using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Models;
using Deve.Criteria;

namespace Deve.Clients.Maui.ViewModels
{
    public class CountriesViewModel : ListDataViewModel
    {
        #region Constructor
        public CountriesViewModel(INavigationService navigationService, Internal.Data.IData data)
            : base(navigationService, data)
        {
        }
        #endregion

        #region Overrides
        protected override async Task GetListData()
        {
            CriteriaCountry? criteria = null;
            var res = await Data.Countries.Get(criteria);
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