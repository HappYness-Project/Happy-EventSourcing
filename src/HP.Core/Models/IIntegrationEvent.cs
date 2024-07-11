using MediatR;

namespace HP.Core.Models
{
    public interface IIntegrationEvent : INotification
    {
        public Guid EventId { get; }
        DateTime OccuredOn { get; }
    }
}
