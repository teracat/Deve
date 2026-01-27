using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.ViewModels;
using Deve.Tests.Maui.Fixtures;

namespace Deve.Tests.Maui;

public class TestCitiesViewModel : TestListDataViewModel
{
    public TestCitiesViewModel(FixtureMaui fixture)
        : base(fixture)
    {
    }

    internal override ListDataViewModel CreateViewModel(INavigationService navigationService, Data.IData data) => new CitiesViewModel(navigationService, data);
}
