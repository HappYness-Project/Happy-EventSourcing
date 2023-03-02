using HP.Application.DTOs;
using MediatR;
namespace HP.Application.Queries.Todos
{
    public record GetCompletedTodoItemsByTodoId(Guid TodoId) : IRequest<IEnumerable<TodoItemDto>>;
}
