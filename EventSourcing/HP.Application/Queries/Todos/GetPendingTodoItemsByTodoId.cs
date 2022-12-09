using HP.Application.DTOs;
using MediatR;
namespace HP.Application.Queries.Todos
{
    public record GetPendingTodoItemsByTodoId(string todoId) : IRequest<IEnumerable<TodoItemDto>>;
}
