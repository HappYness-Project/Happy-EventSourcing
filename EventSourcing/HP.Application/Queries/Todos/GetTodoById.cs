using HP.Application.DTOs;
using MediatR;
namespace HP.Application.Queries.Todos
{
    public record GetTodoById(Guid Id) : IRequest<TodoDetailsDto>;
}
