using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using MediatR;

namespace HP.Application.Commands.Persons
{
    public record DeletePersonCommand(Guid PersonId) : BaseCommand
    {
        public static DeletePersonCommand Create(Guid personId)
        {
            return new DeletePersonCommand(personId);
        }
    }
    public class DeletePersonCommandHandler : BaseCommandHandler, IRequestHandler<DeletePersonCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Person> _personRepository;
        public DeletePersonCommandHandler(IEventProducer eventProducer, IAggregateRepository<Domain.Person> personRepository) : base(eventProducer)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        public async Task<CommandResult> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _personRepository.RehydrateAsync<Domain.Person>(request.PersonId);
            if (person == null)
                throw new ApplicationException("Person doesn't exist in the database. ");

            person.Remove();
            await _personRepository.PersistAsync(person);
            return new CommandResult(true, "Person is removed", person.Id.ToString());
        }
    }
}
