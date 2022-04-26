using HP.Domain.Todos;
using MediatR;

namespace HP.Application.Handlers
{
    public record CreateTodoCommand(string TodoTitle) : IRequest<Todo>;
}
