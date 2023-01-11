using HP.Core.Commands;
using HP.Core.Events;
using HP.Domain;
using MediatR;

namespace HP.Application.Commands.Todo
{
    public record CreateTodoItemCommand(Guid TodoId, string TodoTitle, string TodoType, string? Description, string[] Tag = null) : BaseCommand;
    public class CreateTodoItemCommandHandler : BaseCommandHandler, IRequestHandler<CreateTodoItemCommand, CommandResult>
    {
        private readonly ITodoAggregateRepository _todoRepository;
        public CreateTodoItemCommandHandler(IEventProducer eventProducer, ITodoAggregateRepository repository) : base(eventProducer)
        {
            _todoRepository = repository ?? throw new ArgumentNullException(nameof(repository));
        }
        public async Task<CommandResult> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(request.TodoId);
            if (todo == null)
                throw new ApplicationException($"There is no Todo Id {request.TodoId}");

            var subTodo = todo.AddTodoItem(request.TodoTitle, request.TodoType, request.Description);
            await _todoRepository.UpdateAsync(todo);

            await ProduceDomainEvents(todo.UncommittedEvents);
            return new CommandResult(true, $"TodoItem has been created within TodoId: {todo.Id}, SubTodoId: {subTodo.Id}", subTodo.Id.ToString());
        }
    }

}
