using System.Windows.Input;

namespace Deve.Clients.Wpf.Helpers
{
    public class Command : ICommand
    {
        public delegate void ICommandOnExecute();
        public delegate void ICommandWithParamOnExecute(object? param);
        public delegate bool ICommandOnCanExecute();

        private readonly ICommandOnExecute? _execute;
        private readonly ICommandWithParamOnExecute? _executeWithParam;
        private readonly ICommandOnCanExecute? _canExecute;

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
            else
                _executeWithParam?.Invoke(parameter);
        }
        #endregion
    }
}