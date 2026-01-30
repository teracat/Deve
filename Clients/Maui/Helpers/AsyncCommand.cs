using System.Windows.Input;

namespace Deve.Clients.Maui.Helpers;

internal sealed class AsyncCommand : ICommand
{
    private readonly Func<Task>? _execute;
    private readonly Func<object?, Task>? _executeWithParam;
    private readonly Func<bool>? _canExecute;
    private bool _isExecuting;

    public event EventHandler? CanExecuteChanged;

    public AsyncCommand(Func<Task> execute, Func<bool>? canExecute)
    {
        ArgumentNullException.ThrowIfNull(execute);
        _execute = execute;
        _canExecute = canExecute;
    }

    public AsyncCommand(Func<Task> execute)
    {
        ArgumentNullException.ThrowIfNull(execute);
        _execute = execute;
    }

    public AsyncCommand(Func<object?, Task> execute, Func<bool>? canExecute)
    {
        ArgumentNullException.ThrowIfNull(execute);
        _executeWithParam = execute;
        _canExecute = canExecute;
    }

    public AsyncCommand(Func<object?, Task> execute)
    {
        ArgumentNullException.ThrowIfNull(execute);
        _executeWithParam = execute;
    }

    public bool CanExecute(object? parameter) => !_isExecuting && (_canExecute?.Invoke() ?? true);

    public async Task ExecuteAsync(object? parameter)
    {
        if (!CanExecute(null))
        {
            return;
        }

        _isExecuting = true;
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        try
        {
            if (_execute is not null)
            {
                await _execute();
            }
            else if (_executeWithParam is not null)
            {
                await _executeWithParam(parameter);
            }
        }
        finally
        {
            _isExecuting = false;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    void ICommand.Execute(object? parameter) => _ = ExecuteAsync(parameter);
}
