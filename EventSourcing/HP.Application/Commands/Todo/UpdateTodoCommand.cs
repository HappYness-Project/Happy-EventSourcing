using HP.Core.Commands;
using HP.Domain;
using MediatR;

namespace HP.Application.Commands.Todo
{
    public record UpdateTodoCommand(Guid TodoId, string Title, string type, string Description, string[] Tags, DateTime? TargetStartDate = null, DateTime? TargetEndDate = null) : BaseCommand;
    public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, CommandResult>
    {
        private readonly ITodoRepository _repository;
        public UpdateTodoCommandHandler(ITodoRepository todoRepository)
        {
            _repository = todoRepository;
        }
        public async Task<CommandResult> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(request.TodoId);
            if (todo == null)
                throw new ApplicationException($"TodoId:{request.TodoId}, does not exist.");

            todo.Update(request.Title, request.type, request.Description, request.Tags, request.TargetStartDate, request.TargetEndDate);
            await _repository.UpdateAsync(todo);
            return new CommandResult(true, "Todo information has been changed", todo.Id.ToString());
        }
    }
}
