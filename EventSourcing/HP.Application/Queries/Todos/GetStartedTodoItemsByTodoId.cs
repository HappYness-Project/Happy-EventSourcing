using HP.Application.DTOs;
using MediatR;
namespace HP.Application.Queries.Todos
{
    public record GetStartedTodoItemsByTodoId(Guid TodoId) : IRequest<IEnumerable<TodoItemDto>>;
}
