using System.Windows.Input;

namespace Deve.ClientApp.Wpf.Helpers
{
    public class Command : ICommand
    {
        public delegate void ICommandOnExecute();
        public delegate void ICommandWithParamOnExecute(object? param);
        public delegate bool ICommandOnCanExecute();

        private ICommandOnExecute? _execute;
        private ICommandWithParamOnExecute? _executeWithParam;
        private ICommandOnCanExecute? _canExecute;

        public Command(ICommandOnExecute onExecuteMethod, ICommandOnCanExecute? onCanExecuteMethod = null)
        {
            _execute = onExecuteMethod;
            _canExecute = onCanExecuteMethod;
        }

        public Command(ICommandWithParamOnExecute onExecuteWithParamMethod, ICommandOnCanExecute? onCanExecuteMethod = null)
        {
            _executeWithParam = onExecuteWithParamMethod;
            _canExecute = onCanExecuteMethod;
        }

        #region ICommand Members
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        public void Execute(object? parameter)
        {
            if (_execute is not null)
                _execute.Invoke();
            else if (_executeWithParam is not null)
                _executeWithParam.Invoke(parameter);
        }
        #endregion
    }
}
