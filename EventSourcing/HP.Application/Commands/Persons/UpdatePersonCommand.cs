using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using MediatR;

namespace HP.Application.Commands.Persons
{
    public record UpdatePersonCommand(Guid PersonId, string? PersonType, string? GoalType, int? GroupId = null) : BaseCommand;
    public class UpdatePersonCommandHandler : BaseCommandHandler, IRequestHandler<UpdatePersonCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Person> _personRepository;
        public UpdatePersonCommandHandler(IEventProducer eventProducer, IAggregateRepository<Domain.Person> personRepository) : base(eventProducer)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        public async Task<CommandResult> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByAggregateId<Domain.Person>(request.PersonId);
            if (person == null)
                throw new ApplicationException($"PersonId : {request.PersonId} is not exist.");

            person.UpdateBasicInfo(request.PersonType, request.GoalType, request.GroupId);
            await _personRepository.PersistAsync(person);
            return new CommandResult(true, "", person.Id.ToString());
        }
    }
}
