using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.ViewModels;
using Deve.Tests.Maui.Fixtures;

namespace Deve.Tests.Maui
{
    public class TestClientsViewModel : TestListDataViewModel
    {
        public TestClientsViewModel(FixtureMaui fixture)
            : base(fixture)
        {
        }

        protected override ListDataViewModel CreateViewModel(INavigationService navigationService, IDataService dataService)
        {
            return new ClientsViewModel(navigationService, dataService);
        }
    }
}