using FluentAssertions;
using HP.Core.Models;
using HP.Infrastructure.Kafka;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HP.UnitTest
{
    public class JsonConverterTest
    {
        [Test]
        public void JsonTesting()
        {
            string jsonMsg = @"{ 'TodoId':'8ce96ee6-923b-40c0-a125-860f86d913dc','PersonId':'b333e7ae-10eb-4bf7-bd8c-60b61a91ba8f','TodoTitle':'asdfdsfasd','TodoDesc':'string','TodoType':'Research','EventId':'9ab752b3-1736-463c-96ca-d05f2a52b476','AggregateId':'8ce96ee6-923b-40c0-a125-860f86d913dc','OccuredOn':'2023-01-19T22:07:07.5039668-05:00','EventType':'TodoCreated','AggregateVersion':0}";
            var options = new JsonSerializerOptions { Converters = { new EventJsonConverter() } };
            var @event = JsonSerializer.Deserialize<DomainEvent>(jsonMsg, options);
            @event.Should().NotBeNull();
        }
    }
}
