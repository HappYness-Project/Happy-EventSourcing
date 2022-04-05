using DemoLib.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLib.Handlers
{
    public interface ICommandHandler<in T> : IRequestHandler<T> where T : ICommand
    {
    }
}
https://codeopinion.com/category/event-sourcing/
https://codeopinion.com/publishing-events-from-crud-or-commands/
https://codeopinion.com/event-based-architecture-what-do-you-mean-by-event/
