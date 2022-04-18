using System.Text.Encodings.Web;
using System.Text.Json;

namespace KamishibaiApp.Repository
{
    // This class holds sample data used by some generated pages to show how they can be used.
    // TODO WTS: The following classes have been created to display sample data. Delete these files once your app is using real data.
    // 1. Contracts/Services/ISampleDataService.cs
    // 2. Services/SampleDataRepository.cs
    // 3. Models/Company.cs
    // 4. Models/Order.cs
    // 5. Models/OrderDetail.cs
    public class SampleDataRepository : ISampleDataRepository
    {
        public async Task<IEnumerable<Order>> GetSampleOrdersAsync()
        {
            await using var stream = File.OpenRead("companies.json");
            var option = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };
            var companies =
                await JsonSerializer.DeserializeAsync<Company[]>(stream, option)
                ?? Array.Empty<Company>();
            return companies.SelectMany(x => x.Orders);
        }
    }
}
