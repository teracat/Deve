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

        public Command(ICommandOnExecute execute, ICommandOnCanExecute? onCanExecuteMethod)
        {
            ArgumentNullException.ThrowIfNull(execute);
            _execute = execute;
            _canExecute = onCanExecuteMethod;
        }

        public Command(ICommandOnExecute execute)
        {
            ArgumentNullException.ThrowIfNull(execute);
            _execute = execute;
        }

        public Command(ICommandWithParamOnExecute executeWithParam, ICommandOnCanExecute? onCanExecuteMethod)
        {
            ArgumentNullException.ThrowIfNull(executeWithParam);
            _executeWithParam = executeWithParam;
            _canExecute = onCanExecuteMethod;
        }

        public Command(ICommandWithParamOnExecute executeWithParam)
        {
            ArgumentNullException.ThrowIfNull(executeWithParam);
            _executeWithParam = executeWithParam;
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