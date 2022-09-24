using HP.Domain;
using MediatR;
namespace HP.Application.Queries.Todos
{
    public record GetTodoItemsByTodoId(string todoId) : IRequest<IEnumerable<TodoItem>>;
}
