using ReactiveUI.SourceGenerators;
using Deve.Internal.Model;
using Deve.Clients.Maui.Interfaces;

namespace Deve.Clients.Maui.ViewModels
{
    public partial class ClientDetailsViewModel : BaseDetailsViewModel
    {
        #region Atributes
        [Reactive]
        private Client? _client;
        #endregion

        #region Constructor
        public ClientDetailsViewModel(INavigationService navigationService, Internal.Data.IData data, ISchedulerProvider scheduler)
            : base(navigationService, data, scheduler)
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