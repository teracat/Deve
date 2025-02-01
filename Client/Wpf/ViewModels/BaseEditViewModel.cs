using System.Windows.Input;
using Deve.ClientApp.Wpf.Helpers;

namespace Deve.ClientApp.Wpf.ViewModels
{
    public abstract class BaseEditViewModel : BaseViewModel
    {
        #region Fields
        private ICommand? _saveCommand;
        private ICommand? _cancelCommand;
        #endregion

        #region Constructor
        public BaseEditViewModel()
        {
        }
        #endregion

        #region Methods
        internal void DoCancel()
        {
            SetResult(false);
            Close();
        }

        internal abstract Task DoSave();
        #endregion

        #region Commands
        public ICommand Cancel => _cancelCommand ??= new Command(() => DoCancel(), () => IsIdle);
        public ICommand Save => _saveCommand ??= new Command(() => _ = DoSave(), () => IsIdle);
        #endregion
    }
}