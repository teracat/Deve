using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.ViewModels;
using Deve.Tests.Maui.Fixtures;

namespace Deve.Tests.Maui
{
    public class TestCitiesViewModel : TestListDataViewModel
    {
        public TestCitiesViewModel(FixtureMaui fixture)
            : base(fixture)
        {
        }

        protected override ListDataViewModel CreateViewModel(INavigationService navigationService, Internal.Data.IData data)
        {
            return new CitiesViewModel(navigationService, data);
        }
    }
}