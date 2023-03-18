using CookBook.App.Components.Common.ViewModels;

namespace CookBook.App.Components.Common.Views;

public class ContentPageBase : ContentPage
{
	public ContentPageBase(IViewModel  viewModel)
    {
        BindingContext = viewModel;
    }
}