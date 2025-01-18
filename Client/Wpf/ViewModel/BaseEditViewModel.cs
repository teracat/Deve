using System.Windows.Input;
using Deve.ClientApp.Wpf.Window;

namespace Deve.ClientApp.Wpf.ViewModel
{
    public abstract class BaseEditViewModel : BaseViewModel
    {
        #region Fields
        private ICommand? _saveCommand;
        private ICommand? _cancelCommand;
        #endregion

        #region Constructor
        public BaseEditViewModel(BaseWindow window)
            : base(window)
        {
        }
        #endregion

        #region Methods
        internal void DoCancel()
        {
            Window.DialogResult = false;
            Window.Close();
        }

        internal abstract Task DoSave();
        #endregion

        #region Commands
        public ICommand Cancel => _cancelCommand ??= new Command(() => DoCancel(), () => IsIdle);
        public ICommand Save => _saveCommand ??= new Command(() => _ = DoSave(), () => IsIdle);
        #endregion
    }
}