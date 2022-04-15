namespace KamishibaiApp
{
    // Remove this class once your pages/features are using your data.
    // This is used by the SampleDataService.
    // It is the model class we use to display data on pages like Grid, Chart, and List Details.
    public class SampleCompany
    {
        public SampleCompany(string companyId, string companyName, string contactName, string contactTitle, string address, string city, string postalCode, string country, string phone, string fax, ICollection<SampleOrder> orders)
        {
            CompanyId = companyId;
            CompanyName = companyName;
            ContactName = contactName;
            ContactTitle = contactTitle;
            Address = address;
            City = city;
            PostalCode = postalCode;
            Country = country;
            Phone = phone;
            Fax = fax;
            Orders = orders;
        }

        public string CompanyId { get; }

        public string CompanyName { get; }

        public string ContactName { get; }

        public string ContactTitle { get; }

        public string Address { get; }

        public string City { get; }

        public string PostalCode { get; }

        public string Country { get; }

        public string Phone { get; }

        public string Fax { get; }

        public ICollection<SampleOrder> Orders { get; }
    }
}
