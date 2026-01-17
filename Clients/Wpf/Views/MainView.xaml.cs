using Deve.Clients.Wpf.ViewModels;

namespace Deve.Clients.Wpf.Views;

internal sealed partial class MainView : BaseView
{
    public MainView(MainViewModel mainViewModel)
    {
        InitializeComponent();

        ViewModel = mainViewModel;
    }
}