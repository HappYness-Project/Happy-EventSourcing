using HP.Core.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace HP.Application
{
    public interface IDomainEventNotification : INotification
    {
        Guid Id { get; }
    }
    public interface IDomainEventNotification<out TEventType> : IDomainEventNotification
    {
        TEventType DomainEvent { get; }
    }
    public class DomainNotificationBase<T> : IDomainEventNotification<T> where T : IDomainEvent
    {
        [JsonIgnore]
        public T DomainEvent { get; }
        public Guid Id { get; }
        public DomainNotificationBase(T domainEvent)
        {
            Id = Guid.NewGuid();
            DomainEvent = domainEvent;
        }
    }
}
