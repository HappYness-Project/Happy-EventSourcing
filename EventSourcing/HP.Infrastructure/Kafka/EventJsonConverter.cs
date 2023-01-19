using HP.Core.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using static HP.Domain.PersonDomainEvents;
using static HP.Domain.TodoDomainEvents;

namespace HP.Infrastructure.Kafka
{
    public class EventJsonConverter : JsonConverter<IDomainEvent>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(IDomainEvent));
        }
        public override IDomainEvent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        { 
            if(!JsonDocument.TryParseValue(ref reader, out var doc))
            {
                throw new JsonException($"Failed to parse {nameof(JsonDocument)}");
            }

            if (!doc.RootElement.TryGetProperty("EventType", out var type))
            {
                throw new JsonException("Could not detect the Type discriminator property!");
            }
            var typeDiscriminator = type.GetString();
            var json = doc.RootElement.GetRawText();
            return typeDiscriminator switch
            {
                nameof(TodoCreated) => JsonSerializer.Deserialize<TodoCreated>(json, options),
                nameof(TodoUpdated) => JsonSerializer.Deserialize<TodoUpdated>(json, options),
                nameof(TodoRemoved) => JsonSerializer.Deserialize<TodoRemoved>(json, options),
                nameof(TodoActivated) => JsonSerializer.Deserialize<TodoActivated>(json, options),
                nameof(TodoDeactivated) => JsonSerializer.Deserialize<TodoDeactivated>(json, options),
                nameof(TodoStarted) => JsonSerializer.Deserialize<TodoStarted>(json, options),
                nameof(TodoCompleted) => JsonSerializer.Deserialize<TodoCompleted>(json, options),
                nameof(TodoItemCreated) => JsonSerializer.Deserialize<TodoItemCreated>(json, options),
                nameof(TodoItemUpdated) => JsonSerializer.Deserialize<TodoItemUpdated>(json, options),
                nameof(TodoItemRemoved) => JsonSerializer.Deserialize<TodoItemRemoved>(json, options),
                nameof(TodoStatusToAccepted) => JsonSerializer.Deserialize<TodoStatusToAccepted>(json, options),
                nameof(TodoStatusToPending) => JsonSerializer.Deserialize<TodoStatusToPending>(json, options),

                nameof(PersonCreated) => JsonSerializer.Deserialize<PersonCreated>(json, options),
                nameof(PersonInfoUpdated) => JsonSerializer.Deserialize<PersonInfoUpdated>(json, options),
                nameof(PersonGroupUpdated) => JsonSerializer.Deserialize<PersonGroupUpdated>(json, options),

                _ => throw new JsonException($"{typeDiscriminator} is not supported yet.")
            };
        }
        public override void Write(Utf8JsonWriter writer, IDomainEvent value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
