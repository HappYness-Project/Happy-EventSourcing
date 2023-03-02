using HP.Application.DTOs;
using MediatR;
namespace HP.Application.Queries.Todos
{
    public record GetPendingTodoItemsByTodoId(Guid todoId) : IRequest<IEnumerable<TodoItemDto>>;
}
