//using CookBook.Mobile.ViewModels;

//namespace CookBook.Mobile.Views;

//public abstract class ContentPageBase : ContentPage
//{
//    protected IViewModel viewModel { get; }

//    public ContentPageBase(IViewModel viewModel)
//    {
//        BindingContext = this.viewModel = viewModel;
//    }

//    protected override async void OnAppearing()
//    {
//        base.OnAppearing();
//        await viewModel.OnAppearingAsync();
//    }
//}