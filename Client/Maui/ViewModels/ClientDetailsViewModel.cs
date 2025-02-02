using CommunityToolkit.Mvvm.ComponentModel;
using Deve.Internal;

namespace Deve.ClientApp.Maui.ViewModels
{
    public partial class ClientDetailsViewModel : BaseDetailsViewModel
    {
        #region Atributes
        [ObservableProperty]
        private Client? _client;
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