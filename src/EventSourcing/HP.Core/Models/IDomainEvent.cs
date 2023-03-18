﻿using MediatR;
using MongoDB.Bson.Serialization.Attributes;

namespace HP.Core.Models
{
    public interface IDomainEvent : INotification
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public Guid EventId { get; }
        public Guid AggregateId { get; set; }
        public int AggregateVersion { get; set; }
        public string EventType { get; }
        DateTime OccuredOn { get; }
    }
}