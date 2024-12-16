namespace Deve.External.ClientApp.Maui
{
    public class ClientsViewModel : BaseViewModel
    {
        #region Fields
        IList<ClientBasic>? _clients;
        #endregion

        #region Properties
        public IList<ClientBasic>? Clients
        {
            get => _clients;
            set => SetProperty(ref _clients, value);
        }
        #endregion

        #region Constructor
        public ClientsViewModel(ClientsPage page) 
            : base(page)
        {
        }
        #endregion

        #region Overrides
        public override void OnViewAppearing()
        {
            base.OnViewAppearing();
            _ = LoadData();
        }
        #endregion

        #region Methods
        internal async Task LoadData()
        {
            ErrorText = string.Empty;
            IsBusy = true;
            try
            {
                CriteriaClientBasic? criteria = null;
                var resClients = await Globals.Data.Clients.Get(criteria);
                if (!resClients.Success)
                {
                    ErrorText = Utils.ErrorsToString(resClients.Errors);
                    return;
                }

                Clients = resClients.Data;
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}