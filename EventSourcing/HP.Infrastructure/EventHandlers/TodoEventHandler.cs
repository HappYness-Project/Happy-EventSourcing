using HP.Core.Common;
using HP.Domain;
using HP.Domain.Todos.Read;
using MongoDB.Driver;
using static HP.Domain.TodoDomainEvents;
namespace HP.Infrastructure.EventHandlers
{
    public class TodoEventHandler : ITodoEventHandler
    {
        private readonly IBaseRepository<TodoDetails> _todoDetailsRepository;
        #region Ctors
        public TodoEventHandler(IBaseRepository<TodoDetails> todoRepository)
        {
            this._todoDetailsRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }
        #endregion

        #region handlers
        public async Task On(TodoCreated @event)
        {
            var todoDetails = new TodoDetails(@event.TodoId)
            {

                PersonId = @event.PersonId,
                Title = @event.TodoTitle,
                Description = @event.TodoDesc,
                TodoType = @event.TodoType
            };
            await _todoDetailsRepository.CreateAsync(todoDetails);
        }
        public async Task On(TodoUpdated @event)
        {
            var todoDetails = new TodoDetails(@event.TodoId)
            {
                Title = @event.TodoTitle,
                Description = @event.TodoDesc,
                TodoType = @event.TodoType
            };
            await _todoDetailsRepository.UpdateAsync(todoDetails);
        }
        public async Task On(TodoActivated @event)
        {
            var findTodo = await _todoDetailsRepository.FindOneAsync(x => x.Id == @event.TodoId);
            if(findTodo != null)
            {
                findTodo.IsActive = true;
                await _todoDetailsRepository.UpdateAsync(findTodo);
            }
        }
        public async Task On(TodoDeactivated @event)
        {
            var findTodo = await _todoDetailsRepository.FindOneAsync(x => x.Id == @event.AggregateId);
            if(findTodo != null)
            {
                findTodo.IsActive = false;
                await _todoDetailsRepository.UpdateAsync(findTodo);
            }
        }
        public async Task On(TodoRemoved @event)
        {
            await _todoDetailsRepository.DeleteByIdAsync(@event.TodoId);
        }
        public async Task On(TodoItemCreated @event)
        {
            var findTodo = await _todoDetailsRepository.FindOneAsync(x => x.Id == @event.AggregateId);
            if(findTodo !=null)
            {
                TodoItem todoItem = new TodoItem(@event.TodoTitle, @event.TodoDesc, @event.TodoType);
                findTodo.SubTodos.Add(todoItem);
                await _todoDetailsRepository.UpdateAsync(findTodo);
            }
        }
        public async Task On(TodoItemUpdated @event)
        {
            var findTodo = await _todoDetailsRepository.FindOneAsync(x => x.Id == @event.AggregateId);
            if (findTodo != null)
            {
                TodoItem todoItem = new TodoItem(@event.TodoTitle, @event.TodoDesc, @event.TodoType);
                findTodo.SubTodos.Add(todoItem);
                await _todoDetailsRepository.UpdateAsync(findTodo);
            }
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
