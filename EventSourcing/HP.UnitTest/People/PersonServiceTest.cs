using HP.Core.Events;
using HP.Infrastructure.Repository;
using HP.Infrastructure;
using HP.test;
using NUnit.Framework;
using HP.Shared.Contacts;
using HP.Infrastructure.Repository.Write;
using HP.Domain.People.Write;

namespace HP.UnitTest.People
{
    public class PersonServiceTest : TestBase
    {
        IPersonService _personService = null;
        IPersonAggregateRepository _personRepository = null;
        [SetUp]
        public void Setup()
        {
            _personRepository = new PersonAggregateRepository(_mongoDbContext);
        }
    }
}
