using HP.Domain.Common;
using HP.Domain.Todos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Application.Events
{
    public class TodoUpdatedEvent : DomainEventBase
    {
        public TodoUpdatedEvent(string userId, string id, string todoTitle, string description, string type) : base(entityId: id, entityType: nameof(Todo))
        {
            UserId = userId;
            TodoId = id;
            TodoTitle = todoTitle;
            Description = description;
            TodoType = type;
        }
        public string UserId { get; set; }
        public string TodoId { get; set; }
        public string TodoTitle { get; set; }
        public string Description { get; set; }
        public string TodoType { get; set; }
    }
}
