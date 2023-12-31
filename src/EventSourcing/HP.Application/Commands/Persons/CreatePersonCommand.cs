using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using HP.Core.Exceptions;
using HP.Domain;
using HP.Domain.People;
using MediatR;

namespace HP.Application.Commands.Persons;
public record CreatePersonCommand(string DisplayName, string Email, string PersonType, int? GroupId = null) : BaseCommand;
public class CreatePersonCommandHandler : BaseCommandHandler, IRequestHandler<CreatePersonCommand, CommandResult>
{
    private readonly IAggregateRepository<Person> _peronRepository;
    private readonly IUserUniqueChecker _uniqueChecker;
    public CreatePersonCommandHandler(IAggregateRepository<Person> personRepository, IEventProducer eventProducer, IUserUniqueChecker uniqueChecker) : base(eventProducer)
    {
        _peronRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        _uniqueChecker = uniqueChecker ?? throw new ArgumentNullException(nameof(uniqueChecker));
    }
    public async Task<CommandResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        if (await _uniqueChecker.IsEmailUnique(request.Email))
            throw new BusinessRuleException($"The email:{request.Email}  is already in use.");

        var newPerson = Person.Create(request.Email, request.DisplayName);
        await _peronRepository.PersistAsync(newPerson);
        return new CommandResult(true, "Successfully person has been created.", newPerson.Id.ToString());
    }
}
