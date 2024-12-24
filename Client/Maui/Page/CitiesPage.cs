using Deve.ClientApp.Maui.Resources.Strings;

namespace Deve.ClientApp.Maui
{
    public class CitiesPage : ListDataPage
    {
        public CitiesPage()
        {
            Title = AppResources.Cities;
            ViewModel = new CitiesViewModel(this);
        }
    }
}
