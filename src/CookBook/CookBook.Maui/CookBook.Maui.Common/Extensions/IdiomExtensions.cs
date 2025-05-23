namespace CookBook.Maui.Extensions;

[AcceptEmptyServiceProvider]
public class IdiomExtension<T> : IMarkupExtension
{
    public T? Phone { get; set; }
    public T? Desktop { get; set; }
    public virtual object? ProvideValue(IServiceProvider serviceProvider)
    {
#if DESKTOP
        return Desktop;
#elif PHONE
        return Phone;
#endif
    }
}

public class IdiomIntExtension : IdiomExtension<int>;
public class IdiomDoubleExtension : IdiomExtension<double>;
public class IdiomThicknessExtension : IdiomExtension<Thickness>;
public class IdiomBoolExtension : IdiomExtension<bool>;
public class IdiomObjectExtension : IdiomExtension<object>;