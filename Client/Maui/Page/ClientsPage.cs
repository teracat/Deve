using Deve.ClientApp.Maui.Resources.Strings;

namespace Deve.ClientApp.Maui
{
    public class ClientsPage : ListDataPage
    {
        public ClientsPage()
        {
            Title = AppResources.Clients;
            ViewModel = new ClientsViewModel(this);
        }
    }
}
