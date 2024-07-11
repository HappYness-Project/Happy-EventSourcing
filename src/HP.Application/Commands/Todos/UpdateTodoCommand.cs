using HP.Core.Commands;
using HP.Core.Common;
using HP.Core.Events;
using MediatR;
namespace HP.Application.Commands.Todos
{
    public record UpdateTodoCommand(Guid TodoId, string Title, string type, string Description, string[] Tags, DateTime? TargetStartDate = null, DateTime? TargetEndDate = null) : BaseCommand;
    public class UpdateTodoCommandHandler : BaseCommandHandler, IRequestHandler<UpdateTodoCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Todo> _todoRepository;
        public UpdateTodoCommandHandler(IEventProducer eventProducer, IAggregateRepository<Domain.Todo> todoRepository) : base(eventProducer)
        {
            _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }
        public async Task<CommandResult> Handle(UpdateTodoCommand cmd, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.RehydrateAsync<Domain.Todo>(cmd.TodoId);
            if (todo == null)
                throw new ApplicationException($"TodoId:{cmd.TodoId}, does not exist.");

            todo.Update(cmd.Title, cmd.type, cmd.Description, cmd.Tags, cmd.TargetStartDate, cmd.TargetEndDate);
            await _todoRepository.PersistAsync(todo);
            return new CommandResult(true, "Todo information has been changed", todo.Id.ToString());
        }
    }
}
