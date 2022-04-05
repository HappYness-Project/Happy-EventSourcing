using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLib.Events
{
    public interface IEventHandler
    {
        Type CanHandle { get; }
        ValueTask<object?> Handle(object @event, CancellationToken ct); 
    }

    public interface IEventHandler<in TEvent>
    {
        Task Handle(TEvent @event, CancellationToken ct);
    }
}
