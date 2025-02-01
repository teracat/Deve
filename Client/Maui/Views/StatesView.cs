using Deve.ClientApp.Maui.Resources.Strings;
using Deve.ClientApp.Maui.ViewModels;

namespace Deve.ClientApp.Maui.Views
{
    public class StatesView : ListDataView
    {
        public StatesView()
        {
            Title = AppResources.States;
            ViewModel = new StatesViewModel();
        }
    }
}
