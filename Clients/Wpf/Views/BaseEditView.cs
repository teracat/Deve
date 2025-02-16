using System.Windows.Input;
using Deve.Clients.Wpf.ViewModels;

namespace Deve.Clients.Wpf.Views
{
    public class BaseEditView : BaseView
    {
        #region Fields
        private BaseEditViewModel? _viewModel;
        #endregion

        #region Properties
        protected new BaseEditViewModel? ViewModel
        {
            get => _viewModel;
            set
            {
                if (_viewModel != value)
                {
                    DataContext = base.ViewModel = _viewModel = value;
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
                _viewModel?.DoCancel();
            }
        }
        #endregion
    }
}
