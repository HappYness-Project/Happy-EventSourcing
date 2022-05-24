using HP.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Infrastructure
{

    static class MediatorExtension
    {
        //public static ContextStaticAttribute async Task DispatchDomainEventsAsync(this IMediator mediator, P)
    }


    //public class DomainEventsDispatcher : IDomainEventsDispatcher
    //{
    //    private readonly IMediator _mediator;
    //    private readonly ILifetimeScope _scope;
    //    private readonly OrdersContext _ordersContext;

    //    public DomainEventsDispatcher(IMediator mediator, ILifetimeScope scope, OrdersContext ordersContext)
    //    {
    //        this._mediator = mediator;
    //        this._scope = scope;
    //        this._ordersContext = ordersContext;
    //    }

    //    public async Task DispatchEventsAsync()
    //    {
    //        var domainEntities = this._ordersContext.ChangeTracker
    //            .Entries<Entity>()
    //            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

    //        var domainEvents = domainEntities
    //            .SelectMany(x => x.Entity.DomainEvents)
    //            .ToList();

    //        var domainEventNotifications = new List<IDomainEventNotification<IDomainEvent>>();
    //        foreach (var domainEvent in domainEvents)
    //        {
    //            Type domainEvenNotificationType = typeof(IDomainEventNotification<>);
    //            var domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());
    //            var domainNotification = _scope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
    //            {
    //                new NamedParameter("domainEvent", domainEvent)
    //            });

    //            if (domainNotification != null)
    //            {
    //                domainEventNotifications.Add(domainNotification as IDomainEventNotification<IDomainEvent>);
    //            }
    //        }

    //        domainEntities
    //            .ForEach(entity => entity.Entity.ClearDomainEvents());

    //        var tasks = domainEvents
    //            .Select(async (domainEvent) =>
    //            {
    //                await _mediator.Publish(domainEvent);
    //            });

    //        await Task.WhenAll(tasks);

    //        foreach (var domainEventNotification in domainEventNotifications)
    //        {
    //            string type = domainEventNotification.GetType().FullName;
    //            var data = JsonConvert.SerializeObject(domainEventNotification);
    //            OutboxMessage outboxMessage = new OutboxMessage(
    //                domainEventNotification.DomainEvent.OccurredOn,
    //                type,
    //                data);
    //            this._ordersContext.OutboxMessages.Add(outboxMessage);
    //        }
    //    }
    //}
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync();
    }
}
