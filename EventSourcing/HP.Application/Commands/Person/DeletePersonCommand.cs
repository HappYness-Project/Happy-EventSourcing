using HP.Core.Commands;
using HP.Domain;
using MediatR;

namespace HP.Application.Commands.Person
{
    public record DeletePersonCommand(string PersonId) : BaseCommand
    {
        public static DeletePersonCommand Create(string personId)
        {
            return new DeletePersonCommand(personId);
        }
    }
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, CommandResult>
    {
        private readonly IPersonRepository _repository;
        public DeletePersonCommandHandler(IPersonRepository personRepository)
        {
            _repository = personRepository;
        }
        public async Task<CommandResult> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _repository.GetByIdAsync(request.PersonId);
            if (person == null)
                throw new ApplicationException("Person doesn't exist in the database. ");

            await _repository.DeletePersonAsync(request.PersonId);
            return new CommandResult(true, "Person is removed", person.Id);
        }
    }
}
