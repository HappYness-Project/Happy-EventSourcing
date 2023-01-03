using HP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, IDomainEvent value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
