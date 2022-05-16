using HP.Domain.Todos;
using MediatR;

namespace HP.Application.Queries
{
    public record GetTodoByIdQuery(string Id) : IRequest<Todo>;

}
