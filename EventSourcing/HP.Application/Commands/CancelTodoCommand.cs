using MediatR;
namespace HP.Application.Commands
{
   public record CancelTodoCommand(string todoId) : IRequest<Unit>;
}
