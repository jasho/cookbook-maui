using CookBook.Maui.Services.Interfaces;
using CookBook.Maui.ViewModels.Recipe;

namespace CookBook.Maui.Pages;

public partial class RecipeListPage
{
    public RecipeListPage(
        RecipeListViewModel viewModel,
        IGlobalExceptionService globalExceptionService)
        : base(viewModel, globalExceptionService)
    {
        InitializeComponent();
    }
}