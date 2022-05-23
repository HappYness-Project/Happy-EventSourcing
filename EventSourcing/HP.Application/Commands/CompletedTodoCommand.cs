using HP.Domain.Todos;
using MediatR;

namespace HP.Application.Handlers
{
    public record CompletedTodoCommand(string TodoId) : IRequest<Todo>;
}
