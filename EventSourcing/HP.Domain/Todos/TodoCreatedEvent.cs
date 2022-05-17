﻿using HP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Domain.Todos
{
    public class TodoCreatedEvent : DomainEventBase
    {
        public TodoCreatedEvent(string todoId, string userId)
        {
        }
        public string Id { get; }
        public string UserId { get; }
    }
}
