using Deve.ClientApp.Maui.Interfaces;
using Deve.ClientApp.Maui.Views;

namespace Deve.ClientApp.Maui
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