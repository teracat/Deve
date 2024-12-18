using Deve.External.ClientApp.Maui.Resources.Strings;

namespace Deve.External.ClientApp.Maui
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
