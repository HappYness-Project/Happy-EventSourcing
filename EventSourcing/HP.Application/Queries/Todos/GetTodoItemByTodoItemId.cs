using HP.Domain;
using MediatR;
namespace HP.Application.Queries.Todos
{
    public record GetTodoItemByTodoItemId(string todoId, string todoItemId) : IRequest<TodoItem>;
}
