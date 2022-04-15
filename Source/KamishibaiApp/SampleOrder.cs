namespace KamishibaiApp
{
    // Remove this class once your pages/features are using your data.
    // This is used by the SampleDataService.
    // It is the model class we use to display data on pages like Grid, Chart, and List Details.
    public class SampleOrder
    {
        public SampleOrder(long orderId, DateTime orderDate, DateTime requiredDate, DateTime shippedDate, string shipperName, string shipperPhone, double freight, string company, string shipTo, double orderTotal, string status, int symbolCode, ICollection<SampleOrderDetail> details)
        {
            OrderId = orderId;
            OrderDate = orderDate;
            RequiredDate = requiredDate;
            ShippedDate = shippedDate;
            ShipperName = shipperName;
            ShipperPhone = shipperPhone;
            Freight = freight;
            Company = company;
            ShipTo = shipTo;
            OrderTotal = orderTotal;
            Status = status;
            SymbolCode = symbolCode;
            Details = details;
        }

        public long OrderId { get; }

        public DateTime OrderDate { get; }

        public DateTime RequiredDate { get; }

        public DateTime ShippedDate { get; }

        public string ShipperName { get; }

        public string ShipperPhone { get; }

        public double Freight { get; }

        public string Company { get; }

        public string ShipTo { get; }

        public double OrderTotal { get; }

        public string Status { get; }

        public char Symbol => (char)SymbolCode;

        public int SymbolCode { get; }

        public ICollection<SampleOrderDetail> Details { get; }

        public override string ToString()
        {
            return $"{Company} {Status}";
        }

        public string ShortDescription => $"Order ID: {OrderId}";
    }
}
