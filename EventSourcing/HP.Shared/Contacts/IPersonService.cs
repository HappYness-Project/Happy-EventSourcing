using HP.Domain;

namespace HP.Shared.Contacts
{
    public interface IPersonService
    {
        Task CreateAsync(Person person);
    }
}
