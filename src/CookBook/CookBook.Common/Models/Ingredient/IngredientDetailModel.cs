using CommunityToolkit.Mvvm.ComponentModel;

namespace CookBook.Common.Models;

public partial class IngredientDetailModel : ModelBase
{
    [ObservableProperty]
    private Guid? id;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string? description;

    [ObservableProperty]
    private string? imageUrl;
}
