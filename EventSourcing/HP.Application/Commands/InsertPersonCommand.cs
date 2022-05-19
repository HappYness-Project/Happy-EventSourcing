using HP.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Application.Commands
{
    // FirstName and LastName are not a parameter, they are property. so it should be written as Capital.
    public record InsertPersonCommand(string UserId, string FirstName, string LastName, string Address) : IRequest<Person>;


}
