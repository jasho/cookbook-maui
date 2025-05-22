namespace CookBook.Maui.Extensions;

[AcceptEmptyServiceProvider]
public class IdiomObjectExtension : IMarkupExtension
{
    public object? Phone { get; set; }
    public object? Desktop { get; set; }

    public object? ProvideValue(IServiceProvider serviceProvider)
    {
#if DESKTOP
        return Desktop;
#elif PHONE
        return Phone;
#endif
    }
}