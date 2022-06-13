using HP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Domain.Person
{
    public static class PersonDomainEvents
    {
        public class AddressChanged : DomainEventBase
        {
            public AddressChanged(string id, string country, string city, string stress, string zipCode) : base(entityId: id,entityType: nameof(Address))
            {
                Country = country;
                City = city;
                Stress = stress;
                ZipCode = zipCode;
            }
            public string Country { get; }
            public string City { get; }
            public string Stress { get; }
            public string ZipCode { get; }
        }
    }
}
