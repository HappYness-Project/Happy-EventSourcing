namespace HP.Domain
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<Person> UpdatePersonAsync(Person person);
        Task<bool> DeletePersonAsync(string personId);
        Task<Person> GetPersonByUserIdAsync(string UserId);
        Task<IEnumerable<Person>> GetListByGroupIdAsync(int groupId);
        Task<IEnumerable<Person>> GetListByRoleAsync(string role);
    }
}
