using HP.Application.DTOs;
using MediatR;
namespace HP.Application.Queries.Todos
{
    public record GetTodos() : IRequest<IEnumerable<TodoBasicInfoDto>>;
}
