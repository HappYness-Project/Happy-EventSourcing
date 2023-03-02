using HP.Application.DTOs;
using MediatR;
namespace HP.Application.Queries.Todos
{
    public record GetTodosByProjectId(int ProjectId) : IRequest<IEnumerable<TodoDetailsDto>>;

}
