using HP.Domain;
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
            var todo = _repository.Find(x => x.Id == request.TodoId);


            //await _repository.UpdateAsync(todo);
            return true;
        }
    }
}
