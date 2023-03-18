using CookBook.App.Components.Common.ViewModels;

namespace CookBook.App.Views;

public partial class SettingsView
{
    public SettingsView(SettingsViewModel viewModel)
        : base(viewModel)
    {
        InitializeComponent();
    }
}