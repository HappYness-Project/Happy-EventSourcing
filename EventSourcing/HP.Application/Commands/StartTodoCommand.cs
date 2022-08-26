using HP.Domain;
using MediatR;
namespace HP.Application.Handlers
{
    public record StartTodoCommand(string UserId, string TodoId) : IRequest<Todo>;
}
