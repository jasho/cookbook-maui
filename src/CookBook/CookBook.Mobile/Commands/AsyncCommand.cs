using System.Windows.Input;

namespace CookBook.Mobile.Commands;

public class AsyncCommand : ICommand
{
    private readonly Func<Task> execute;
    private readonly Func<bool> canExecute;

    public event EventHandler? CanExecuteChanged;

    public AsyncCommand(
        Func<Task> execute,
        Func<bool>? canExecute)
    {
        this.execute = execute;
        this.canExecute = canExecute ?? (() => true);
    }

    public bool CanExecute(object? parameter)
        => canExecute.Invoke();

    public void Execute(object? parameter)
        => ExecuteAsync();

    public Task ExecuteAsync()
        => execute.Invoke();

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}

public class AsyncCommand<T> : ICommand
{
    private readonly Func<T, Task> execute;
    private readonly Func<T, bool> canExecute;
    
    public event EventHandler? CanExecuteChanged;

    public AsyncCommand(
        Func<T, Task> execute,
        Func<T, bool>? canExecute)
    {
        this.execute = execute;
        this.canExecute = canExecute ?? (T => true);
    }

    public bool CanExecute(object? parameter)
        => (parameter is T typedParameter) && canExecute.Invoke(typedParameter);

    public void Execute(object? parameter)
    {
        if (parameter is T typedParameter)
        {
            ExecuteAsync(typedParameter);
        }
    }

    public Task ExecuteAsync(T parameter)
        => execute.Invoke(parameter);

    public void RaiseCanExecuteChanged()
    {
        CanExecuteChanged?.Invoke(this, new EventArgs());
    }
}