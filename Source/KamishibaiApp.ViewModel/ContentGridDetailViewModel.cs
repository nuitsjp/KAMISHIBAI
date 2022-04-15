using Kamishibai.Wpf;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace KamishibaiApp.ViewModel;

[Navigatable]
public class ContentGridDetailViewModel : ObservableObject, INavigatedAsyncAware
{
    private readonly long _orderId;
    private readonly ISampleDataRepository _sampleDataService;
    private SampleOrder? _item;

    public SampleOrder? Item
    {
        get => _item;
        set => SetProperty(ref _item, value);
    }

    public ContentGridDetailViewModel(
        long orderId,
        [Inject] ISampleDataRepository sampleDataService)
    {
        _orderId = orderId;
        _sampleDataService = sampleDataService;
    }

    public async Task OnNavigatedAsync()
    {
        var data = await _sampleDataService.GetContentGridDataAsync();
        Item = data.First(i => i.OrderId == _orderId);
    }
}
