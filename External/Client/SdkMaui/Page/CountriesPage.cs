using Deve.External.ClientApp.Maui.Resources.Strings;

namespace Deve.External.ClientApp.Maui
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
