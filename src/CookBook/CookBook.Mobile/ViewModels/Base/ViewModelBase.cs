using Microsoft.Toolkit.Mvvm.Input;

namespace CookBook.Mobile.ViewModels
{
    public abstract partial class ViewModelBase : IViewModel
    {
        public virtual Task OnAppearingAsync()
        {
            return Task.CompletedTask;
        }

        [ICommand]
        private async Task GoBackAsync()
        {
            if (Shell.Current.Navigation.NavigationStack.Count > 1)
            {
                Shell.Current.SendBackButtonPressed();
            }
        }
    }
}