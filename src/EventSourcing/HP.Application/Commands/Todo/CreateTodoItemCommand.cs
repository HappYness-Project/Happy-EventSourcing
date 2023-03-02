using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using MediatR;

namespace HP.Application.Commands.Todo
{
    public record CreateTodoItemCommand(Guid TodoId, string TodoTitle, string TodoType, string? Description, string[] Tag = null, DateTime? TargetStartDate = null, DateTime? TargetEndDate = null) : BaseCommand;
    public class CreateTodoItemCommandHandler : BaseCommandHandler, IRequestHandler<CreateTodoItemCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Todo> _todoRepository;
        public CreateTodoItemCommandHandler(IEventProducer eventProducer, IAggregateRepository<Domain.Todo> repository) : base(eventProducer)
        {
            _todoRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<CommandResult> Handle(CreateTodoItemCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByAggregateId<Domain.Todo>(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"There is no Todo Id {cmd.TodoId}");

            var subTodo = todo.AddTodoItem(cmd.TodoTitle, cmd.TodoType, cmd.Description, cmd.TargetStartDate, cmd.TargetEndDate);
            await _todoRepository.PersistAsync(todo);
            return new CommandResult(true, $"TodoItem has been created within TodoId: {todo.Id}, SubTodoId: {subTodo.Id}", subTodo.Id.ToString());
        }
    }

}
