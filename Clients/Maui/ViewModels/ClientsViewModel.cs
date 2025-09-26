using Deve.Model;
using Deve.Clients.Maui.Models;
using Deve.Clients.Maui.Interfaces;

namespace Deve.Clients.Maui.ViewModels
{
    public partial class ClientsViewModel : ListDataViewModel
    {
        #region Constructor
        public ClientsViewModel(INavigationService navigationService, Internal.Data.IData data, ISchedulerProvider scheduler)
            : base(navigationService, data, scheduler)
        {
        }
        #endregion

        #region Overrides
        protected override async Task<Result> GetListData()
        {
            var res = await Data.Clients.Get(null);
            if (res.Success)
            {
                ListData = res.Data.Select(x => new ListData()
                {
                    Id = x.Id,
                    Main = x.DisplayName,
                    Detail = x.Location.City + ", " + x.Location.State + " (" + x.Location.Country + ")",
                }).ToArray();
            }

            return res;
        }
        #endregion
    }
}