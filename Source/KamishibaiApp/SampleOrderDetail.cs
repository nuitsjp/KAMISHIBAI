namespace KamishibaiApp
{
    // Remove this class once your pages/features are using your data.
    // This is used by the SampleDataService.
    // It is the model class we use to display data on pages like Grid, Chart, and List Details.
    public class SampleOrderDetail
    {
        public SampleOrderDetail(long productId, string productName, int quantity, double discount, string quantityPerUnit, double unitPrice, string categoryName, string categoryDescription, double total)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            Discount = discount;
            QuantityPerUnit = quantityPerUnit;
            UnitPrice = unitPrice;
            CategoryName = categoryName;
            CategoryDescription = categoryDescription;
            Total = total;
        }

        public long ProductId { get; }

        public string ProductName { get; }

        public int Quantity { get; }

        public double Discount { get; }

        public string QuantityPerUnit { get; }

        public double UnitPrice { get; }

        public string CategoryName { get; }

        public string CategoryDescription { get; }

        public double Total { get; }

        public string ShortDescription => $"Product ID: {ProductId} - {ProductName}";
    }
}
