using HP.Application.DTOs;
using MediatR;
namespace HP.Application.Queries.Todos
{
    public record GetStoppedTodoItemsByTodoId(string todoId) : IRequest<IEnumerable<TodoItemDto>>;

}
