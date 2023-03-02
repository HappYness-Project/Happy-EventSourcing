using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using HP.Domain;
using MediatR;

namespace HP.Application.Commands.Todo
{
    public record ActivateTodoItemCommand(Guid TodoId, Guid TodoItemId) : BaseCommand;
    public class ActivateTodoItemCommandHandler : BaseCommandHandler, IRequestHandler<ActivateTodoItemCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Todo> _todoRepository;
        public ActivateTodoItemCommandHandler(IEventProducer eventProducer, IAggregateRepository<Domain.Todo> repository) : base(eventProducer)
        {
            _todoRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<CommandResult> Handle(ActivateTodoItemCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByAggregateId<Domain.Todo>(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"There is no Todo ID: {cmd.TodoId}.");

            TodoItem todoItem = todo.SubTodos.Where(x => x.Id == cmd.TodoItemId).FirstOrDefault();
            if (todoItem == null)
                throw new ApplicationException($"there is no todo Item ID :{cmd.TodoItemId}");

            todoItem.IsActive = true;
            await _todoRepository.PersistAsync(todo);
            return new CommandResult(true, $"ParentTodo:{todo.Id}, TodoItem is actiavated", todoItem.Id.ToString());
        }
    }
}
