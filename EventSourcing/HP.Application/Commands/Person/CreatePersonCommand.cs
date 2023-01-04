using AutoMapper;
using HP.Core.Commands;
using HP.Core.Events;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Person
{
    public record CreatePersonCommand(Guid PersonId, string PersonType, int? GroupId = null) : BaseCommand;
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, CommandResult>
    {
        private readonly IPersonRepository _repository;
        private readonly IEventStore _eventStore;
        public CreatePersonCommandHandler(IPersonRepository personRepository, IEventStore eventStore)
        {
            this._repository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            this._eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
        }
        public async Task<CommandResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _repository.GetByIdAsync(request.PersonId);
            if(person != null)
                throw new ApplicationException($"The PersonId : {request.PersonId} Already exists.");

            var aggregate = await _repository.CreateAsync(Domain.Person.Create(request.PersonId.ToString()));
            if (aggregate != null)
                await _eventStore.SaveEventsAsync(aggregate.Id, aggregate.UncommittedEvents, aggregate.Version);

            return new CommandResult(true, "Successfully person has been created.", aggregate.Id.ToString());
        }
    }
}
