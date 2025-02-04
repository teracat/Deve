using Deve.ClientApp.Maui.ViewModels;

namespace Deve.ClientApp.Maui.Views
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
