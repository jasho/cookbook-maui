using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CookBook.Mobile.ViewModels;

public abstract class ViewModelBase : INotifyPropertyChanged, IViewModel
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public virtual Task OnAppearingAsync()
    {
        return Task.CompletedTask;
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}