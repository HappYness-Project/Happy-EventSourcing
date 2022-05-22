using HP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Domain.Todos
{
    public class TodoCreatedEvent : DomainEventBase
    {
        public TodoCreatedEvent(string todoId, string userId, string todoTitle, string type)
        {
            this.TodoId = todoId;
            this.UserId = userId;
            this.TodoTitle = todoTitle;
            this.Type = type;
        }
        public string TodoId { get; }
        public string UserId { get; }
        public string TodoTitle { get; }
        public string Type { get; }
    }
}
