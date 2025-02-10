using Deve.Clients.Maui.Resources.Strings;
using Deve.Clients.Maui.ViewModels;

namespace Deve.Clients.Maui.Views
{
    public class CitiesView : ListDataView
    {
        public CitiesView(CitiesViewModel viewModel)
            : base(viewModel)
        {
            Title = AppResources.Cities;
        }
    }
}
