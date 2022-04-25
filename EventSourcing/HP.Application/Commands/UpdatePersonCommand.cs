using HP.Domain.Person;
using MediatR;
namespace HP.Application.Commands
{
    public record UpdatePersonCommand(string FirstName, string LastName) : IRequest<Person>;

}
