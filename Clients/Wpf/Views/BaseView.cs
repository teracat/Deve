using System.ComponentModel;
using System.Windows;
using Deve.Clients.Wpf.ViewModels;

namespace Deve.Clients.Wpf.Views
{
    public class BaseView : Window
    {
        #region Fields
        private BaseViewModel? _viewModel;
        #endregion

        #region Properties
        public BaseViewModel? ViewModel
        {
            get => _viewModel;
            set
            {
                if (_viewModel != value)
                {
                    DataContext = _viewModel = value;
                    if (_viewModel is not null)
                    {
                        _viewModel.SetResultAction = SetResult;
                        _viewModel.CloseAction = Close;
                    }
                }
            }
        }
        #endregion

        #region Constructor
        public BaseView()
        {
        }
        #endregion

        #region Overrides
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (_viewModel is not null && _viewModel.IsBusy)
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region IView
        public void SetResult(bool result)
        {
            DialogResult = result;
        }
        #endregion
    }
}