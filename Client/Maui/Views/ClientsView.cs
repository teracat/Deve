using Deve.ClientApp.Maui.Resources.Strings;
using Deve.ClientApp.Maui.ViewModels;

namespace Deve.ClientApp.Maui.Views
{
    public class ClientsView : ListDataView
    {
        public ClientsView(ClientsViewModel viewModel)
            : base(viewModel)
        {
            Title = AppResources.Clients;
        }
    }
}
