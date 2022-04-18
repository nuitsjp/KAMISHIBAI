using System.Collections.ObjectModel;
using Kamishibai.Wpf;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using PropertyChanged;

namespace KamishibaiApp.ViewModel;

[AddINotifyPropertyChangedInterface]
public class ListDetailsViewModel : ObservableObject, INavigatedAsyncAware
{
    private readonly ISampleDataRepository _sampleDataRepository;

    public Order? Selected { get; set; }

    public ObservableCollection<Order> SampleItems { get; } = new();

    public ListDetailsViewModel(ISampleDataRepository sampleDataRepository)
    {
        _sampleDataRepository = sampleDataRepository;
    }

    public async Task OnNavigatedAsync()
    {
        SampleItems.Clear();

        var data = await _sampleDataRepository.GetSampleOrdersAsync();

        foreach (var item in data)
        {
            SampleItems.Add(item);
        }

        Selected = SampleItems.First();
    }
}
