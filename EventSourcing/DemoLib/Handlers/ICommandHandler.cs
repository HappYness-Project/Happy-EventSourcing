using DemoLib.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLib.Handlers
{
    public interface ICommandHandler<in T> : IRequestHandler<T>
        where T : ICommand
    {
    }
}
