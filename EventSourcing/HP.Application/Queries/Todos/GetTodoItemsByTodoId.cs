using HP.Domain;
using MediatR;
namespace HP.Application.Queries.Todos
{
    public record GetActiveTodoItemsByTodoId(string todoId) : IRequest<IEnumerable<TodoItem>>;
}
