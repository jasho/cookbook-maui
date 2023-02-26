using CommunityToolkit.Mvvm.ComponentModel;

namespace CookBook.Common.Models;

public partial class IngredientListModel : ModelBase
{
    public Guid Id { get; set; }

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string? imageUrl;
}
