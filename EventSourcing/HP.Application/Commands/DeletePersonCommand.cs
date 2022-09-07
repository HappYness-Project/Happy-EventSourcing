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
}
