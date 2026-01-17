using Deve.Clients.Maui.Resources.Strings;
using Deve.Clients.Maui.ViewModels;

namespace Deve.Clients.Maui.Views;

internal sealed class StatesView : ListDataView
{
    public StatesView(StatesViewModel viewModel)
        : base(viewModel)
    {
        Title = AppResources.States;
    }
}
