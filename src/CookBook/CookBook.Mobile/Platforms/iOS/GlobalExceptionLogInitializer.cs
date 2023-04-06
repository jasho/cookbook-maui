using CookBook.Mobile.Services;

namespace CookBook.Mobile.Platforms
{
    public class GlobalExceptionServiceInitializer : IGlobalExceptionServiceInitializer
    {
        private readonly IGlobalExceptionService globalExceptionService;

        public GlobalExceptionServiceInitializer(IGlobalExceptionService globalExceptionService)
        {
            this.globalExceptionService = globalExceptionService;
        }

        public void Initialize()
        {
            AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
            TaskScheduler.UnobservedTaskException += OnTaskSchedulerUnobservedTaskException;
        }

        private void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception exception)
            {
                globalExceptionService.HandleException(exception);
            }
        }

        private void OnTaskSchedulerUnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
        {
            globalExceptionService.HandleException(e.Exception);
            e.SetObserved();
        }
    }
}