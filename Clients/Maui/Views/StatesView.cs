using Deve.Clients.Maui.Resources.Strings;
using Deve.Clients.Maui.ViewModels;

namespace Deve.Clients.Maui.Views
{
    public class StatesView : ListDataView
    {
        public StatesView(StatesViewModel viewModel)
            : base(viewModel)
        {
            Title = AppResources.States;
        }
    }
}
