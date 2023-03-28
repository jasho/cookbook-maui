using CookBook.Mobile.Services;
using System.Windows.Input;

namespace CookBook.Mobile.Commands
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IGlobalExceptionService globalExceptionService;

        public CommandFactory(IGlobalExceptionService globalExceptionService)
        {
            this.globalExceptionService = globalExceptionService;
        }

        public ICommand Create(Action action)
            => new Command(action, () => true, exception => globalExceptionService.HandleException(exception));

        public Command<T> Create<T>(Func<T, Task> action, Func<T, bool>? canExecute = null)
            => new (action, canExecute, exception => globalExceptionService.HandleException(exception));
    }
}