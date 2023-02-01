using HP.Application.DTOs;
using MediatR;

namespace HP.Application.Queries.Todos
{
    public record GetTodosByPersonName(string PersonName) : IRequest<IEnumerable<TodoDetailsDto>>;
}
