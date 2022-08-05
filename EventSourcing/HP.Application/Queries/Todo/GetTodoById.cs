using HP.Application.DTOs;
using MediatR;
namespace HP.Application.Queries
{
    public record GetTodoById(string Id) : IRequest<TodoDetailsDto>;

}
