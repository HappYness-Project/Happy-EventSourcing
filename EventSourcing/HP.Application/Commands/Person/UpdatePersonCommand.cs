using HP.Core.Commands;
using HP.Core.Events;
using HP.Domain;
using MediatR;

namespace HP.Application.Commands.Person
{
    public record UpdatePersonCommand(Guid PersonId, string? PersonType, string? GoalType, int? GroupId = null) : BaseCommand;
    public class UpdatePersonCommandHandler : BaseCommandHandler, IRequestHandler<UpdatePersonCommand, CommandResult>
    {
        private readonly IPersonRepository _repository;
        public UpdatePersonCommandHandler(IEventProducer eventProducer, IPersonRepository personRepository) : base(eventProducer)
        {
            _repository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        public async Task<CommandResult> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = _repository.GetByIdAsync(request.PersonId).Result;
            if (person == null)
                throw new ApplicationException($"PersonId : {request.PersonId} is not exist.");

            person.UpdateBasicInfo(request.PersonType, request.GoalType, request.GroupId);
            var check = await _repository.UpdatePersonAsync(person);
            if (check != null)
                return new CommandResult(false, "Updated failure. ", person.Id.ToString());

            await ProduceDomainEvents(person.UncommittedEvents);
            return new CommandResult(true, "", person.Id.ToString());
        }
    }
}
