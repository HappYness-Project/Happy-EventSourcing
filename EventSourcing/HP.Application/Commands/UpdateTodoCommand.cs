using HP.Domain;
using MediatR;

namespace HP.Application.Commands
{
    public record UpdateTodoCommand(string TodoId, string Title, string type, string Description, string[] Tags) : CommandBase<bool>;
    public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, bool>
    {
        private readonly ITodoRepository _repository;
        public UpdateTodoCommandHandler(ITodoRepository todoRepository)
        {
            this._repository = todoRepository;
        }
        public async Task<bool> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(request.TodoId);
            if(todo == null)
                throw new ApplicationException($"TodoId:{request.TodoId}, does not exist.");

            todo.Update(request.Title, request.Description, request.Tags);
            await _repository.UpdateAsync(todo);
            return true;
        }
    }
}
