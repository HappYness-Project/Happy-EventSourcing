using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using HP.Core.Models;
using HP.Domain.People.Read;
using MediatR;

namespace HP.Application.Commands.Persons
{
    public record CreatePersonCommand(string PersonName, string PersonType, int? GroupId = null) : BaseCommand;
    public class CreatePersonCommandHandler : BaseCommandHandler, IRequestHandler<CreatePersonCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Person> _peronRepository;
        private readonly IBaseRepository<PersonDetails> _personDetailsRepository;

        public CreatePersonCommandHandler(IAggregateRepository<Domain.Person> personRepository,
                                          IBaseRepository<PersonDetails> personDetailsRepository,
                                          IEventProducer eventProducer)
            : base(eventProducer)
        {
            this._peronRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            this._personDetailsRepository = _personDetailsRepository ?? throw new ArgumentNullException(nameof(personDetailsRepository));
        }
        public async Task<CommandResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
        {
            bool isExist =  _personDetailsRepository.Exists(x => x.PersonName == request.PersonName);

            var person = await _peronRepository.GetByAggregateId<Domain.Person>(Guid.NewGuid());
            //if(person != null)
            //    throw new ApplicationException($"The PersonName : {request.PersonName} Already exists.");
            var newPerson = Domain.Person.Create(request.PersonName);
            await _peronRepository.PersistAsync(newPerson);

            return new CommandResult(true, "Successfully person has been created.", newPerson.Id.ToString());
        }
    }
}
