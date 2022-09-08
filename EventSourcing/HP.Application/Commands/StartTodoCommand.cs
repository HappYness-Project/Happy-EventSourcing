using HP.Application.Commands;
using HP.Domain;
using MediatR;
namespace HP.Application.Handlers
{
    public record StartTodoCommand(string TodoId) : CommandBase<Todo>;
}
