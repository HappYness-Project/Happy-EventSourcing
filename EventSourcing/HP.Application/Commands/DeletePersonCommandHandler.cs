using HP.Domain;
using MediatR;

namespace HP.Application.Commands
{
    public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, Person>
    {
        public Task<Person> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
