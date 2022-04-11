using MediatR;

namespace HP.Domain
{
    public interface IDomainEvent : INotification
    {
        DateTime OccuredOn { get; }
    }
}
