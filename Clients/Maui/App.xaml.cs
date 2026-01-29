using Deve.Clients.Maui.Interfaces;
using Deve.Diagnostics;

namespace Deve.Clients.Maui;

internal sealed partial class App : Application
{
    private readonly INavigationService _navigationService;
    private readonly IDiagnosticsTransactionHandler _diagnosticsTransactionHandler;

    public App(INavigationService navigationService, IDiagnosticsTransactionHandler diagnosticsTransactionHandler)
    {
        InitializeComponent();

        _navigationService = navigationService;
        _diagnosticsTransactionHandler = diagnosticsTransactionHandler;
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = new Window(new AppShell(_navigationService));

        window.Created += OnCreated;
        window.Destroying += OnDestroying;

        return window;
    }

    private void OnCreated(object? sender, EventArgs e) => _diagnosticsTransactionHandler.StartTransaction("MAUI App User Session", "app.maui");

    private void OnDestroying(object? sender, EventArgs e) => _diagnosticsTransactionHandler.StopTransaction();
}
