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
                    if (field is not null)
                    {
                        field.SetResultAction = SetResult;
                        field.CloseAction = Close;
                    }
                }
            }
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

        #region IView
        public void SetResult(bool result) => DialogResult = result;
        #endregion
    }
}