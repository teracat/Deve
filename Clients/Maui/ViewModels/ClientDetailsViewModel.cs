using CommunityToolkit.Mvvm.ComponentModel;
using Deve.Clients.Maui.Interfaces;
using Deve.Internal.Model;

namespace Deve.Clients.Maui.ViewModels
{
    public partial class ClientDetailsViewModel : BaseDetailsViewModel
    {
        #region Atributes
        [ObservableProperty]
        private Client? _client;
        #endregion

        #region Constructor
        public ClientDetailsViewModel(INavigationService navigationService, Internal.Data.IData data)
            : base(navigationService, data)
        {
        }
        #endregion

        #region Methods
        protected override async Task GetData()
        {
            var res = await Data.Clients.Get(Id);
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