using CommunityToolkit.Mvvm.ComponentModel;
using CookBook.Common.Enums;

namespace CookBook.Common.Models;

public partial class RecipeListModel : ModelBase
{
    [ObservableProperty]
    private Guid id;

    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private FoodType foodType;

    [ObservableProperty]
    private string? imageUrl;
}
