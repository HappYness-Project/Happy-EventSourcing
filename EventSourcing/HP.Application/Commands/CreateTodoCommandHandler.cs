using HP.Domain.Todos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HP.Application.Handlers
{
    public class CreateTodoCommandHandler : IRequestHandler<CreateTodoCommand, Todo>
    {
        public Task<Todo> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
