using System.Collections.ObjectModel;
using System.Windows.Input;
using Kamishibai.Wpf;
using KamishibaiApp.ViewModel.Properties;
using MahApps.Metro.Controls;
using Microsoft.Toolkit.Mvvm.Input;
using PropertyChanged;

namespace KamishibaiApp.ViewModel;

[AddINotifyPropertyChangedInterface]
[Navigatable]
public class ShellWindowViewModel : INavigatingAware
{
    private readonly INavigationService _navigationService;

    public ShellWindowViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    public HamburgerMenuItem? SelectedMenuItem { get; set; }

    public HamburgerMenuItem? SelectedOptionsMenuItem { get; set; }

    // TODO WTS: Change the icons and titles for all HamburgerMenuItems here.
    public ObservableCollection<HamburgerMenuItem> MenuItems { get; } = new()
        {
            new HamburgerMenuGlyphItem() { Label = Resources.ShellMainPage, Glyph = "\uE8A5", TargetPageType = typeof(MainViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellListDetailsPage, Glyph = "\uE8A5", TargetPageType = typeof(ListDetailsViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellContentGridPage, Glyph = "\uE8A5", TargetPageType = typeof(ContentGridViewModel) },
            new HamburgerMenuGlyphItem() { Label = Resources.ShellDataGridPage, Glyph = "\uE8A5", TargetPageType = typeof(DataGridViewModel) },
        };

    public ObservableCollection<HamburgerMenuItem> OptionMenuItems { get; } = new()
        {
            new HamburgerMenuGlyphItem() { Label = Resources.ShellSettingsPage, Glyph = "\uE713", TargetPageType = typeof(SettingsViewModel) }
        };

    public AsyncRelayCommand GoBackCommand => new(OnGoBack, CanGoBack);

    public ICommand MenuItemInvokedCommand => new RelayCommand(OnMenuItemInvoked);

    public ICommand OptionsMenuItemInvokedCommand => new RelayCommand(OnOptionsMenuItemInvoked);

    public ICommand LoadedCommand => new RelayCommand(OnLoaded);

    public ICommand UnloadedCommand => new RelayCommand(OnUnloaded);

    private void OnLoaded()
    {
        //_navigationService.Navigated += OnNavigated;
    }

    private void OnUnloaded()
    {
        //_navigationService.Navigated -= OnNavigated;
    }

    private bool CanGoBack()
        => _navigationService.CanGoBack;

    private Task OnGoBack()
        => _navigationService.GoBackAsync();

    private void OnMenuItemInvoked()
        => NavigateTo(SelectedMenuItem.TargetPageType);

    private void OnOptionsMenuItemInvoked()
        => NavigateTo(SelectedOptionsMenuItem.TargetPageType);

    private void NavigateTo(Type? targetViewModel)
    {
        if (targetViewModel is not null)
        {
            _navigationService.NavigateAsync(targetViewModel);
        }
    }

    private void OnNavigated(object sender, string viewModelName)
    {
        var item = MenuItems
                    .OfType<HamburgerMenuItem>()
                    .FirstOrDefault(i => viewModelName == i.TargetPageType?.FullName);
        if (item != null)
        {
            SelectedMenuItem = item;
        }
        else
        {
            SelectedOptionsMenuItem = OptionMenuItems
                    .OfType<HamburgerMenuItem>()
                    .FirstOrDefault(i => viewModelName == i.TargetPageType?.FullName);
        }

        GoBackCommand.NotifyCanExecuteChanged();
    }

    public void OnNavigating()
    {
    }
}
