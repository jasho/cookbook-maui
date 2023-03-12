using CookBook.App.Components.ViewModels;

namespace CookBook.App.Components.Views;

public class ContentPageBase : ContentPage
{
	public ContentPageBase(IViewModel  viewModel)
    {
        BindingContext = viewModel;
    }
}