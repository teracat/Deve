using System.ComponentModel;
using System.Windows;
using Deve.Clients.Wpf.ViewModels;

namespace Deve.Clients.Wpf.Views
{
    public class BaseView : Window
    {
        #region Properties
        public BaseViewModel? ViewModel
        {
            get;
            set
            {
                if (field != value)
                {
                    DataContext = field = value;
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
            if (ViewModel is not null && ViewModel.IsBusy)
            {
                e.Cancel = true;
            }
        }
        #endregion

        #region Methods
        protected virtual void OnWindowLoaded()
        {
            if (ViewModel is not null)
            {
                ViewModel.SetResultAction = SetResult;
                ViewModel.CloseAction = Close;
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
        public void SetResult(bool result) => DialogResult = result;
        #endregion
    }
}