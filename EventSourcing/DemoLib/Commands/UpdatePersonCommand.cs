using HP.Domain.Person;
using MediatR;
namespace DemoLib.Commands
{
    public record UpdatePersonCommand(string FirstName, string LastName) : IRequest<Person>;

}
