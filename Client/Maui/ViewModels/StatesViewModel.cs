using Deve.ClientApp.Maui.Interfaces;
using Deve.ClientApp.Maui.Models;

namespace Deve.ClientApp.Maui.ViewModels
{
    public class StatesViewModel : ListDataViewModel
    {
        #region Constructor
        public StatesViewModel(INavigationService navigationService, IServiceProvider serviceProvider, IDataService dataService)
            : base(navigationService, serviceProvider, dataService)
        {
        }
        #endregion

        #region Overrides
        protected override async Task GetListData()
        {
            CriteriaState? criteria = null;
            var res = await DataService.Data.States.Get(criteria);
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