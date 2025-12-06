namespace Deve.Clients.Maui.Interfaces
{
    /// <summary>
    /// Interface for the navigation service.
    /// Based on https://learn.microsoft.com/en-us/dotnet/architecture/maui/navigation
    /// </summary>
    public interface INavigationService
    {
        Task NavigateToAsync(string route, INavigationParameters routeParameters);

        Task NavigateToAsync(string route);

        Task PopAsync();
    }
}
