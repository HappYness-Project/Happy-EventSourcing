using HP.Domain.Todos;
using MediatR;
namespace HP.Application.Commands
{
    public record DeleteTodoCommand(string id) : IRequest<bool>;

}
