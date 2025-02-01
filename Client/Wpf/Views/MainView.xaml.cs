using Deve.ClientApp.Wpf.ViewModels;

namespace Deve.ClientApp.Wpf.Views
{
    public partial class MainView : BaseView
    {
        public MainView()
        {
            InitializeComponent();

            ViewModel = new MainViewModel();
        }
    }
}