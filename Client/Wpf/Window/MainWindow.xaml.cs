using Deve.ClientApp.Wpf.ViewModel;

namespace Deve.ClientApp.Wpf.Window
{
    public partial class MainWindow : BaseWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new MainViewModel(this);
        }
    }
}