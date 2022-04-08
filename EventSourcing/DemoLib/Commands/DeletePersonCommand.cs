using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLib.Commands
{
    public record DeletePersonCommand(string PersonId) : ICommand
    {
        public static DeletePersonCommand Create(string personId)
        {
            if()
            return new DeletePersonCommand(personId);
        }
    }
}
