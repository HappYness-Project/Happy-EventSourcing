using HP.Domain.Todos;
using MediatR;

namespace HP.Application.Queries
{
    public record GetTodoById(string Id) : IRequest<Todo>;

}
