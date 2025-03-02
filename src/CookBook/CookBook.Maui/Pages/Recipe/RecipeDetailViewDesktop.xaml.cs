using CookBook.Maui.Services.Interfaces;
using CookBook.Maui.ViewModels.Recipe;

namespace CookBook.Maui.Pages;

public partial class RecipeDetailViewDesktop
{
    public RecipeDetailViewDesktop(
        RecipeDetailViewModel viewModel,
        IGlobalExceptionService globalExceptionService)
        : base(viewModel, globalExceptionService)
    {
        InitializeComponent();
    }
}