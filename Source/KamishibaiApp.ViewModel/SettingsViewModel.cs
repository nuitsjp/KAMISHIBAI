using System.Windows.Input;
using Kamishibai.Wpf;
using KamishibaiApp.App;
using KamishibaiApp.Application;
using KamishibaiApp.ViewModel.Service;
using Microsoft.Extensions.Options;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using PropertyChanged;

namespace KamishibaiApp.ViewModel
{
    // TODO WTS: Change the URL for your privacy policy in the appsettings.json file, currently set to https://YourPrivacyUrlGoesHere
    [AddINotifyPropertyChangedInterface]
    public class SettingsViewModel : ObservableObject, INavigatedAware
    {
        private readonly AppConfig _appConfig;
        private readonly IThemeSelectorService _themeSelectorService;
        private readonly IBrowserService _browserService;
        private readonly IApplicationInfoService _applicationInfoService;

        public AppTheme Theme { get; set; }

        public string VersionDescription { get; set; } = string.Empty;

        public ICommand SetThemeCommand { get; }

        public ICommand PrivacyStatementCommand { get; }

        public SettingsViewModel(IOptions<AppConfig> appConfig, IThemeSelectorService themeSelectorService, IBrowserService browserService, IApplicationInfoService applicationInfoService)
        {
            _appConfig = appConfig.Value;
            _themeSelectorService = themeSelectorService;
            _browserService = browserService;
            _applicationInfoService = applicationInfoService;
            SetThemeCommand = new RelayCommand<string>(OnSetTheme);
            PrivacyStatementCommand = new RelayCommand(OnPrivacyStatement);
        }

        private void OnSetTheme(string? themeName)
        {
            var theme = (AppTheme)Enum.Parse(typeof(AppTheme), themeName!);
            _themeSelectorService.SetTheme(theme);
        }

        private void OnPrivacyStatement()
            => _browserService.OpenInWebBrowser(_appConfig.PrivacyStatement);

        public void OnNavigated()
        {
            VersionDescription = $"{Properties.Resources.AppDisplayName} - {_applicationInfoService.GetVersion()}";
            Theme = _themeSelectorService.GetCurrentTheme();
        }
    }
}
