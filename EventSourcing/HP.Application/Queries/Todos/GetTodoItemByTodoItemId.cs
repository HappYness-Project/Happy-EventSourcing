using HP.Domain;
using MediatR;
namespace HP.Application.Queries.Todos
{
    public record GetTodoItemByTodoItemId(Guid TodoId, Guid TodoItemId) : IRequest<TodoItem>;
}
