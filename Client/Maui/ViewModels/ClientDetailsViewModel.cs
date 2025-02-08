using CommunityToolkit.Mvvm.ComponentModel;
using Deve.Internal;
using Deve.ClientApp.Maui.Interfaces;

namespace Deve.ClientApp.Maui.ViewModels
{
    public partial class ClientDetailsViewModel : BaseDetailsViewModel
    {
        #region Atributes
        [ObservableProperty]
        private Client? _client;
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