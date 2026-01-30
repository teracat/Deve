using Deve.Clients.Maui.ViewModels;

namespace Deve.Clients.Maui.Views;

internal sealed partial class ClientDetailsView : BaseView
{
    public ClientDetailsView(ClientDetailsViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}
