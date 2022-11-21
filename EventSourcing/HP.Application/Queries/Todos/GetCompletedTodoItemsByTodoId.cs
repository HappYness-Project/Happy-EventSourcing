using HP.Domain;
using MediatR;
namespace HP.Application.Queries.Todos
{
    public record GetCompletedTodoItemsByTodoId(string todoId) : IRequest<IEnumerable<TodoItem>>;
}
