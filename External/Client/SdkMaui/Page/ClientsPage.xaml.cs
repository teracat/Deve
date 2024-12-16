namespace Deve.External.ClientApp.Maui
{
    public partial class ClientsPage : BasePage
    {
        public ClientsPage()
        {
            InitializeComponent();

            ViewModel = new ClientsViewModel(this);
        }
    }
}
