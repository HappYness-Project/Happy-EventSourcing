using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using MediatR;

namespace HP.Application.Commands.Persons
{
    public record CreatePersonCommand(string PersonName, string PersonType, int? GroupId = null) : BaseCommand;
    public class CreatePersonCommandHandler : BaseCommandHandler,IRequestHandler<CreatePersonCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Person> _repository;
        public CreatePersonCommandHandler(IAggregateRepository<Domain.Person> personRepository, IEventProducer eventProducer) : base(eventProducer)
        {
            this._repository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        public async Task<CommandResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            //var person = await _repository.RehydrateAsync(request.PersonName);
            //if(person != null)
            //    throw new ApplicationException($"The PersonName : {request.PersonName} Already exists.");
            var newPerson = Domain.Person.Create(request.PersonName);
            await _repository.PersistAsync(newPerson);

            return new CommandResult(true, "Successfully person has been created.", newPerson.Id.ToString());
        }
    }
}
