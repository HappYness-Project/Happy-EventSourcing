using HP.Core.Commands;
using HP.Core.Events;
using HP.Domain.People.Write;
using HP.Infrastructure.Kafka;
using MediatR;
namespace HP.Application.Commands.Person
{
    public record UpdatePersonRoleCommand(Guid personId, string Role) : BaseCommand;
    public class UpdatePersonRoleCommandHandler : BaseCommandHandler,
                                              IRequestHandler<UpdatePersonRoleCommand, CommandResult>
    {
        private readonly IPersonAggregateRepository _repository;
        public UpdatePersonRoleCommandHandler(IPersonAggregateRepository personRepository, IEventProducer eventProducer) : base(eventProducer)
        {
            this._repository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        public async Task<CommandResult> Handle(UpdatePersonRoleCommand request, CancellationToken cancellationToken)
        {
            var person = _repository.GetByIdAsync(request.personId).Result;
            if (person == null)
                throw new ApplicationException($"Person ID:{request.personId} doesn't exist.");

            person.UpdateRole(request.Role);
            await _repository.UpdateAsync(person);
            await ProduceDomainEvents(person.UncommittedEvents);
            return new CommandResult(true, "Successfully person has been created.", person.Id.ToString());
        }
    }
}
