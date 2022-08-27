using HP.Application.DTOs;
using MediatR;
namespace HP.Application.Queries.Todos
{
    public record GetTodoById(string Id) : IRequest<TodoDetailsDto>;
}
