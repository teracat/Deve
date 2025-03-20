using Deve.Model;
using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Models;

namespace Deve.Clients.Maui.ViewModels
{
    public class StatesViewModel : ListDataViewModel
    {
        #region Constructor
        public StatesViewModel(INavigationService navigationService, Internal.Data.IData data, ISchedulerProvider scheduler)
            : base(navigationService, data, scheduler)
        {
        }
        #endregion

        #region Overrides
        protected override async Task<Result> GetListData()
        {
            var res = await Data.States.Get(null);
            if (res.Success)
            {
                ListData = res.Data.Select(x => new ListData()
                {
                    Id = x.Id,
                    Main = x.Name,
                    Detail = x.Country,
                }).ToArray();
            }

            return res;
        }
        #endregion
    }
}