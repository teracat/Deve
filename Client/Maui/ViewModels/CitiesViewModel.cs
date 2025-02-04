using Deve.ClientApp.Maui.Interfaces;
using Deve.ClientApp.Maui.Models;

namespace Deve.ClientApp.Maui.ViewModels
{
    public class CitiesViewModel : ListDataViewModel
    {
        #region Constructor
        public CitiesViewModel(IServiceProvider serviceProvider, IDataService dataService) 
            : base(serviceProvider, dataService)
        {
        }
        #endregion

        #region Overrides
        protected override async Task GetListData()
        {
            CriteriaCity? criteria = null;
            var res = await DataService.Data.Cities.Get(criteria);
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