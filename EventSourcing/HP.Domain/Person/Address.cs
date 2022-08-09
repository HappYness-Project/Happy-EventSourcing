namespace HP.Domain.Person
{
    public record Address
    {
        public Address(string country, string city, string region, string postalCode)
        {
            if (string.IsNullOrWhiteSpace(country))
                throw new ArgumentNullException(nameof(country));
            if(string.IsNullOrWhiteSpace(city))
                throw new ArgumentNullException(nameof(city));
            if(string.IsNullOrEmpty(region))
                throw new ArgumentNullException(nameof(region));
            if(string.IsNullOrEmpty(PostalCode))
                throw new ArgumentNullException(nameof(PostalCode));

            this.Country = country;
            this.City = city;
            this.Region = region;
            this.PostalCode = postalCode;
        }
        public string Country { get; }
        public string City { get; }
        public string Region { get; }
        public string PostalCode { get; }
    };
}
