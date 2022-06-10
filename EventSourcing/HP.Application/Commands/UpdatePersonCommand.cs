using HP.Domain.Person;
using MediatR;

namespace HP.Application.Commands
{
    public record UpdatePersonCommand(string UserId, string FirstName, string LastName, Address Address, string Email) : IRequest<Person>;
}
