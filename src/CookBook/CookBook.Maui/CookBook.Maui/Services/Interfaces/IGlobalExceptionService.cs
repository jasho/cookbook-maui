using System.Runtime.CompilerServices;

namespace CookBook.Maui.Services.Interfaces
{
    public interface IGlobalExceptionService
    {
        void HandleException(Exception exception, [CallerMemberName] string? source = null);
    }
}