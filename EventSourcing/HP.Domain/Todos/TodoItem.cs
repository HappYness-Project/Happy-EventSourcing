using HP.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Domain.Todos
{
    public class TodoItem : Entity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool IsActive { get; private set; }
        public TodoStatus TodoStatus { get; private set; }
        protected override void When(IDomainEvent @event)
        {
            throw new NotImplementedException();
        }
    }
}
