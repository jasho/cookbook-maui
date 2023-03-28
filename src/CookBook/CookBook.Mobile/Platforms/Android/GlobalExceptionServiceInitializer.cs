using CookBook.Mobile.Services;

namespace CookBook.Mobile.Platforms
{
    public class GlobalExceptionServiceInitializer : IGlobalExceptionServiceInitializer, IDisposable
    {
        private readonly IGlobalExceptionService globalExceptionService;
        private readonly GlobalUncaughtExceptionHandler globalExceptionHandler;

        public GlobalExceptionServiceInitializer(IGlobalExceptionService globalExceptionService)
        {
            this.globalExceptionService = globalExceptionService;
            globalExceptionHandler = new GlobalUncaughtExceptionHandler(globalExceptionService);
        }

        public void Initialize()
        {
            Java.Lang.Thread.DefaultUncaughtExceptionHandler = globalExceptionHandler;
            Java.Lang.Thread.CurrentThread().UncaughtExceptionHandler = globalExceptionHandler;

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
        }

        public void Dispose()
        {
            globalExceptionHandler.Dispose();
        }
    }
}