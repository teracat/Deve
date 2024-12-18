using Deve.External.ClientApp.Maui.Resources.Strings;

namespace Deve.External.ClientApp.Maui
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
