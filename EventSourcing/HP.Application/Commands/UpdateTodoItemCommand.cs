using HP.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Application.Commands
{
    public record UpdateTodoItemCommand(string TodoId, string TodoItemId, string Title, string Desc, string Type) : IRequest<bool>;
    public class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand, bool>
    {
        private readonly ITodoRepository _repository;
        public UpdateTodoItemCommandHandler(ITodoRepository todoRepository)
        {
            this._repository = todoRepository;
        }
        public async Task<bool> Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var todo = await _repository.GetByIdAsync(request.TodoId);
            if (todo == null)
                throw new ApplicationException($"TodoId:{todo.Id} doesn't exist. ");

            var todoItem = todo.SubTodos.Where(x => x.Id == request.TodoItemId).FirstOrDefault();
            if (todoItem == null)
                throw new ApplicationException($"{request.TodoItemId} does not exist in the TodoId: {todo.Id}");

            todo.UpdateTodoItem(request.TodoItemId, request.Title, request.Desc, request.Type);
            await _repository.UpdateAsync(todo);
            var @event = new TodoDomainEvents.TodoItemRemoved(request.TodoItemId);
            return true;
        }
    }
}
