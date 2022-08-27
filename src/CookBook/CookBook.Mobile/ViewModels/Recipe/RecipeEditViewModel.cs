namespace CookBook.Mobile.ViewModels;

[QueryProperty(nameof(Id), "id")]
public partial class RecipeEditViewModel : ViewModelBase
{
    public Guid Id { get; set; }
}