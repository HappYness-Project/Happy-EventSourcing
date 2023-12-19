using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using MediatR;
namespace HP.Application.Commands.Persons
{
    public record UpdatePersonRoleCommand(Guid PersonId, string Role) : BaseCommand;
    public class UpdatePersonRoleCommandHandler : BaseCommandHandler, IRequestHandler<UpdatePersonRoleCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Person> _personRepository;
        public UpdatePersonRoleCommandHandler(IEventProducer eventProducer, IAggregateRepository<Domain.Person> personRepository) : base(eventProducer)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        public async Task<CommandResult> Handle(UpdatePersonRoleCommand cmd, CancellationToken cancellationToken)
        {
            var person = await _personRepository.RehydrateAsync<Domain.Person>(cmd.PersonId);
            if (person == null)
                throw new ApplicationException($"Person ID:{cmd.PersonId} doesn't exist.");

            person.UpdateRole(cmd.Role);
            await _personRepository.PersistAsync(person);
            return new CommandResult(true, "Successfully person has been created.", person.Id.ToString());
        }
    }
}
