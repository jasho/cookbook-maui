namespace CookBook.App.Handlers;

public interface ICustomEntry : IView {
    public string Text { get; }
    public Color TextColor { get; }
}