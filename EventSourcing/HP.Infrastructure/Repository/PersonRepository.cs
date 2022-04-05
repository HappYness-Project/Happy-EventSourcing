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
        public Task<bool> DeletePersonAsync(string personId)
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetPersonAsync(string personId)
        {
            throw new NotImplementedException();
        }

        public Task<Person> UpdatePersonAsync(Person person)
        {
            throw new NotImplementedException();
        }
    }
}
