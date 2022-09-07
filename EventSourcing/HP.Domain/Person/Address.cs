using System.Text.RegularExpressions;

namespace HP.Domain
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
            if(string.IsNullOrEmpty(postalCode))
                throw new ArgumentNullException(nameof(postalCode));
            if (!IsPostalCode(postalCode))
                throw new ApplicationException($"Postal Code : {postalCode} invalid format.");
            
                    this.Country = country;
            this.City = city;
            this.Region = region;
            this.PostalCode = postalCode;
        }
        public string Country { get; }
        public string City { get; }
        public string Region { get; }
        public string PostalCode { get; }

        public static bool IsPostalCode(string postalCode)
        {

            //Canadian Postal Code in the format of "M3A 1A5"
            string pattern = "^[ABCEGHJ-NPRSTVXY]{1}[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[ ]?[0-9]{1}[ABCEGHJ-NPRSTV-Z]{1}[0-9]{1}$";

            Regex reg = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            if (!(reg.IsMatch(postalCode)))
                return false;
            return true;
        }

    };
}
