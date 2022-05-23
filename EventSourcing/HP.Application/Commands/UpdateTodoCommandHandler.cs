using HP.Domain.Todos;
using MediatR;

namespace HP.Application.Commands
{
    public class UpdateTodoCommandHandler : IRequestHandler<UpdateTodoCommand, bool>
    {
        private readonly ITodoRepository _repository;
        public UpdateTodoCommandHandler(ITodoRepository todoRepository)
        {
            this._repository = todoRepository;
        }
        public async Task<bool> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            
            //Todo todo = new Todo
            //{
            //    Title = request.Title,
            //    Description = request.Description,
            //    Tag = request.Tags,
            //};
            //await _repository.UpdateAsync(todo);
            return true;
        }
    }
}
