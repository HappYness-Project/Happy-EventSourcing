using HP.Core.Commands;
using HP.Domain;
using MediatR;

namespace HP.Application.Commands.Person
{
    public record UpdatePersonCommand(Guid PersonId, string PersonType, int? GroupId = null) : BaseCommand;
    public class UpdatePersonCommandHandler : IRequestHandler<UpdatePersonCommand, CommandResult>
    {
        private readonly IPersonRepository _repository;
        public UpdatePersonCommandHandler(IPersonRepository personRepository)
        {
            _repository = personRepository;
        }
        public async Task<CommandResult> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = _repository.GetByIdAsync(request.PersonId).Result;
            if (person == null)
                throw new ApplicationException($"PersonId : {request.PersonId} is not exist.");

            person.UpdateBasicInfo(person.PersonType, person.GroupId);
            var check = await _repository.UpdatePersonAsync(person);
            if (check != null)
                return new CommandResult(false, "Updated failure. ", person.Id.ToString());
            return new CommandResult(true, "", person.Id.ToString());
        }
    }
}
