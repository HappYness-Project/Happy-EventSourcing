using HP.Application.DTOs;
using MediatR;

namespace HP.Application.Queries
{
    public record GetTodosByUserId(string UserId) : IRequest<IEnumerable<TodoBasicInfoDto>>;

}
