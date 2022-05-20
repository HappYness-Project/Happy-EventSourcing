using HP.Domain.Todos;
using MediatR;

namespace HP.Application.Handlers
{
    public record StartTodoCommand(string todoId) : IRequest<Todo>;
}
