using HP.Domain;
using MediatR;

namespace HP.Application.Commands
{
    public record DeletePersonCommand(string PersonId) : IRequest<Person>
    {
        public static DeletePersonCommand Create(string personId)
        {
            return new DeletePersonCommand(personId);
        }
    }
}
