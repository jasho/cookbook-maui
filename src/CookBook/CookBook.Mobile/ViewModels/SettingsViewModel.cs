using CommunityToolkit.Mvvm.Input;
using CookBook.Mobile.Resources.Styles;
using Humanizer;
using System.Globalization;
using Application = Microsoft.Maui.Controls.Application;
using System.Numerics;

#if ANDROID
using Android.Widget;
#elif WINDOWS
using System.Windows;
#endif

namespace CookBook.Mobile.ViewModels;

public partial class SettingsViewModel : IViewModel
{

	private readonly IPreferences preferences;

	public Setting<Theme> SelectedTheme { get; set; }

	public Setting<CultureInfo> SelectedLanguage { get; set; }

	public IEnumerable<Theme> AvailableThemes => (Theme[])typeof(Theme).GetEnumValues();
	public IEnumerable<MyCultureInfo> AvailableLanguages => new MyCultureInfo[] {
		MyCultureInfo.GetCultureInfo("cs"),
		MyCultureInfo.GetCultureInfo("en")
	};

	public SettingsViewModel(IPreferences preferences)
	{
		this.preferences = preferences;

		var defaultLang = Thread.CurrentThread.CurrentCulture.IetfLanguageTag.Split('-')[0];
		string storedLang = preferences.Get(nameof(SelectedLanguage), defaultLang);

		SelectedLanguage = new Setting<CultureInfo>(CultureInfo.GetCultureInfo(storedLang), ApplyCulture);
		SelectedTheme = new Setting<Theme>((Theme)preferences.Get(nameof(SelectedTheme), 2), ApplyTheme);
	}

	[RelayCommand]
	private void ApplyChanges()
	{
		if (SelectedTheme.Changed)
		{
			SelectedTheme.Apply();
		}
		if (SelectedLanguage.Changed)
		{
			SelectedLanguage.Apply();
		}
	}

	private void ApplyCulture()
	{
		App.Current.Dispatcher.Dispatch(() =>
		{
			Thread.CurrentThread.CurrentCulture = SelectedLanguage.Value;
			Thread.CurrentThread.CurrentUICulture = SelectedLanguage.Value;
		});
		preferences.Set(nameof(SelectedLanguage), SelectedLanguage.Value.IetfLanguageTag.Split('-')[0]);
#if ANDROID
		Toast.MakeText(Android.App.Application.Context, "Restart for changes to take effect!", ToastLength.Short).Show();
#elif WINDOWS
		MessageBox.Show("Restart for changes to take effect!");
#endif
	}

	private void ApplyTheme()
	{
		var bgName = nameof(View.BackgroundColor);
		var currentTheme = Application.Current.Resources.MergedDictionaries.FirstOrDefault(x => x.Keys.Contains(bgName));
		var systemTheme = Application.Current.RequestedTheme;
		if (((Color)currentTheme[bgName]).Equals(Colors.White))
		{
			// Is in light theme
			if (SelectedTheme.Value == Theme.Light)
			{
				return;
			}

			Application.Current.Resources.MergedDictionaries.Remove(currentTheme);
			if (SelectedTheme.Value == Theme.Dark)
			{
				Application.Current.Resources.MergedDictionaries.Add(new DarkTheme());
			}
			else
			{
				Application.Current.Resources.MergedDictionaries.Add(systemTheme == AppTheme.Light ? new LightTheme() : new DarkTheme());
			}
		}
		else
		{
			// Is in dark theme
			if (SelectedTheme.Value == Theme.Dark)
			{
				return;
			}

			Application.Current.Resources.MergedDictionaries.Remove(currentTheme);
			if (SelectedTheme.Value == Theme.Light)
			{
				Application.Current.Resources.MergedDictionaries.Add(new LightTheme());
			}
			else
			{
				Application.Current.Resources.MergedDictionaries.Add(systemTheme == AppTheme.Light ? new LightTheme() : new DarkTheme());
			}
		}
		preferences.Set(nameof(SelectedTheme), (int)SelectedTheme.Value);
	}

	public Task OnAppearingAsync()
	{
		return Task.CompletedTask;
	}
}

public enum Theme
{
	Light,
	Dark,
	System
}

public class MyCultureInfo : CultureInfo
{
	public MyCultureInfo(string name) : base(name)
	{
	}

	public static MyCultureInfo GetCultureInfo(string name)
	{
		return new MyCultureInfo(CultureInfo.GetCultureInfo(name).Name);
	}

	public override string ToString()
	{
		return NativeName.ApplyCase(LetterCasing.Title);
	}
}

public class Setting<T>
{
	private T appliedValue;
	private Action applyAction;
	public T Value { get; set; }
	public bool Changed => !Value.Equals(appliedValue);

	public Setting(T initialValue, Action applyAction)
	{
		Value = appliedValue = initialValue;
		this.applyAction = applyAction;
	}

	public void Apply()
	{
		applyAction();
		appliedValue = Value;
	}
}