using HP.Domain;
using MediatR;
namespace HP.Application.Queries.Todos
{
    public record GetActiveTodoItemsByTodoId(Guid TodoId) : IRequest<IEnumerable<TodoItem>>;
}
