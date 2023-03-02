using HP.Application.DTOs;
using MediatR;
namespace HP.Application.Queries.Todos
{
    public record GetStoppedTodoItemsByTodoId(Guid TodoId) : IRequest<IEnumerable<TodoItemDto>>;

}
