using Deve.ClientApp.Maui.Resources.Strings;
using Deve.ClientApp.Maui.ViewModels;

namespace Deve.ClientApp.Maui.Views
{
    public class CountriesView : ListDataView
    {
        public CountriesView(CountriesViewModel viewModel)
            : base(viewModel)
        {
            Title = AppResources.Countries;
        }
    }
}
