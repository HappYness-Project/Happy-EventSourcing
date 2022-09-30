using HP.Domain;
using MediatR;

namespace HP.Application.Commands
{
    public record DeletePersonCommand(string PersonId) : CommandBase<bool>
    {
        public static DeletePersonCommand Create(string personId)
        {
            return new DeletePersonCommand(personId);
        }
    }
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
    {
        private readonly IPersonRepository _repository;
        public DeletePersonCommandHandler(IPersonRepository personRepository)
        {
            this._repository = personRepository;
        }
        public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            var person = _repository.GetByIdAsync(request.Id);
            if(person == null)
                throw new ApplicationException("Person doesn't exist in the database. ");

            return await _repository.DeletePersonAsync(request.PersonId);
        }
    }
}
