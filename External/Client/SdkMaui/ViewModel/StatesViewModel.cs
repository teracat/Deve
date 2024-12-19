namespace Deve.External.ClientApp.Maui
{
    public class StatesViewModel : ListDataViewModel
    {
        #region Constructor
        public StatesViewModel(StatesPage page) 
            : base(page)
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
            }).ToList();
        }
        #endregion
    }
}