using HP.Core.Commands;
using HP.Core.Events;
using HP.Core.Models;
using HP.Domain.People.Write;
using MediatR;
using System.Collections.Generic;

namespace HP.Application.Commands.Person
{
    public record CreatePersonCommand(string PersonName, string PersonType, int? GroupId = null) : BaseCommand;
    public class CreatePersonCommandHandler : BaseCommandHandler,IRequestHandler<CreatePersonCommand, CommandResult>
    {
        private readonly IPersonAggregateRepository _repository;
        private readonly IEventStore _eventStore;
        public CreatePersonCommandHandler(IPersonAggregateRepository personRepository, IEventProducer eventProducer) : base(eventProducer)
        {
            this._repository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        public async Task<CommandResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            // Todo : Event Rehydrate Async. 
            var person = await _repository.GetPersonByPersonNameAsync(request.PersonName);
            if(person != null)
                throw new ApplicationException($"The PersonName : {request.PersonName} Already exists.");

            var newPerson = Domain.Person.Create(request.PersonName);
            await _eventStore.SaveEventsAsync(newPerson.Id, newPerson.UncommittedEvents as IReadOnlyCollection<DomainEvent>, 0); // Needs to be updatd.
            return new CommandResult(true, "Successfully person has been created.", newPerson.Id.ToString());
        }
    }
}
