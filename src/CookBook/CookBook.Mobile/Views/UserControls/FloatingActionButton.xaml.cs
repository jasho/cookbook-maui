using System.Windows.Input;
using Maui.BindableProperty.Generator.Core;

namespace CookBook.Mobile.Views.UserControls;

public partial class FloatingActionButton : ContentView {
	public FloatingActionButton() {
		InitializeComponent();
	}

	[AutoBindable]
	private ICommand command;

	[AutoBindable]
	private string text;
}