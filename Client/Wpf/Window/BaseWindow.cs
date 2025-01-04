using System.Windows;

namespace Deve.ClientApp.Wpf
{
    public class BaseWindow : Window
    {
        #region Fields
        private BaseViewModel? _viewModel;
        #endregion

        #region Properties
        protected BaseViewModel? ViewModel
        {
            get => _viewModel;
            set
            {
                if (_viewModel != value)
                {
                    DataContext = _viewModel = value;
                }
            }
        }
        #endregion

        #region Constructor
        public BaseWindow()
        {
            Loaded += OnWindowLoaded;
            Unloaded += OnWindowUnloaded;
        }
        #endregion

        #region Events
        private void OnWindowLoaded(object sender, RoutedEventArgs e) => _viewModel?.OnWindowLoaded();
        private void OnWindowUnloaded(object sender, RoutedEventArgs e) => _viewModel?.OnWindowUnloaded();
        #endregion
    }
}
