using Deve.External.ClientApp.Maui.Resources.Strings;

namespace Deve.External.ClientApp.Maui
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
