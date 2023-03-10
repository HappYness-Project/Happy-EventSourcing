using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using MediatR;
namespace HP.Application.Commands.Persons
{
    public record UpdatePersonGroupCommand(Guid PersonId, int GroupId) : BaseCommand;
    public class UpdatePersonGroupCommandHandler : BaseCommandHandler, IRequestHandler<UpdatePersonGroupCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Person> _personRepository;
        public UpdatePersonGroupCommandHandler(IEventProducer eventProducer, IAggregateRepository<Domain.Person> personRepository) : base(eventProducer)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        public async Task<CommandResult> Handle(UpdatePersonGroupCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.GetByAggregateId<Domain.Person>(request.PersonId);
            if (person == null)
                throw new ApplicationException($"PersonId : {request.PersonId} is not exist.");

            person.UpdateGroupId(request.GroupId);
            await _personRepository.PersistAsync(person);
            return new CommandResult(true, "", person.Id.ToString());
        }
    }
}
