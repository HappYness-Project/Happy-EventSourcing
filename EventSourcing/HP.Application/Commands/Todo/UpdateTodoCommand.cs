using HP.Core.Commands;
using HP.Core.Common;
using HP.Domain.Todos.Write;
using MediatR;

namespace HP.Application.Commands.Todo
{
    public record UpdateTodoCommand(Guid TodoId, string Title, string type, string Description, string[] Tags, DateTime? TargetStartDate = null, DateTime? TargetEndDate = null) : BaseCommand;
    public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, CommandResult>
    {
        private readonly IAggregateRepository<Domain.Todo> _todoRepository;
        public UpdateTodoCommandHandler(IAggregateRepository<Domain.Todo> todoRepository)
        {
            _todoRepository = todoRepository ?? throw new ArgumentNullException(nameof(todoRepository));
        }
        public async Task<CommandResult> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByAggregateId<Domain.Todo>(request.TodoId);
            if (todo == null)
                throw new ApplicationException($"TodoId:{request.TodoId}, does not exist.");

            todo.Update(request.Title, request.type, request.Description, request.Tags, request.TargetStartDate, request.TargetEndDate);
            await _todoRepository.PersistAsync(todo);
            return new CommandResult(true, "Todo information has been changed", todo.Id.ToString());
        }
    }
}
