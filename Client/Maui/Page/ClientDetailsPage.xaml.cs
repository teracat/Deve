namespace Deve.ClientApp.Maui
{
    public partial class ClientDetailsPage : BasePage
    {
        public ClientDetailsPage()
        {
            InitializeComponent();

            ViewModel = new ClientDetailsViewModel(this);
        }
    }
}
