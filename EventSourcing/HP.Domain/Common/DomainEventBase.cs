﻿using HP.Domain;
namespace HP.Domain.Common
{
    public class DomainEventBase : IDomainEvent
    {
        public DomainEventBase()
        {
            OccuredOn = DateTime.Now;
        }
        public DateTime OccuredOn { get; }
        public string EntityId { get; }
        public string Type { get; set; }
        public string EntityType { get; }
    }
}

