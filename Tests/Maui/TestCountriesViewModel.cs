using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.ViewModels;
using Deve.Tests.Maui.Fixtures;

namespace Deve.Tests.Maui
{
    public class TestCountriesViewModel : TestListDataViewModel
    {
        public TestCountriesViewModel(FixtureMaui fixture)
            : base(fixture)
        {
        }

        protected override ListDataViewModel CreateViewModel(INavigationService navigationService, Internal.Data.IData data, ISchedulerProvider scheduler)
        {
            return new CountriesViewModel(navigationService, data, scheduler);
        }
    }
}