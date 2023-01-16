using HP.Core.Commands;
using HP.Core.Events;
using HP.Domain.People.Write;
using MediatR;
namespace HP.Application.Commands.Person
{
    public record CreatePersonCommand(string PersonName, string PersonType, int? GroupId = null) : BaseCommand;
    public class CreatePersonCommandHandler : BaseCommandHandler,
                                              IRequestHandler<CreatePersonCommand, CommandResult>
    {
        private readonly IPersonAggregateRepository _repository;
        public CreatePersonCommandHandler(IPersonAggregateRepository personRepository, IEventProducer eventProducer) : base(eventProducer)
        {
            this._repository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        public async Task<CommandResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _repository.GetPersonByPersonNameAsync(request.PersonName);
            if(person != null)
                throw new ApplicationException($"The PersonName : {request.PersonName} Already exists.");

            var newPerson = await _repository.CreateAsync(Domain.Person.Create(request.PersonName));
            if (newPerson != null)
                await ProduceDomainEvents(newPerson.UncommittedEvents);

            return new CommandResult(true, "Successfully person has been created.", newPerson.Id.ToString());
        }
    }
}
