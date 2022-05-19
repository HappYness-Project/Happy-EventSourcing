using HP.Domain;
using MediatR;

namespace HP.Application.Commands
{
    public record UpdatePersonCommand(string UserId, string FirstName, string LastName, string Address, string Email) : IRequest<Person>;
}
