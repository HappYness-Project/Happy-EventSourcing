using HP.Core.Common;
using HP.Domain;
using HP.Domain.Todos;
using HP.Infrastructure.DbAccess;
using static HP.Domain.TodoDomainEvents;
namespace HP.Infrastructure.EventHandlers
{
    // This is Query side
    public class TodoEventHandler : ITodoEventHandler
    {
        private readonly ITodoAggregateRepository _todoRepository;
        #region Ctors
        public TodoEventHandler(ITodoAggregateRepository todoRepository)
        {
            this._todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }
        #endregion

        #region handlers
        public async Task On(TodoCreated @event)
        {
            TodoDetails todoDetails = new TodoDetails(@event.TodoId);
            todoDetails.PersonId = todoDetails.PersonId;
            todoDetails.ProjectId = todoDetails.ProjectId;
            //await _todoRepository.CreateAsync(todo);
        }
        public Task On(TodoUpdated @event)
        {
            throw new NotImplementedException();
        }
        public Task On(TodoActivated @event)
        {
            throw new NotImplementedException();
        }
        public Task On(TodoDeactivated @event)
        {
            throw new NotImplementedException();
        }
        public Task On(TodoRemoved @event)
        {
            throw new NotImplementedException();
        }
        public Task On(TodoItemCreated @event)
        {
            throw new NotImplementedException();
        }
        public Task On(TodoItemUpdated @event)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
