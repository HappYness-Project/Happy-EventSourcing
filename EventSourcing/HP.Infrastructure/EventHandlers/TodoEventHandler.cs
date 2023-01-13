using HP.Domain;
using HP.Domain.Todos;
using Microsoft.EntityFrameworkCore.Metadata;
using MongoDB.Driver;
using static HP.Domain.TodoDomainEvents;
namespace HP.Infrastructure.EventHandlers
{
    // This is Query side
    public class TodoEventHandler : ITodoEventHandler
    {
        private readonly ITodoRepository _todoRepository;
        #region Ctors
        public TodoEventHandler(ITodoRepository todoRepository)
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


        private async Task SaveTodoDetailsViewAsync(TodoDetails todoView, CancellationToken cancellationToken)
        {
            var filter = Builders<TodoDetails>.Filter
                           .Eq(a=> a.Id, todoView.Id);

            var update = Builders<TodoDetails>.Update
                .Set(a => a.Id, todoView.Id)
                .Set(a => a.Description, todoView.Description)
                .Set(a => a.TodoType, todoView.TodoType)
                .Set(a => a.TodoStatus, todoView.TodoStatus)
                .Set(a => a.Created, todoView.Created)
                .Set(a => a.SubTodos, todoView.SubTodos);


        }
    }
}
