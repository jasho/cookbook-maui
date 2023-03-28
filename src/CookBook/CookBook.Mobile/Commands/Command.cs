using System.Windows.Input;

namespace CookBook.Mobile.Commands
{
    public class Command : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool>? canExecute;
        private readonly Action<Exception>? exceptionHandler;

        public Command(
            Action execute,
            Func<bool>? canExecute = null,
            Action<Exception>? exceptionHandler = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
            this.exceptionHandler = exceptionHandler;
        }

        public bool CanExecute(object parameter)
            => canExecute?.Invoke() ?? true;

        public void Execute(object parameter)
        {
            try
            {
                execute.Invoke();
            }
            catch (Exception e) when (exceptionHandler is not null)
            {
                exceptionHandler.Invoke(e);
            }
        }

        public event EventHandler? CanExecuteChanged;
    }

    public class Command<T> : ICommand
    {
        private readonly Func<T, Task> execute;
        private readonly Func<T, bool>? canExecute;
        private readonly Action<Exception>? alternateExceptionHandling;

        public Command(
            Func<T, Task> execute,
            Func<T, bool>? canExecute = null,
            Action<Exception>? alternateExceptionHandling = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
            this.alternateExceptionHandling = alternateExceptionHandling;
        }

        public bool CanExecute(object? parameter)
            => (parameter is T typedParameter) && (canExecute?.Invoke(typedParameter) ?? true);

        public async void Execute(object? parameter)
        {
            if (parameter is T typedParameter)
            {
                try
                {
                    await execute.Invoke(typedParameter);
                }
                catch (Exception e) when (alternateExceptionHandling is not null)
                {
                    alternateExceptionHandling.Invoke(e);
                }
            }
        }

        public event EventHandler? CanExecuteChanged;
    }
}