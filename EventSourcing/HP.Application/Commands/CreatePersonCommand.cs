using HP.Domain;
using HP.Domain.Person;
using MediatR;

namespace HP.Application.Commands
{
    // FirstName and LastName are not a parameter, they are property. so it should be written as Capital.
    public record CreatePersonCommand(string FirstName, string LastName, Address Address, string emailAddr, string UserName = null) : IRequest<Person>;


}
