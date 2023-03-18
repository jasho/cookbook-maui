using CookBook.App.Components.Common.ViewModels;

namespace CookBook.App.Components.Common.Views;

public class ContentPageBase : ContentPage
{
	public ContentPageBase(IViewModel  viewModel)
    {
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is IViewModel viewModel)
        {
            await viewModel.OnAppearingAsync();
        }
    }
}