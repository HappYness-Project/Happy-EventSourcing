using HP.Application.DTOs;
using HP.Core.Commands;
using HP.Shared.Common;
using HP.Shared.Requests.People;

namespace HP.Shared.Contacts
{
    public interface IPersonService
    {
        Task<Result<CommandResult>> CreateAsync(CreatePersonRequest request);
        Task<Result<CommandResult>> UpdateAsync(UpdatePersonRequest request);
        Task<Result<PersonDetailsDto>> GetPersonByPersonId(string id);
        Task<Result<List<PersonDetailsDto>>> GetPeopleList();
    }
}
