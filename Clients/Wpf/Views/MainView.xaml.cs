using Deve.Clients.Wpf.ViewModels;

namespace Deve.Clients.Wpf.Views
{
    public partial class MainView : BaseView
    {
        public MainView(MainViewModel mainViewModel)
        {
            InitializeComponent();

            ViewModel = mainViewModel;
        }
    }
}