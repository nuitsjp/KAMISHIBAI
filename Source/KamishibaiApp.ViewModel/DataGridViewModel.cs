using System.Collections.ObjectModel;
using Kamishibai.Wpf;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace KamishibaiApp.ViewModel
{
    public class DataGridViewModel : ObservableObject, INavigatedAsyncAware
    {
        private readonly ISampleDataRepository _sampleDataRepository;

        public ObservableCollection<Order> Source { get; } = new ObservableCollection<Order>();

        public DataGridViewModel(ISampleDataRepository sampleDataRepository)
        {
            _sampleDataRepository = sampleDataRepository;
        }

        public async Task OnNavigatedAsync()
        {
            Source.Clear();

            // Replace this with your actual data
            var data = await _sampleDataRepository.GetSampleOrdersAsync();

            foreach (var item in data)
            {
                Source.Add(item);
            }
        }
    }
}
