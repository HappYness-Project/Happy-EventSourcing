using HP.Application.DTOs;
using HP.Core.Commands;
using HP.Shared.Common;
using HP.Shared.Requests.Persons;

namespace HP.Shared.Contacts
{
    public interface IPersonService
    {
        Task<Result<CommandResult>> CreateAsync(CreatePersonDto request);
        Task<Result<CommandResult>> UpdateAsync(string personId, UpdatePersonDto request);
        Task<Result<PersonDetailsDto>> GetPersonByPersonId(string id);
        Task<IEnumerable<PersonDetailsDto>> GetPeopleList();
    }
}
