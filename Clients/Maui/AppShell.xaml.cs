using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Views;

namespace Deve.Clients.Maui;

internal sealed partial class AppShell : Shell
{
    private readonly INavigationService _navigationService;

    public AppShell(INavigationService navigationService)
    {
        _navigationService = navigationService;

        InitializeComponent();

        Routing.RegisterRoute("//clients/details", typeof(ClientDetailsView));
    }

    private async void LogoutClicked(object sender, EventArgs e) => await _navigationService.NavigateToAsync("//login");
}