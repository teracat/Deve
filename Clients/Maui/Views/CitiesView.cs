using Deve.Clients.Maui.Resources.Strings;
using Deve.Clients.Maui.ViewModels;

namespace Deve.Clients.Maui.Views;

internal sealed class CitiesView : ListDataView
{
    public CitiesView(CitiesViewModel viewModel)
        : base(viewModel)
    {
        Title = AppResources.Cities;
    }
}
