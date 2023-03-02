using HP.Application.DTOs;
using MediatR;

namespace HP.Application.Queries.Todos
{
    public record GetTodosByPersonId(Guid PersonId) : IRequest<IEnumerable<TodoDetailsDto>>;
}
