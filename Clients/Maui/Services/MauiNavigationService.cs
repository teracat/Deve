using Deve.Clients.Maui.Interfaces;

namespace Deve.Clients.Maui.Services
{
    /// <summary>
    /// Navigation service for the app.
    /// Based on https://learn.microsoft.com/en-us/dotnet/architecture/maui/navigation
    /// </summary>
    internal class MauiNavigationService : INavigationService
    {
        public Task NavigateToAsync(string route, INavigationParameters routeParameters) => Shell.Current.GoToAsync(route, routeParameters);

        public Task NavigateToAsync(string route) => Shell.Current.GoToAsync(route);

        public Task PopAsync() => Shell.Current.Navigation.PopAsync();
    }
}
