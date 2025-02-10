using Deve.Internal.Model;
using Deve.Clients.Maui.Interfaces;

namespace Deve.Clients.Maui.ViewModels
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
        public ClientDetailsViewModel(INavigationService navigationService, IDataService dataService)
            : base(navigationService, dataService)
        {
        }
        #endregion

        #region Methods
        protected override async Task GetData()
        {
            var res = await DataService.Data.Clients.Get(Id);
            if (!res.Success)
            {
                ErrorText = Utils.ErrorsToString(res.Errors);
                return;
            }

            Client = res.Data;
        }
        #endregion
    }
}