namespace KamishibaiApp
{
    public class Company
    {
        public Company(string companyId, string companyName, string contactName, string contactTitle, string address, string city, string postalCode, string country, string phone, string fax, ICollection<Order> orders)
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

        public ICollection<Order> Orders { get; }
    }
}
