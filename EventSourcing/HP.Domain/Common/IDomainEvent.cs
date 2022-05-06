using MediatR;

namespace HP.Domain.Common
{
    public interface IDomainEvent : INotification
    {
        public string Type { get; set; }
        //public string EventType { get; }

        DateTime OccuredOn { get; }
    }
}
