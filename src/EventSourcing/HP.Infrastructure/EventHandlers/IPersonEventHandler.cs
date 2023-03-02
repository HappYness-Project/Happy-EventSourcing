using MediatR;
using static HP.Domain.PersonDomainEvents;
namespace HP.Infrastructure.EventHandlers
{
    public interface IPersonEventHandler : INotification
    {
        Task On(PersonCreated @event);
        Task On(PersonInfoUpdated @event);
        Task On(PersonGroupUpdated @event);
        Task On(PersonRoleUpdated @event);
    }
}
 