using CookBook.Mobile.Services;
using Java.Lang;

namespace CookBook.Mobile.Platforms
{
    public class GlobalUncaughtExceptionHandler : Java.Lang.Object, Java.Lang.Thread.IUncaughtExceptionHandler
    {
        private readonly IGlobalExceptionService globalExceptionService;

        public GlobalUncaughtExceptionHandler(IGlobalExceptionService globalExceptionService)
        {
            this.globalExceptionService = globalExceptionService;
        }

        public void UncaughtException(Java.Lang.Thread t, Throwable e)
        {
            globalExceptionService.HandleException(e, nameof(GlobalUncaughtExceptionHandler));
        }
    }
}