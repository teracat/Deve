using Deve.Clients.Maui.Interfaces;
using Deve.Internal.Dto;

namespace Deve.Clients.Maui.ViewModels
{
    public class ClientDetailsViewModel : BaseDetailsViewModel
    {
        #region Properties
        public Client? Client
        {
            get;
            set => SetProperty(ref field, value);
        }
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