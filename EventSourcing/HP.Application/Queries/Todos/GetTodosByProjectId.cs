using HP.Application.DTOs;
using MediatR;
namespace HP.Application.Queries.Todos
{
    public record GetTodosByProjectId(string ProjectId) : IRequest<IEnumerable<TodoDetailsDto>>;

}
