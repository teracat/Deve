using Deve.ClientApp.Maui.Resources.Strings;
using Deve.ClientApp.Maui.ViewModels;

namespace Deve.ClientApp.Maui.Views
{
    public class CitiesView : ListDataView
    {
        public CitiesView()
        {
            Title = AppResources.Cities;
            ViewModel = new CitiesViewModel();
        }
    }
}
