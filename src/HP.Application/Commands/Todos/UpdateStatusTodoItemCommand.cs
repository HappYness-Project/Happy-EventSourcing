using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using MediatR;
namespace HP.Application.Commands.Todo
{
    public record UpdateStatusTodoItemCommand(Guid TodoId, Guid TodoItemId, string Status) : BaseCommand;
    public class UpdateStatusTodoItemCommandHandler : BaseCommandHandler, IRequestHandler<UpdateStatusTodoItemCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Todo> _todoRepository;
        public UpdateStatusTodoItemCommandHandler(IEventProducer eventProducer, IAggregateRepository<Domain.Todo> repository) : base(eventProducer)
        {
            _todoRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<CommandResult> Handle(UpdateStatusTodoItemCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.RehydrateAsync<Domain.Todo>(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"TodoId:{todo.Id} doesn't exist. ");

            var todoItem = todo.SubTodos.Where(x => x.Id == cmd.TodoItemId).FirstOrDefault();
            if (todoItem == null)
                throw new ApplicationException($"{cmd.TodoItemId} does not exist in the TodoId: {todo.Id}");

            todoItem.SetStatus(cmd.Status);
            await _todoRepository.PersistAsync(todo);
            return new CommandResult(true, "Success", todo.Id.ToString());
        }
    }
}
