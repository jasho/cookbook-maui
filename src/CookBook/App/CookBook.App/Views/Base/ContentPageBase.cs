using CookBook.App.ViewModels;

namespace CookBook.App.Views;

public abstract class ContentPageBase : ContentPage {

    protected ContentPageBase(IViewModel viewModel) {
        BindingContext = viewModel;
    }

    protected override void OnAppearing() {
        base.OnAppearing();

        if (BindingContext is IViewModel viewModel)
        {
            Task.Run(() => viewModel.OnAppearingAsync());
        }
    }
}