using Deve.Clients.Maui.Resources.Strings;
using Deve.Clients.Maui.ViewModels;

namespace Deve.Clients.Maui.Views
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
