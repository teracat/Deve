using Deve.Clients.Maui.Resources.Strings;
using Deve.Clients.Maui.ViewModels;

namespace Deve.Clients.Maui.Views;

internal sealed class ClientsView : ListDataView
{
    public ClientsView(ClientsViewModel viewModel)
        : base(viewModel)
    {
        Title = AppResources.Clients;
    }
}
