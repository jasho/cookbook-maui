using System.Runtime.CompilerServices;

namespace CookBook.Mobile.Services
{
    public interface IGlobalExceptionService
    {
        void HandleException(Exception exception, [CallerMemberName] string? source = null);
    }
}