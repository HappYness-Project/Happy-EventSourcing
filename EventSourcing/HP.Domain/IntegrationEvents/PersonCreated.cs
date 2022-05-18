using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Domain.IntegrationEvents
{
    public interface IIntegrationEvent
    {
        Guid Id { get; }
    }
    public record PersonCreated : IIntegrationEvent, INotification
    {
        public PersonCreated(string id, string accountId)
        {
        }
        public Guid AccountId { get; init; }
        public Guid Id { get; }
    }
}
