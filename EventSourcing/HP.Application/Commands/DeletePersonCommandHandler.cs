using HP.Domain.Person;
using MediatR;

namespace HP.Application.Commands
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
    {
        private readonly IPersonRepository _repository;
        public DeletePersonCommandHandler(IPersonRepository personRepository)
        {
            this._repository = personRepository;
        }
        public Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            return _repository.DeletePersonAsync(request.PersonId);
        }
    }
}
