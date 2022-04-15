namespace KamishibaiApp
{
    public interface ISampleDataRepository
    {
        Task<IEnumerable<Order>> GetSampleOrdersAsync();
    }
}
