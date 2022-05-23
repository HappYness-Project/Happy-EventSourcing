using HP.Domain.Todos;
using MediatR;

namespace HP.Application.Handlers
{
    public record CreateTodoCommand(string UserId, string TodoTitle, string Type, string Description = null, string[] Tag = null) : IRequest<Todo>;
}
