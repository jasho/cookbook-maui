using CookBook.Maui.Services.Interfaces;
using CookBook.Maui.ViewModels;

namespace CookBook.Maui.Pages.Base;

public abstract partial class ContentPageBase : ContentPage
{
    private readonly IGlobalExceptionService globalExceptionService;
    protected ViewModelBase ViewModel { get; }

    protected ContentPageBase(
        ViewModelBase viewModel,
        IGlobalExceptionService globalExceptionService)
    {
        this.globalExceptionService = globalExceptionService;
        BindingContext = ViewModel = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            await ViewModel.OnAppearingAsync();
        }
        catch (Exception e)
        {
            globalExceptionService.HandleException(e);
        }
    }

    public class Dependencies
    {
        public IGlobalExceptionService GlobalExceptionService { get; }

        public Dependencies(IGlobalExceptionService globalExceptionService)
        {
            GlobalExceptionService = globalExceptionService;
        }
    }
}
