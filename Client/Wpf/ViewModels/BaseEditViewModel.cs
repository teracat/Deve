using CommunityToolkit.Mvvm.Input;

namespace Deve.ClientApp.Wpf.ViewModels
{
    public abstract partial class BaseEditViewModel : BaseViewModel
    {
        #region Methods
        [RelayCommand(CanExecute = nameof(IsIdle))]
        internal void Cancel()
        {
            SetResult(false);
            Close();
        }

        [RelayCommand(CanExecute = nameof(IsIdle))]
        internal abstract Task Save();
        #endregion

        #region Overrides
        protected override void OnIsBusyChanged()
        {
            CancelCommand.NotifyCanExecuteChanged();
            SaveCommand.NotifyCanExecuteChanged();
        }
        #endregion
    }
}