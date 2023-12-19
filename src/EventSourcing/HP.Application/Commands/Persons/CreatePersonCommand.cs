using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using HP.Core.Models;
using HP.Domain;
using HP.Domain.People.Read;
using MediatR;

namespace HP.Application.Commands.Persons
{
    public record CreatePersonCommand(string PersonName, string PersonType, int? GroupId = null) : BaseCommand;
    public class CreatePersonCommandHandler : BaseCommandHandler, IRequestHandler<CreatePersonCommand, CommandResult>
    {
        private readonly IAggregateRepository<Person> _peronRepository;

        public CreatePersonCommandHandler(IAggregateRepository<Person> personRepository, IEventProducer eventProducer)
            : base(eventProducer)
        {
            this._peronRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        public async Task<CommandResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            //if(_personDetailsRepository.Exists(x => x.PersonName == request.PersonName))
            //    throw new ApplicationException($"The PersonName : {request.PersonName} Already exists.");

            var newPerson = Person.Create(request.PersonName);
            await _peronRepository.PersistAsync(newPerson);
            return new CommandResult(true, "Successfully person has been created.", newPerson.Id.ToString());
        }
    }
}
