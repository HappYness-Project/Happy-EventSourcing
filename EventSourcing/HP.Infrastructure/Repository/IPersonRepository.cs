using HP.Domain.Person;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Infrastructure.Repository
{
    public interface IPersonRepository 
    {
        Task<Person> GetPersonAsync(string personId);
        Task<Person> UpdatePersonAsync(Person person);
        Task<bool> DeletePersonAsync(string personId);
    }
}
