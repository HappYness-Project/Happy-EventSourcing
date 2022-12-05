using HP.Domain;
using HP.Shared.Requests.People;

namespace HP.Shared.Contacts
{
    public interface IPersonService
    {
        Task CreateAsync(CreatePersonRequest request);
        Task UpdatePersonAsync(UpdatePersonRequest request);
    }
}
