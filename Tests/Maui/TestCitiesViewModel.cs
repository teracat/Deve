using Deve.ClientApp.Maui.Interfaces;
using Deve.ClientApp.Maui.ViewModels;
using Deve.Tests.Maui.Fixtures;

namespace Deve.Tests.Maui
{
    public class TestCitiesViewModel : TestListDataViewModel
    {
        public TestCitiesViewModel(FixtureMaui fixture)
            : base(fixture)
        {
        }

        protected override ListDataViewModel CreateViewModel(INavigationService navigationService, IDataService dataService)
        {
            return new CitiesViewModel(navigationService, dataService);
        }
    }
}