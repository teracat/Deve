using Deve.ClientApp.Maui.Resources.Strings;

namespace Deve.ClientApp.Maui
{
    public class StatesPage : ListDataPage
    {
        public StatesPage()
        {
            Title = AppResources.States;
            ViewModel = new StatesViewModel(this);
        }
    }
}
