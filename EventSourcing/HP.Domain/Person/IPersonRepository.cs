

namespace HP.Domain
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<Person> UpdatePersonAsync(Person person);
        Task<bool> DeletePersonAsync(string personId);
        Task<IEnumerable<Person>> GetListByGroupIdAsync(string groupId);
        Task<IEnumerable<Person>> GetListByTagAsync(string tag);
        Task<IEnumerable<Person>> GetListByRoleAsync(string role);
    }
}
