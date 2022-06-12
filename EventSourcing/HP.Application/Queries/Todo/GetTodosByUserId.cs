using HP.Application.DTOs;
using MediatR;

namespace HP.Application.Queries.Todo
{
    public record GetTodosByUserId(string UserId) : IRequest<IEnumerable<TodoBasicInfoDto>>;

}
