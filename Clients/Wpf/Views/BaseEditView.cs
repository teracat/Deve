using System.Windows.Input;
using Deve.Clients.Wpf.ViewModels;

namespace Deve.Clients.Wpf.Views
{
    public class BaseEditView : BaseView
    {
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
                ViewModel?.DoCancel();
            }
        }
        #endregion
    }
}
