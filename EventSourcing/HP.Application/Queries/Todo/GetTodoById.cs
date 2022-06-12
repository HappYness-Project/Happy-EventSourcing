using HP.Domain.Todos;
using MediatR;

namespace HP.Application.Queries.Todo
{
    public record GetTodoById(string Id) : IRequest<Todo>;

}
