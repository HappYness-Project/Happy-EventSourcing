using HP.Application.DTOs;
using MediatR;
namespace HP.Application.Queries.Todos
{
    public record GetStartedTodoItemsByTodoId(string todoId) : IRequest<IEnumerable<TodoItemDto>>;
}
