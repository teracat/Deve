using System.ComponentModel;
using System.Windows;
using Deve.ClientApp.Wpf.ViewModels;

namespace Deve.ClientApp.Wpf.Views
{
    public class BaseView : Window
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
        public BaseView()
        {
            Loaded += OnViewLoaded;
            Unloaded += OnViewUnloaded;
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
            if (_viewModel is not null)
            {
                _viewModel.SetResultAction = SetResult;
                _viewModel.CloseAction = Close;
            }
        }

        protected virtual void OnWindowUnloaded()
        {
        }
        #endregion

        #region Events
        private void OnViewLoaded(object sender, RoutedEventArgs e) => OnWindowLoaded();
        private void OnViewUnloaded(object sender, RoutedEventArgs e) => OnWindowUnloaded();
        #endregion

        #region IView
        public void SetResult(bool result)
        {
            DialogResult = result;
        }
        #endregion
    }
}