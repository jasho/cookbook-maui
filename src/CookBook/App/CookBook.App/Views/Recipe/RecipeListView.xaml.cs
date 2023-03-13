using CookBook.App.ViewModels;

namespace CookBook.App.Views;

public partial class RecipeListView
{
    public RecipeListView(RecipeListViewModel viewModel)
        : base(viewModel)
    {
        TimingHelper.Log("START");
        InitializeComponent();
        TimingHelper.Log("END");
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        TimingHelper.Log("END");
    }
}