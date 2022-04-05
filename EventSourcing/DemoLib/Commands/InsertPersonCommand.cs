using DemoLib.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLib.Commands
{
    // FirstName and LastName are not a parameter, they are property. so it should be written as Capital.
    public record InsertPersonCommand(string FirstName, string LastName) : IRequest<Person>;
}
