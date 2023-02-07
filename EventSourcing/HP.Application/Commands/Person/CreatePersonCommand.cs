using HP.Core.Commands;
using HP.Core.Events;
using HP.Core.Models;
using HP.Domain.People.Write;
using MediatR;
namespace HP.Application.Commands.Person
{
    public record CreatePersonCommand(string PersonName, string PersonType, int? GroupId = null) : BaseCommand;
    public class CreatePersonCommandHandler : BaseCommandHandler,IRequestHandler<CreatePersonCommand, CommandResult>
    {
        private readonly IPersonAggregateRepository _repository;
        private readonly IEventStoreRepository _esRepository;
        public CreatePersonCommandHandler(IPersonAggregateRepository personRepository, IEventStoreRepository eventStoreRepository, IEventProducer eventProducer) : base(eventProducer)
        {
            this._repository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            this._esRepository = eventStoreRepository ?? throw new ArgumentNullException(nameof(eventStoreRepository));
        }
        public async Task<CommandResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            // Todo : Event Rehydrate Async. 
            var person = await _repository.GetPersonByPersonNameAsync(request.PersonName);
            if(person != null)
                throw new ApplicationException($"The PersonName : {request.PersonName} Already exists.");

            var newPerson = Domain.Person.Create(request.PersonName);
            // Event Store Save Events(Aggregate ID, Events, Expected Version)
            var eventStream = await _esRepository.FindByAggregateId(newPerson.Id);// Get Event Stream - Event Store, Find By Aggregate ID
            // Get from Event Store Collection within MongoDB.

            foreach (var e in eventStream) // Iterate All Events and constructor Event Model in order to save it into Event Store Repository. 
            {
                EventModel eventModel = new()
                {

                };
                await _esRepository.SaveAsync(eventModel);
                if (newPerson != null)
                    await ProduceDomainEvents(newPerson.UncommittedEvents);
            }
            // TODO : mark Changes As Uncommitted.
            return new CommandResult(true, "Successfully person has been created.", newPerson.Id.ToString());
        }
    }
}
