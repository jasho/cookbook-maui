using CookBook.Maui.Services.Interfaces;
using CookBook.Maui.ViewModels;

namespace CookBook.Maui.Pages;

public partial class SettingsView
{
    public SettingsView(
        SettingsViewModel viewModel,
        IGlobalExceptionService globalExceptionService)
        : base(viewModel, globalExceptionService)
    {
        InitializeComponent();
    }
}