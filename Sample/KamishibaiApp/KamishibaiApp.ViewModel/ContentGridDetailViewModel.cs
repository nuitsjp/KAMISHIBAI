using Kamishibai;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace KamishibaiApp.ViewModel;

[Navigate]
public class ContentGridDetailViewModel : ObservableObject, INavigatedAsyncAware
{
    private readonly long _orderId;
    private readonly ISampleDataRepository _sampleDataService;
    private Order? _item;

    public Order? Item
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

    public async Task OnNavigatedAsync(PostForwardEventArgs args)
    {
        var data = await _sampleDataService.GetSampleOrdersAsync();
        Item = data.First(i => i.OrderId == _orderId);
    }
}
