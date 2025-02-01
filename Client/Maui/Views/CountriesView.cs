using Deve.ClientApp.Maui.Resources.Strings;
using Deve.ClientApp.Maui.ViewModels;

namespace Deve.ClientApp.Maui.Views
{
    public class CountriesView : ListDataView
    {
        public CountriesView()
        {
            Title = AppResources.Countries;
            ViewModel = new CountriesViewModel();
        }
    }
}
