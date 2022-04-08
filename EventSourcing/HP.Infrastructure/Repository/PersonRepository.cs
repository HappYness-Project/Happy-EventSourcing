using HP.Domain.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Infrastructure.Repository
{
    public class PersonRepository : IPersonRepository
    {
        //private readonly PeopleContext _context;
        public PersonRepository()
        {
            //this._context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public void Add(Person aggregate)
        {
            throw new NotImplementedException();
        }

        public void Delete(Person aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeletePersonAsync(string personId)
        {
            throw new NotImplementedException();
        }

        public Person Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetPersonAsync(string personId)
        {
            throw new NotImplementedException();
        }

        public void Update(Person aggregate)
        {
            throw new NotImplementedException();
        }

        public Task<Person> UpdatePersonAsync(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
