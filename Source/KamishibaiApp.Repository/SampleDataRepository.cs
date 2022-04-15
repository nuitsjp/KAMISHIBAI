using System.Text.Encodings.Web;
using System.Text.Json;

namespace KamishibaiApp.Repository
{
    // This class holds sample data used by some generated pages to show how they can be used.
    // TODO WTS: The following classes have been created to display sample data. Delete these files once your app is using real data.
    // 1. Contracts/Services/ISampleDataService.cs
    // 2. Services/SampleDataRepository.cs
    // 3. Models/SampleCompany.cs
    // 4. Models/SampleOrder.cs
    // 5. Models/SampleOrderDetail.cs
    public class SampleDataRepository : ISampleDataRepository
    {
        public SampleDataRepository()
        {
        }

        private static IEnumerable<SampleOrder> AllOrders()
        {
            // The following is order summary data
            var companies = AllCompanies();
            return companies.SelectMany(c => c.Orders);
        }

        private static IEnumerable<SampleCompany> AllCompanies()
        {
            using var stream = File.OpenRead("companies.json");
            return JsonSerializer.Deserialize<SampleCompany[]>(
                stream,
                new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                })!;
        }

        // Remove this once your ContentGrid pages are displaying real data.
        public async Task<IEnumerable<SampleOrder>> GetContentGridDataAsync()
        {
            await Task.CompletedTask;
            return AllOrders();
        }

        // Remove this once your DataGrid pages are displaying real data.
        public async Task<IEnumerable<SampleOrder>> GetGridDataAsync()
        {
            await Task.CompletedTask;
            return AllOrders();
        }

        // Remove this once your ListDetails pages are displaying real data.
        public async Task<IEnumerable<SampleOrder>> GetListDetailsDataAsync()
        {
            await Task.CompletedTask;
            return AllOrders();
        }
    }
}
