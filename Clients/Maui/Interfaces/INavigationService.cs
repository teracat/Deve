namespace Deve.Clients.Maui.Interfaces
{
    /// <summary>
    /// Interface for the navigation service.
    /// Based on https://learn.microsoft.com/en-us/dotnet/architecture/maui/navigation
    /// </summary>
    public interface INavigationService
    {
        Task InitializeAsync();

        Task NavigateToAsync(string route, INavigationParameters? routeParameters = null);

        Task PopAsync();
    }
}
