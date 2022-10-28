using HP.Application.DTOs;
using MediatR;

namespace HP.Application.Queries.Todos
{
    public record GetTodosByUserId(string UserId) : IRequest<IEnumerable<TodoDetailsDto>>;
}
