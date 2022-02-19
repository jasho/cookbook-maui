using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CookBook.Common.Models;

public record ModelBase() : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}