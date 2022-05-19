using HP.Domain.Todos;
using MediatR;

namespace HP.Application.Queries
{
    public record GetTodosByUserId(string UserId) : IRequest<IEnumerable<Todo>>;

}
