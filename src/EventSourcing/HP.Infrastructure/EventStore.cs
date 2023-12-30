using Confluent.Kafka;
using EventStore.Client;
using Grpc.Core;
using HP.Core.Common;
using HP.Core.Events;
using HP.Core.Exceptions;
using HP.Core.Models;
using MongoDB.Driver;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;


namespace HP.Infrastructure
{
    public class EventStore : IEventStore
    {
        private readonly IEventProducer _eventProducer;
        private readonly IEventSerializer _eventSerializer;
        private readonly EventStoreClient _esClient;
        public EventStore(EventStoreClient client, IEventProducer eventProducer)
        {
            _esClient = client;
            _eventProducer = eventProducer;
        }
        public async Task<List<string>> GetAllAggregateIdsAsync()
        {
            var eventStream = await _esClient.ReadAllAsync(Direction.Forwards, Position.Start).ToListAsync().ConfigureAwait(false);
            if (eventStream == null || !eventStream.Any())
                throw new ArgumentNullException(nameof(eventStream), "Could not retrieve event stream from the event store!.");

            return eventStream.Select(x => x.OriginalStreamId).Distinct().ToList();

        }
        public async Task<IEnumerable<IDomainEvent>> GetEventsAsync(string streamId)
        {
            var result = _esClient.ReadStreamAsync(Direction.Forwards, streamId, StreamPosition.Start);
            List<ResolvedEvent> events = await result.ToListAsync().ConfigureAwait(false);
            if(events ==null|| !events.Any())
                throw new AggregateNotFoundException("Incorrect stream ID provided.");

            var deserializedEvents = events.Select(Map);
            return deserializedEvents;
        }

        public async Task SaveEventsAsync(string streamId, IReadOnlyCollection<IDomainEvent> events,int expectedVersion)
        {
            var firstEvent = events.First();
            var newEvents = events.Select(Map).ToArray();
            await _esClient.AppendToStreamAsync(streamId, StreamState.Any, newEvents).ConfigureAwait(false);
        }

        private IDomainEvent Map(ResolvedEvent resolvedEvent)
        {
            var meta = JsonSerializer.Deserialize<EventMeta>(resolvedEvent.Event.Metadata.ToArray());
            return _eventSerializer.Deserialize(meta.EventType, resolvedEvent.Event.Data.ToArray());
        }
        private static EventData Map(IDomainEvent @event)
        {

            var json = JsonSerializer.Serialize((dynamic)@event);
            var data = Encoding.UTF8.GetBytes(json);

            var typeOfEvent = @event.GetType();
            var meta = new EventMeta()
            {
                EventType = typeOfEvent.AssemblyQualifiedName
            };
            var metaJson = JsonSerializer.Serialize(meta);
            var metadata = Encoding.UTF8.GetBytes(metaJson);

            var eventPayload = new EventData(Uuid.NewUuid(), typeOfEvent.Name, data, metadata);
            return eventPayload;
        }
        internal struct EventMeta
        {
            public string EventType { get; set; }
        }
    }
}
