using System;
using System.Windows;
using ControlzEx.Theming;
using KamishibaiApp.ViewModel.Service;
using MahApps.Metro.Theming;

namespace KamishibaiApp.View.Service
{
    public class ThemeSelectorService : IThemeSelectorService
    {
        private const string HcDarkTheme = "pack://application:,,,/KamishibaiApp.View;component/Styles/Themes/HC.Dark.Blue.xaml";
        private const string HcLightTheme = "pack://application:,,,/KamishibaiApp.View;component/Styles/Themes/HC.Light.Blue.xaml";

        public void InitializeTheme()
        {
            // We've added basic high contrast dictionaries for Dark and Light themes
            // Please complete these themes following the docs on https://mahapps.com/docs/themes/thememanager#creating-custom-themes
            ThemeManager.Current.AddLibraryTheme(new LibraryTheme(new Uri(HcDarkTheme), MahAppsLibraryThemeProvider.DefaultInstance));
            ThemeManager.Current.AddLibraryTheme(new LibraryTheme(new Uri(HcLightTheme), MahAppsLibraryThemeProvider.DefaultInstance));

            var theme = GetCurrentTheme();
            SetTheme(theme);
        }

        public void SetTheme(AppTheme theme)
        {
            if (theme == AppTheme.Default)
            {
                ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncAll;
                ThemeManager.Current.SyncTheme();
            }
            else
            {
                ThemeManager.Current.ThemeSyncMode = ThemeSyncMode.SyncWithHighContrast;
                ThemeManager.Current.SyncTheme();
                ThemeManager.Current.ChangeTheme(Application.Current, $"{theme}.Blue", SystemParameters.HighContrast);
            }

            Application.Current.Properties["Theme"] = theme.ToString();
        }

        public AppTheme GetCurrentTheme()
        {
            if (Application.Current.Properties.Contains("Theme"))
            {
                var themeName = Application.Current.Properties["Theme"]!.ToString();
                Enum.TryParse(themeName, out AppTheme theme);
                return theme;
            }

            return AppTheme.Default;
        }
    }
}
