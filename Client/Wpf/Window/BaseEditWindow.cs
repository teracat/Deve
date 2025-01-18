using System.Windows.Input;
using Deve.ClientApp.Wpf.ViewModel;

namespace Deve.ClientApp.Wpf.Window
{
    public class BaseEditWindow : BaseWindow
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

        #region Constructor
        public BaseEditWindow()
        {
        }
        #endregion

        #region Overrides
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.Escape)
                _viewModel?.DoCancel();
        }
        #endregion
    }
}
