using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Kamishibai.Wpf;
using KamishibaiApp.ViewModel.Properties;
using MahApps.Metro.Controls;
using Microsoft.Toolkit.Mvvm.Input;
using PropertyChanged;

namespace KamishibaiApp.ViewModel;

[Navigatable]
public class ShellWindowViewModel : ViewModelBase, INavigatingAware, INotifyPropertyChanged
{
    private readonly INavigationService _navigationService;
    private HamburgerMenuItem? _selectedMenuItem;
    private HamburgerMenuItem? _selectedOptionsMenuItem;

    public ShellWindowViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
        GoBackCommand = new(OnGoBack, CanGoBack);
    }

    [DoNotNotify]
    public HamburgerMenuItem? SelectedMenuItem
    {
        get => _selectedMenuItem;
        set
        {
            if (value is not null)
            {
                NavigateTo(value.TargetPageType);
            }
        }
    }

    public HamburgerMenuItem? SelectedOptionsMenuItem
    {
        get => _selectedOptionsMenuItem;
        set
        {
            if (value is not null)
            {
                NavigateTo(value.TargetPageType);
            }
        }
    }

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

    public AsyncRelayCommand GoBackCommand { get; }

    public ICommand MenuItemInvokedCommand => new RelayCommand(OnMenuItemInvoked);

    public ICommand OptionsMenuItemInvokedCommand => new RelayCommand(OnOptionsMenuItemInvoked);

    private bool CanGoBack()
        => _navigationService.CanGoBack();

    private Task OnGoBack()
        => _navigationService.GoBackAsync();

    private void OnMenuItemInvoked()
        => NavigateTo(SelectedMenuItem?.TargetPageType);

    private void OnOptionsMenuItemInvoked()
        => NavigateTo(SelectedOptionsMenuItem?.TargetPageType);

    private void NavigateTo(Type? targetViewModel)
    {
        if (targetViewModel is not null)
        {
            _navigationService.NavigateAsync(targetViewModel);
        }
    }

    public void OnNavigating()
    {
        _navigationService.GetNavigationFrame().Subscribe(viewModel =>
        {
            var item = MenuItems
                .FirstOrDefault(i => viewModel.GetType() == i.TargetPageType);
            if (item != null)
            {
                _selectedMenuItem = item;
                _selectedOptionsMenuItem = null;
            }
            else
            {
                _selectedMenuItem = null;
                _selectedOptionsMenuItem = OptionMenuItems
                    .FirstOrDefault(i => viewModel.GetType() == i.TargetPageType);
            }

            OnPropertyChanged(nameof(SelectedMenuItem));
            OnPropertyChanged(nameof(SelectedOptionsMenuItem));

            GoBackCommand.NotifyCanExecuteChanged();
        }).AddTo(this);
        NavigateTo(MenuItems.First().TargetPageType);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
