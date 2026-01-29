using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.ViewModels;
using Deve.Tests.Maui.Fixtures;

namespace Deve.Tests.Maui;

public class TestCountriesViewModel : TestListDataViewModel
{
    public TestCountriesViewModel(FixtureMaui fixture)
        : base(fixture)
    {
    }

    internal override ListDataViewModel CreateViewModel(INavigationService navigationService, Data.IData data, ISchedulerProvider scheduler) => new CountriesViewModel(navigationService, data, scheduler);
}