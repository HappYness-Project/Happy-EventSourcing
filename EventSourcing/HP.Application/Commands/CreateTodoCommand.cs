using HP.Domain.Todos;
using MediatR;

namespace HP.Application.Handlers
{
    public record CreateTodoCommand(string TodoTitle, string Description = null, string Tag = null) : IRequest<Todo>;
}
