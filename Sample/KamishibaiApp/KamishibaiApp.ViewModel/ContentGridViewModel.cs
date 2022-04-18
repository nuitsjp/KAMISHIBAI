using System.Collections.ObjectModel;
using Kamishibai.Wpf;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace KamishibaiApp.ViewModel;

public class ContentGridViewModel : ObservableObject, INavigatedAsyncAware
{
    private readonly IPresentationService _presentationService;
    private readonly ISampleDataRepository _sampleDataService;

    public ContentGridViewModel(ISampleDataRepository sampleDataService, IPresentationService presentationService)
    {
        _sampleDataService = sampleDataService;
        _presentationService = presentationService;
        NavigateToDetailCommand = new AsyncRelayCommand<Order>(NavigateToDetail);
    }

    public AsyncRelayCommand<Order> NavigateToDetailCommand { get; }

    public ObservableCollection<Order> Source { get; } = new();

    private Task NavigateToDetail(Order? order)
    {
        return _presentationService.NavigateToContentGridDetailAsync(order!.OrderId);
    }

    public async Task OnNavigatedAsync()
    {
        Source.Clear();

        // Replace this with your actual data
        var data = await _sampleDataService.GetSampleOrdersAsync();
        foreach (var item in data)
        {
            Source.Add(item);
        }
    }
}
