using Deve.Internal;
using Deve.ClientApp.Maui.Interfaces;

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
        public ClientDetailsViewModel(IServiceProvider serviceProvider, IDataService dataService)
            : base(serviceProvider, dataService)
        {
        }
        #endregion

        #region Methods
        protected override async Task GetData()
        {
            var res = await DataService.Data.Clients.Get(_id);
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