using HP.Application.Commands;
using HP.Domain;
using MediatR;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace HP.Application.Handlers
{
    public class CancelTodoCommandHandler : IRequestHandler<CancelTodoCommand, Unit>
    {
        private readonly ITodoRepository _todoRepository;
        public CancelTodoCommandHandler(ITodoRepository todoRepository)
        {
            this._todoRepository = todoRepository;
        }
        public async Task<Unit> Handle(CancelTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(request.todoId);
            if (todo == null)
                throw new ApplicationException($"Todo ID: {request.todoId} does not exist.");

            todo.SetStatus(request.todoId, TodoStatus.Stopped);
            await _todoRepository.UpdateAsync(todo);
            return Unit.Value;
        }
    }
}
