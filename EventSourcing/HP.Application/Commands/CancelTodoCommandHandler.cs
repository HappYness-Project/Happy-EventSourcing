using HP.Application.Commands;
using MediatR;

namespace HP.Application.Handlers
{
    public class CancelTodoCommandHandler : IRequestHandler<CancelTodoCommand, bool>
    {
        public Task<bool> Handle(CancelTodoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
