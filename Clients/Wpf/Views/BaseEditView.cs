using System.Windows.Input;
using Deve.Clients.Wpf.ViewModels;

namespace Deve.Clients.Wpf.Views
{
    public class BaseEditView : BaseView
    {
        #region Fields
        #endregion

        #region Properties
        protected new BaseEditViewModel? ViewModel
        {
            get;
            set
            {
                if (field != value)
                {
                    DataContext = base.ViewModel = field = value;
                }
            }
        }
        #endregion

        #region Overrides
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.Escape)
            {
                _ = (ViewModel?.CancelCommand.Execute().Subscribe());
            }
        }
        #endregion

        #region Events
        protected void OnLastControlKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && ViewModel is not null)
            {
                _ = ViewModel.SaveCommand.Execute().Subscribe();
            }
        }
        #endregion
    }
}