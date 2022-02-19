using CookBook.Mobile.Commands;
using System.Linq.Expressions;
using System.Windows.Input;

namespace CookBook.Mobile.Factories;

public interface ICommandFactory
{
    ICommand CreateCommand(Action execute, Expression<Func<bool>>? canExecute = null);
    ICommand CreateCommand<T>(Action<T> execute, Expression<Func<T, bool>>? canExecute = null);
    AsyncCommand CreateCommand(Func<Task> execute, Func<bool>? canExecute = null);
    AsyncCommand<T> CreateCommand<T>(Func<T, Task> execute, Func<T, bool>? canExecute = null);
}