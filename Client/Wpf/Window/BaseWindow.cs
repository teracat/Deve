using System.ComponentModel;
using System.Windows;
using Deve.ClientApp.Wpf.ViewModel;

namespace Deve.ClientApp.Wpf.Window
{
    public class BaseWindow : System.Windows.Window
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

        #region Overrides
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (_viewModel is not null && _viewModel.IsBusy)
                e.Cancel = true;
        }
        #endregion

        #region Methods
        protected virtual void OnWindowLoaded()
        {
            _viewModel?.OnWindowLoaded();
        }

        protected virtual void OnWindowUnloaded()
        {
            _viewModel?.OnWindowUnloaded();
        }
        #endregion

        #region Events
        private void OnWindowLoaded(object sender, RoutedEventArgs e) => OnWindowLoaded();
        private void OnWindowUnloaded(object sender, RoutedEventArgs e) => OnWindowUnloaded();
        #endregion
    }
}
