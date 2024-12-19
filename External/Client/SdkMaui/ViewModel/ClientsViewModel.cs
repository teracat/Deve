namespace Deve.External.ClientApp.Maui
{
    public class ClientsViewModel : ListDataViewModel
    {
        #region Constructor
        public ClientsViewModel(ClientsPage page) 
            : base(page)
        {
        }
        #endregion

        #region Overrides
        protected override async Task LoadListData()
        {
            CriteriaClientBasic? criteria = null;
            var res = await Globals.Data.Clients.Get(criteria);
            if (!res.Success)
            {
                ErrorText = Utils.ErrorsToString(res.Errors);
                return;
            }

            ListData = res.Data.Select(x => new ListData()
            {
                Id = x.Id,
                Main = x.DisplayName,
                Detail = x.City + ", " + x.State + " (" + x.Country + ")",
            }).ToList();
        }
        #endregion
    }
}