using Deve.Model;
using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Models;

namespace Deve.Clients.Maui.ViewModels
{
    public partial class CountriesViewModel : ListDataViewModel
    {
        #region Constructor
        public CountriesViewModel(INavigationService navigationService, Internal.Data.IData data, ISchedulerProvider scheduler)
            : base(navigationService, data, scheduler)
        {
        }
        #endregion

        #region Overrides
        protected override async Task<Result> GetListData()
        {
            var res = await Data.Countries.Get(null);
            if (res.Success)
            {
                ListData = res.Data.Select(x => new ListData()
                {
                    Id = x.Id,
                    Main = x.Name,
                    Detail = x.IsoCode,
                }).ToArray();
            }

            return res;
        }
        #endregion
    }
}