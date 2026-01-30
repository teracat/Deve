using Deve.Clients.Maui.Resources.Strings;
using Deve.Clients.Maui.ViewModels;

namespace Deve.Clients.Maui.Views;

internal sealed class CountriesView : ListDataView
{
    public CountriesView(CountriesViewModel viewModel)
        : base(viewModel)
    {
        Title = AppResources.Countries;
    }
}
