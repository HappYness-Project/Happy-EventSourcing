﻿namespace HP.Core.Models
{
    public class DomainEvent : IDomainEvent
    {
        public DomainEvent(Guid aggregateId) 
        {
            AggregateId = aggregateId;
            EventId = Guid.NewGuid();
            OccuredOn = DateTime.Now;
            EventType = this.GetType().Name;
        }
        public Guid EventId { get; }
        public Guid AggregateId { get; set; }
        public DateTime OccuredOn { get; }
        public string EventType { get; }
        public int AggregateVersion { get; set; }
    }
}

