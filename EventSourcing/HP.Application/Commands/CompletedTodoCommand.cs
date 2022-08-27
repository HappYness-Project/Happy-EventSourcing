using MediatR;

namespace HP.Application.Handlers
{
    public record CompletedTodoCommand(string TodoId) : IRequest<bool>;
}
