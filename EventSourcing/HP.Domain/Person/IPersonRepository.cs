

namespace HP.Domain
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<Person> UpdatePersonAsync(Person person);
        Task<bool> DeletePersonAsync(string personId);
    }
}
