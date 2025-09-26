using Deve.Clients.Maui.Interfaces;

namespace Deve.Clients.Maui
{
    public partial class App : Application
    {
        // Sentry transaction for user session tracking
        //private ITransactionTracer? _sentryTransaction;

        public App(INavigationService navigationService)
        {
            InitializeComponent();

            MainPage = new AppShell(navigationService);
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Window window = base.CreateWindow(activationState);

            window.Created += OnCreated;
            window.Destroying += OnDestroying;

            return window;
        }

        private void OnCreated(object? sender, EventArgs e)
        {
            // Start a new Sentry transaction for the user session
            /*if (_sentryTransaction is null)
            {
                _sentryTransaction = SentrySdk.StartTransaction("User Session", "app.maui");
                SentrySdk.ConfigureScope(scope => scope.Transaction = _sentryTransaction);
            }*/
        }

        private void OnDestroying(object? sender, EventArgs e)
        {
            // Finish the Sentry transaction when the app is closing
            /*_sentryTransaction?.Finish();
            _sentryTransaction = null;*/
        }
    }
}