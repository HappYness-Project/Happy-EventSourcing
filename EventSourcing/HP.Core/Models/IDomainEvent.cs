using MediatR;
using MongoDB.Bson.Serialization.Attributes;

namespace HP.Core.Models
{
    public interface IDomainEvent : INotification
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public Guid EventId { get; }
        public Guid AggregateId { get; }
        public int AggregateVersion { get; }
        public string EventType { get; }
        public EventData EventData { get; }
        DateTime OccuredOn { get; }
    }
}
