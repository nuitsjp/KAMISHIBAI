using Kamishibai;
using SampleBrowser.ViewModel.Page;

namespace SampleBrowser.ViewModel;

public class MainViewModel : INavigatedAsyncAware
{
    private MenuItem _selectedMenuItem;

    private readonly IPresentationService _presentationService;

    public MainViewModel(IPresentationService presentationService)
    {
        _presentationService = presentationService;
        _selectedMenuItem = SampleItems.First();
    }

    public IReadOnlyList<MenuItem> SampleItems { get; } = new List<MenuItem>
    {
        new ("Navigation", typeof(NavigationMenuViewModel)),
        new ("Open Window", typeof(OpenWindowViewModel)),
        new ("Open Dialog", typeof(OpenDialogViewModel)),
        new ("Show Message", typeof(ShowMessageViewModel)),
        new ("Open File", typeof(OpenFileViewModel)),

    };

    public MenuItem SelectedMenuItem
    {
        get => _selectedMenuItem;
        set
        {
            _selectedMenuItem = value;
            _presentationService.NavigateAsync(_selectedMenuItem.ViewModel);
        }
    }

    public Task OnNavigatedAsync()
    {
        return _presentationService.NavigateAsync(_selectedMenuItem.ViewModel);
    }
}