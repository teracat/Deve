using Deve.ClientApp.Maui.Resources.Strings;

namespace Deve.ClientApp.Maui
{
    public class CountriesPage : ListDataPage
    {
        public CountriesPage()
        {
            Title = AppResources.Countries;
            ViewModel = new CountriesViewModel(this);
        }
    }
}
