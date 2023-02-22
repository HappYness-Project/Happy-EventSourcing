using HP.Domain.Todos.Read;
using MediatR;
namespace HP.Application.Queries.Todos
{
    public record GetTodoById(Guid Id) : IRequest<TodoDetails>;
}
