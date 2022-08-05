using HP.Application.DTOs;
using MediatR;
namespace HP.Application.Queries.Todo
{
    public record GetTodosByProjectId(string ProjectId) : IRequest<IEnumerable<TodoDetailsDto>>;

}
