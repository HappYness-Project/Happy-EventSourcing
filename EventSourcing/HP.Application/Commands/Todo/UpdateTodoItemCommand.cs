using HP.Core.Commands;
using HP.Domain;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record UpdateTodoItemCommand(Guid TodoId, Guid TodoItemId, string Title, string Desc, string Type) : BaseCommand;
    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, CommandResult>
    {
        private readonly ITodoAggregateRepository _repository;
        public UpdateTodoItemCommandHandler(ITodoAggregateRepository todoRepository)
        {
            _repository = todoRepository;
        }
        public async Task<CommandResult> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(request.TodoId);
            if (todo == null)
                throw new ApplicationException($"TodoId:{todo.Id} doesn't exist. ");

            var todoItem = todo.SubTodos.Where(x => x.Id == request.TodoItemId).FirstOrDefault();
            if (todoItem == null)
                throw new ApplicationException($"{request.TodoItemId} does not exist in the TodoId: {todo.Id}");

            todo.UpdateTodoItem(request.TodoItemId, request.Title, request.Desc, request.Type);
            await _repository.UpdateAsync(todo);
            var @event = new TodoDomainEvents.TodoItemRemoved(request.TodoItemId);
            return new CommandResult(true, "UpdateTodoItemCommand.", todo.Id.ToString());
        }
    }
}
