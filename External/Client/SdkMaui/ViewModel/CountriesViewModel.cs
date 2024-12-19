namespace Deve.External.ClientApp.Maui
{
    public class CountriesViewModel : ListDataViewModel
    {
        #region Constructor
        public CountriesViewModel(CountriesPage page) 
            : base(page)
        {
        }
        #endregion

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
            }).ToList();
        }
        #endregion
    }
}