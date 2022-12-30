using HP.Domain;
using MediatR;
namespace HP.Application.Queries.Todos
{
    public record GetActiveTodoItemsByTodoId(Guid todoId) : IRequest<IEnumerable<TodoItem>>;
}
