using HP.Application.Commands;
using HP.Domain;
using MediatR;
namespace HP.Application.Handlers
{
    public record StartTodoCommand(string UserId, string TodoId) : CommandBase<Todo>;
}
