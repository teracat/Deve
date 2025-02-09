using Deve.Clients.Maui.Interfaces;
using Deve.Clients.Maui.Views;

namespace Deve.Clients.Maui
{
    public partial class AppShell : Shell
    {
        private INavigationService _navigationService;

        public AppShell(INavigationService navigationService)
        {
            _navigationService = navigationService;

            InitializeComponent();

            Routing.RegisterRoute("//clients/details", typeof(ClientDetailsView));
        }

        private async void LogoutClicked(object sender, EventArgs e) => await _navigationService.NavigateToAsync("//login");

        protected override void OnParentSet()
        {
            base.OnParentSet();
            _ = _navigationService.InitializeAsync();
        }
    }
}