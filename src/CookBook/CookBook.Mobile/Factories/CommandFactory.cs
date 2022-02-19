using CookBook.Mobile.Commands;
using System.Linq.Expressions;
using System.Windows.Input;

namespace CookBook.Mobile.Factories;

public class CommandFactory : ICommandFactory
{
    public ICommand CreateCommand(Action execute, Expression<Func<bool>>? canExecute)
    {
        return new Commands.Command(execute, canExecute?.Compile());
    }

    public ICommand CreateCommand<T>(Action<T> execute, Expression<Func<T, bool>>? canExecute = null)
    {
        return new Commands.Command<T>(execute, canExecute?.Compile());
    }

    public AsyncCommand CreateCommand(Func<Task> execute, Func<bool>? canExecute = null)
    {
        return new AsyncCommand(execute, canExecute);
    }

    public AsyncCommand<T> CreateCommand<T>(Func<T, Task> execute, Func<T, bool>? canExecute = null)
    {
        return new AsyncCommand<T>(execute, canExecute);
    }
}
