using HP.Core.Commands;
using HP.Core.Events;
using HP.Domain.People.Write;
using MediatR;
namespace HP.Application.Commands.Person
{
    public record UpdatePersonGroupCommand(Guid PersonId, int GroupId) : BaseCommand;
    public class UpdatePersonGroupCommandHandler : BaseCommandHandler, IRequestHandler<UpdatePersonGroupCommand, CommandResult>
    {
        private readonly IPersonAggregateRepository _repository;
        public UpdatePersonGroupCommandHandler(IEventProducer eventProducer, IPersonAggregateRepository personRepository) : base(eventProducer)
        {
            _repository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }
        public async Task<CommandResult> Handle(UpdatePersonGroupCommand request, CancellationToken cancellationToken)
        {
            var person = await _repository.GetByIdAsync(request.PersonId);
            if (person == null)
                throw new ApplicationException($"PersonId : {request.PersonId} is not exist.");

            person.UpdateGroupId(request.GroupId);
            await _repository.UpdateAsync(person);
            await ProduceDomainEvents(person.UncommittedEvents);
            return new CommandResult(true, "", person.Id.ToString());
        }
    }
}
