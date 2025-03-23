using CommunityToolkit.Mvvm.ComponentModel;

namespace CookBook.Common.Models;

public class ModelBase : ObservableObject
{
    public Guid? Id { get; set; }
}