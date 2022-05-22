using HP.Domain.Todos;
using MediatR;

namespace HP.Application.Handlers
{
    public record StartTodoCommand(string UserId, string TodoId) : IRequest<Todo>;
}
