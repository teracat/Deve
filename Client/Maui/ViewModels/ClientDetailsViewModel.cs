using Deve.Internal;

namespace Deve.ClientApp.Maui.ViewModels
{
    public class ClientDetailsViewModel : BaseDetailsViewModel
    {
        #region Atributes
        private Client? _client;
        #endregion

        #region Properties
        public Client? Client
        {
            get => _client;
            set => SetProperty(ref _client, value);
        }
        #endregion

        #region Constructor
        public ClientDetailsViewModel() 
        {
        }
        #endregion

        #region Methods
        protected override async Task LoadData()
        {
            ErrorText = string.Empty;
            IsBusy = true;
            try
            {
                var res = await Globals.Data.Clients.Get(_id);
                if (!res.Success)
                {
                    ErrorText = Utils.ErrorsToString(res.Errors);
                    return;
                }

                Client = res.Data;
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}