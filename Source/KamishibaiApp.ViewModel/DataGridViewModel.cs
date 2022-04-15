using System.Collections.ObjectModel;
using Kamishibai.Wpf;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace KamishibaiApp.ViewModel
{
    public class DataGridViewModel : ObservableObject, INavigatedAsyncAware
    {
        private readonly ISampleDataRepository _sampleDataRepository;

        public ObservableCollection<SampleOrder> Source { get; } = new ObservableCollection<SampleOrder>();

        public DataGridViewModel(ISampleDataRepository sampleDataRepository)
        {
            _sampleDataRepository = sampleDataRepository;
        }

        public async Task OnNavigatedAsync()
        {
            Source.Clear();

            // Replace this with your actual data
            var data = await _sampleDataRepository.GetGridDataAsync();

            foreach (var item in data)
            {
                Source.Add(item);
            }
        }
    }
}
