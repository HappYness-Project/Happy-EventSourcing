using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLib.Events
{
    public interface IEventHandler<in TEvent>
    {
        Task Handle(TEvent @event, CancellationToken ct);
    }
}
