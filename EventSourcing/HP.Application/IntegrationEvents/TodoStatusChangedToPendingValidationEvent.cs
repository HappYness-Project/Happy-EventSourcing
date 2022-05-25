using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HP.Application.IntegrationEvents
{
    public record TodoStatusChangedToPendingValidationEvent : IntegrationEvent
    {
        public TodoStatusChangedToPendingValidationEvent(Guid id, DateTime createDate) : base(id, createDate)
        {
        }

        public string TodoId { get; }

    }
}
