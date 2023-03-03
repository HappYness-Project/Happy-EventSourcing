using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using MediatR;
namespace HP.Application.Commands.Todos
{
    public record UpdateTodoItemCommand(Guid TodoId, Guid TodoItemId, string Title, string Desc, string Type) : BaseCommand;
    public class UpdateTodoItemCommandHandler : BaseCommandHandler, IRequestHandler<UpdateTodoItemCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Todo> _todoRepository;
        public UpdateTodoItemCommandHandler(IEventProducer eventProducer, IAggregateRepository<Domain.Todo> repository) : base(eventProducer)
        {
            _todoRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<CommandResult> Handle(UpdateTodoItemCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByAggregateId<Domain.Todo>(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"TodoId:{todo.Id} doesn't exist. ");

            var todoItem = todo.SubTodos.Where(x => x.Id == cmd.TodoItemId).FirstOrDefault();
            if (todoItem == null)
                throw new ApplicationException($"{cmd.TodoItemId} does not exist in the TodoId: {todo.Id}");

            todo.UpdateTodoItem(cmd.TodoItemId, cmd.Title, cmd.Desc, cmd.Type);
            await _todoRepository.PersistAsync(todo);
            return new CommandResult(true, "UpdateTodoItemCommand.", todo.Id.ToString());
        }
    }
}
