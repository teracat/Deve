using Deve.Clients.Maui.ViewModels;

namespace Deve.Clients.Maui.Views
{
    public partial class ClientDetailsView : BaseView
    {
        public ClientDetailsView(ClientDetailsViewModel viewModel)
            : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
