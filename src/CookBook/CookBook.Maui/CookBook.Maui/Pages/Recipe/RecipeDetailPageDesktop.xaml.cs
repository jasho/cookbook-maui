using CookBook.Maui.Services.Interfaces;
using CookBook.Maui.ViewModels.Recipe;

namespace CookBook.Maui.Pages;

public partial class RecipeDetailPageDesktop
{
    public RecipeDetailPageDesktop(
        RecipeDetailViewModel viewModel,
        IGlobalExceptionService globalExceptionService)
        : base(viewModel, globalExceptionService)
    {
        InitializeComponent();
    }
}